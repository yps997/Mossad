using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mossad.Data;
using Mossad.Models;
using Mossad.SyetemMap;

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
        public async Task<IActionResult> CreateTarget([FromBody] string Name, string position, string? photo_url)
        {
            Target target = new Target();
            target.Name = Name;
            target.Image = photo_url;
            target.Position = position;
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
                return StatusCode(200, new { Message = $"corrent location of target is: {target._Location} " });
            }
            catch (Exception ex)
            {
                return StatusCode(404, new { Error = "target not exist" });
            }
        }




        //  משמש למיקום ראשוני על המפה ועדכון מטרה שכבר על המפה (שרת סימולציה).ץ 
        [HttpPut("{id}/move")]
        public async Task<ActionResult> UpdateLocation(int id, [FromBody] string direction)
        {
            try
            {
                var target = await _context.Targets.FindAsync(id);

                if (target.Status != false)
                {
                    bool result = Move.Moved(target._Location, direction, range: 0..1000);
                    if (result == true)
                    {
                        return StatusCode(200);
                    }
                    else
                    {
                        return StatusCode(401, new { Error = $"Out of range, corrent target location is: {target._Location}" });
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

      


