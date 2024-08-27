using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using Mossad.Data;
using Mossad.Models;


namespace Mossad.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginControler : ControllerBase
    {
        private readonly DBConnect _context;

        public LoginControler(DBConnect context)
        {
            _context = context;
        }

        
        private async Task CreateToken()
        {
            Guid token = new Guid();  
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(string name, string? password)
        {
            try
            {
                User user = _context.Users.FirstOrDefault(u => u.Name == name);
                if (user.Name == name)
                {
                    return StatusCode(200
                            , new { token = CreateToken().ToString() });
                }
                else
                {
                    return StatusCode(401
                             , new { masseg = $"one or more details of {name} not valid" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(404,
                        new { error = "not found" });
            }
        }
    }

}
