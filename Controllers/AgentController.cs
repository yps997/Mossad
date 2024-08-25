using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Mossad.Data;
using Mossad.Models;
using System.Linq.Expressions;
using Mossad.SyetemLogicMap;
using Mossad.SyetemMap;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;

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
                return StatusCode(200, new { Message = $"corrent location of agent is: {agent._Location} " });
            }
            catch (Exception ex)
            {
                return StatusCode(404, new { Error = "agent not exist" });
            }
        }


        //  משמש למיקום ראשוני על המפה ועדכון סוכן שכבר על המפה (שרת סימולציה).ץ 
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
                        return StatusCode(200);
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


    
        

        