using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Mossad.Data;
using Mossad.Models;

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
            return Ok(missions);
        }


        //בדיקת כל המשימות, טיפול בסטטוס ובמיקום שרת סימולציה בלבד
        [HttpPost("update")]
        public async Task<IActionResult>UpdatsMissions()
        {
            var missionsActive = await _context.Missions
       .Where(m => m.Status == "activ").ToArrayAsync();

            foreach (var mission in missionsActive)
            { if (Target._Location)}
            return Ok(missionsActive);
        }

       
    }
}
