using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mossad.Data;
using Mossad.Modles;

namespace Mossad.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TargetController : ControllerBase
    {
        private readonly DBConnect _context;

        public TargetController(DBConnect context)
        {
            _context = context;
        }

        //בקשה של רשימת כל המטרות מבסיס הנתונים (לתת גישה למנהל ולשרת סימולציה).ץ
        [HttpGet]
        public async Task<IActionResult> GetTargets()
        {
            var agents = await _context.Agents.ToArrayAsync();
            return Ok(agents);
        }



        //יצירת מטרה חדשה (לאפשר גישה רק משרת הסימולציה)ץ
        [HttpPost]
        public async Task<IActionResult> CreateTarget([FromBody] string Name, string position ,string? photo_url)
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
        public async Task<ActionResult> SetLocation(Guid id, [FromBody] Location location)
        {
            var target = await _context.Targets.FindAsync(id);
            if (target == null)
            { return NotFound("target not exist"); }

            target.Location = location;
            return Ok(target);

        }



        //  משמש למיקום ראשוני על המפה ועדכון מטרה שכבר על המפה (שרת סימולציה).ץ 
        [HttpPut("{id}/move")]
        public async Task<ActionResult> UpdateLocation(Guid id, [FromBody] string direction)
        {
            try
            {
                var target = await _context.Agents.FindAsync(id);
                if (target.Status = true)
                {
                    switch (direction)
                    {
                        //צפון מערב
                        case "nw":
                            target.Location.X -= 1;
                            target.Location.Y -= 1;
                            break;
                        //צפון
                        case "n":
                            target.Location.Y -= 1;
                            break;
                        // צפון מזרח
                        case "ne":
                            target.Location.X += 1;
                            target.Location.Y -= 1;
                            break;
                        //מזרח
                        case "e":
                            target.Location.X += 1;
                            break;
                        // דרום מזרח
                        case "se":
                            target.Location.X += 1;
                            target.Location.Y += 1;
                            break;
                        //דרום 
                        case "s":
                            target.Location.Y += 1;
                            break;
                        // דרום מערב
                        case "sw":
                            target.Location.X -= 1;
                            target.Location.Y += 1;
                            break;
                        // מערב
                        case "w":
                            target.Location.X -= 1;
                            break;
                    }
                    return Ok($" the location of target is: {target.Location}");
                }
                else { return BadRequest("The target is dead"); }
            }
            catch (Exception ex)
            {
                return NotFound("target not exist");
            }
        }
    }
}

