using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Mossad.Data;
using Mossad.Models;
using Mossad.Services.ControllersServices;
using Mossad.Services.LogicServices;
using Mossad.Enum;


namespace Mossad.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MissionsController : ControllerBase
    {
        private readonly DBConnect _context;

        public MissionsController(DBConnect context)
        {
            _context = context;
        }


        //בקשה של רשימת כל המשימות מבסיס הנתונים (לתת גישה למנהל ולשרת סימולציה).ץ
        [HttpGet]
        public async Task<IActionResult> GetMissions()
        {
            var missions = await _context.Missions.ToArrayAsync();
            return StatusCode(200, missions);
        }


        //בדיקת כל המשימות, טיפול בסטטוס ובמיקום שרת סימולציה בלבד
        [HttpPost("update")]
        public async Task<IActionResult> CheckAndUpdatsMissions()
        {
            var missionsActive = await _context.Missions
       .Where(m => m.Status == MissionsStatus.progress).ToArrayAsync();

            foreach (Mission mission in missionsActive)
            {
                Agent agent = await _context.Agents
                    .FirstOrDefaultAsync(a => a.Id == mission.AgentId);

                Target target = await _context.Targets
                    .FirstOrDefaultAsync(t => t.Id == mission.TargetId);

                string direction = GetDirection.Direction(target._Location, agent._Location);
                if (direction == "same")
                {
                    MissionsService.FinishMission(agent, target, mission);
                }
                else
                {
                    MissionsService.UpdateMission(direction, agent, target, mission);
                    if (agent._Location == target._Location)
                    {
                        MissionsService.FinishMission(agent, target, mission);
                    }
                }
                await _context.SaveChangesAsync();

            }
            return StatusCode(200);
        }

        //התחלת משימה
        [HttpPut("{id}")]
        public async Task<IActionResult> Running(int id, string status)
        {
            Mission mission = await _context.Missions
                    .FirstOrDefaultAsync(m => m.Id == id);

            Agent agent = await _context.Agents
                   .FirstOrDefaultAsync(a => a.Id == mission.AgentId);

            Target target = await _context.Targets
                .FirstOrDefaultAsync(t => t.Id == mission.TargetId);


            if (Matching.MatchingObject(GetRange.Range(agent._Location), target._Location) == true)
            {
                MissionsService.StartedMission(agent, target, mission);
                
                var missionsToDelete = await _context.Missions
                .Where(m => (m.AgentId == mission.AgentId || m.TargetId == mission.TargetId) && m.Id != mission.Id && m.Status == MissionsStatus.offer)
                .ToListAsync();
                _context.Missions.RemoveRange(missionsToDelete);

                await _context.SaveChangesAsync();
                return StatusCode(200);
            }
            else 
            {
                _context.Missions.Remove(mission);
                await _context.SaveChangesAsync();
                return StatusCode(401, new { Error = $"agent and target not in range" });
            }


        }
    }
}
