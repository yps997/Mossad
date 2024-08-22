using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Mossad.Data;
using Mossad.Modles;
using System.Linq.Expressions;

namespace Mossad.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AgentController : ControllerBase
    {
        private readonly DBConnect _context;

        public AgentController(DBConnect context)
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
            return Ok($"Agint {agent.Name} insert to mossad");
        }



        //הגדרת שדות מיקום לסוכן (שרת סימולציה בלבד).ץ
        [HttpPut("{id}/pin")]
        public async Task<ActionResult> SetLocation(Guid id, [FromBody] Location location)
        {
            var agent = await _context.Agents.FindAsync(id);
            if (agent == null)
            { return NotFound("agent not exist"); }

            agent.Location = location;
            return Ok(agent);

        }



        //  משמש למיקום ראשוני על המפה ועדכון סוכן שכבר על המפה (שרת סימולציה).ץ 
        [HttpPut("{id}/move")]
        public async Task<ActionResult> UpdateLocation(Guid id, [FromBody] string direction)
        {
            try
            {
                var agent = await _context.Agents.FindAsync(id);
                if (agent.Status = false)
                {
                    switch (direction)
                    {
                        //צפון מערב
                        case "nw":
                            agent.Location.X -= 1;
                            agent.Location.Y -= 1;
                            break;
                        //צפון
                        case "n":
                            agent.Location.Y -= 1;
                            break;
                        // צפון מזרח
                        case "ne":
                            agent.Location.X += 1;
                            agent.Location.Y -= 1;
                            break;
                        //מזרח
                        case "e":
                            agent.Location.X += 1;
                            break;
                        // דרום מזרח
                        case "se":
                            agent.Location.X += 1;
                            agent.Location.Y += 1;
                            break;
                        //דרום 
                        case "s":
                            agent.Location.Y += 1;
                            break;
                        // דרום מערב
                        case "sw":
                            agent.Location.X -= 1;
                            agent.Location.Y += 1;
                            break;
                        // מערב
                        case "w":
                            agent.Location.X -= 1;
                            break;
                    }
                    return Ok($" the location of agent is: {agent.Location}");
                }
                else { return BadRequest("The agent is busy with other tasks"); }
            }
            catch (Exception ex)
            {
                return NotFound("agent not exist");
            }



        }
    }
}


    
        

        