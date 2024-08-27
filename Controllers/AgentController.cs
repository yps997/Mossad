using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Mossad.Data;
using Mossad.Models;
using System.Linq.Expressions;
using Mossad.Services.LogicServices;

using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Mossad.Services.ControllersServices;
using Mossad.Enum;

namespace Mossad.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AgentsController : ControllerBase
    {
        private readonly DBConnect _context;

        public AgentsController(DBConnect context)
        {
            _context = context;
        }

        //בקשה של רשימת כל הסוכנים מבסיס הנתונים (לתת גישה למנהל ולשרת סימולציה).ץ
        [HttpGet]
        public async Task<IActionResult> GetAgents()
        {
            var agents = await _context.Agents.ToArrayAsync();
            return Ok(agents);
        }



        //יצירת סוכן חדש (לאפשר גישה רק משרת הסימולציה)ץ
        [HttpPost]
        public async Task<IActionResult> CreateAgents([FromBody] string nickName, string photo_url)
        {
            Agent agent = new Agent();
            agent.Name = nickName;
            agent.Image = photo_url;
            _context.Agents.Add(agent);
            await _context.SaveChangesAsync();
            return Ok($"Agint {agent.Id} insert to mossad");
        }



        //הגדרת שדות מיקום לסוכן (שרת סימולציה בלבד).ץ
        [HttpPut("{id}/pin")]
        public async Task<ActionResult> SetLocation(int id, [FromBody] Location location)
        {
            try
            {
                var agent = await _context.Agents.FindAsync(id);
                agent._Location = location;

                Target[] arrayTarget = EntityServices.ChackMatchig(agent, _context, _context.Targets, GetRange.Range(agent._Location));

                foreach (Target target in arrayTarget)
                {
                    EntityServices.CreateMission(target, agent);
                }
                await _context.SaveChangesAsync();
                return StatusCode(200, new { Message = $"corrent location of agent is: {agent._Location} " });
            }
            catch (Exception ex)
            {
                return StatusCode(404, new { Error = "agent not exist" });
            }
        }


        //  עדכון סוכן שכבר על המפה (שרת סימולציה).ץ 
        [HttpPut("{id}/move")]
        public async Task<ActionResult> UpdateLocation(int id, [FromBody] string direction)
        {
            try
            {
                var agent = await _context.Agents.FindAsync(id);

                if (agent.Status != false)
                {
                    bool result = Move.Moved(agent._Location, direction, range: 0..1000);
                    if (result == true)
                    {
                        Target[] arrayAgent = EntityServices.ChackMatchig(agent, _context, _context.Targets, GetRange.Range(agent._Location));
                        List<Mission> newMissions = new List<Mission>();

                        foreach (Target target in arrayAgent)
                        {
                            newMissions.Add(EntityServices.CreateMission(target, agent));
                        }
                        using var transaction = await _context.Database.BeginTransactionAsync();
                        try
                        {
                            // הוספת המשימות 
                            await _context.Missions.AddRangeAsync(newMissions);
                            await _context.SaveChangesAsync();

                            // מחיקת משימות 
                            var missionsToDelete = await _context.Missions
                                .Where(m => m.Status == MissionsStatus.offer && m.AgentId == id)
                                            .ToListAsync();

                            //יישום הפעולות
                            _context.Missions.RemoveRange(missionsToDelete);
                            await _context.SaveChangesAsync();

                            //אישור טרנקזציה
                            await transaction.CommitAsync();
                            return StatusCode(200);
                        }
                        catch (Exception ex)
                        {
                            // שגיאת טרנקזציה
                            await transaction.RollbackAsync();
                            return StatusCode(401, new { Error = $"Update mission for target falde" });
                        }
                    }
                    else
                    {
                        return StatusCode(401, new { Error = $"Out of range, corrent agent location is: {agent._Location}" });
                    }
                }
                else
                {
                    return StatusCode(401, new { Error = "The agent is busy with other tasks" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(404, new { Error = "agent not exist" });
            }
        }
    }
}


    
        

        