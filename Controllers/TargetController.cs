using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mossad.Data;
using Mossad.Models;
using Mossad.Services.LogicServices;
using Mossad.Enum;
using Mossad.Services.ControllersServices;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Data.SqlTypes;
using Mossad.Interface;
using System.Collections.Generic;




namespace Mossad.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TargetsController : ControllerBase
    {
        private readonly DBConnect _context;

        public TargetsController(DBConnect context)
        {
            _context = context;
        }

        //בקשה של רשימת כל המטרות מבסיס הנתונים (לתת גישה למנהל ולשרת סימולציה).ץ
        [HttpGet]
        public async Task<IActionResult> GetTargets()
        {
            var targets = await _context.Targets.ToArrayAsync();
            return Ok(targets);
        }



        //יצירת מטרה חדשה (לאפשר גישה רק משרת הסימולציה)ץ
        [HttpPost]
        public async Task<IActionResult> CreateTarget([FromBody] Target target)
        {
            _context.Targets.Add(target);
            await _context.SaveChangesAsync();
            return Ok($"Target: {target.Id} A target has been added to the target bank");
        }



        //הגדרת שדות מיקום למטרה (שרת סימולציה בלבד).ץ
        [HttpPut("{id}/pin")]
        public async Task<ActionResult> SetLocation(int id, [FromBody] Location location)
        {
            try
            {
                var target = await _context.Targets.FindAsync(id);
                target._Location = location;
                target.Status = TargetEnum.free;

                Agent[] arrayAgent = EntityServices.ChackMatchig(target, _context, _context.Agents, GetRange.Range(target._Location));

                foreach (Agent agent in arrayAgent)
                {
                    await _context.Missions.AddAsync(EntityServices.CreateMission(target, agent));
                }
                await _context.SaveChangesAsync();

                return StatusCode(200, new { Message = $"corrent location of target is: X= {target._Location.X}, Y= {target._Location.Y} " });
            }
            catch (Exception ex)
            {
                return StatusCode(404, new { Error = "target not exist" });
            }
        }




        //  עדכון מיקום מטרה (שרת סימולציה).ץ 
        [HttpPut("{id}/move")]
        public async Task<ActionResult> UpdateLocation(int id, [FromBody] string direction)
        {
            try
            {
                Target target = await _context.Targets.FindAsync(id);

                if (target.Status != TargetEnum.eliminated)
                {  
                    Location location = _context.Locations.FirstOrDefault(x => x.Id == target.LocationId);
                    bool result = Move.Moved(target._Location, direction, range: 0..1000);
                    if (result == true)
                    {
                        Agent[] arrayAgent = EntityServices.ChackMatchig(target, _context, _context.Agents, GetRange.Range(target._Location));
                        
                        List<Mission> newMissions = new List<Mission>(); 

                        foreach (Agent agent in arrayAgent)
                        {
                            newMissions.Add(EntityServices.CreateMission(target, agent));
                        }
                        using var transaction = await _context.Database.BeginTransactionAsync();
                        try
                        {
                            // הוספת המשימות החדשות
                            await _context.Missions.AddRangeAsync(newMissions);
                            await _context.SaveChangesAsync();

                            // מחיקת משימות 
                            var missionsToDelete = await _context.Missions
                                .Where(m => m.Status == MissionsStatus.offer && m.TargetId == id )
                                            .ToListAsync();

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
                        return StatusCode(401, new { Error = $"corrent location of target is: X=  {target._Location.X}, Y= {target._Location.Y} " });
                    }  
                }
                else
                {
                    return StatusCode(401, new { Error = "The target is eliminated " });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(404, new { Error = "target not exist" });
            }      
        }
    }
}

      


