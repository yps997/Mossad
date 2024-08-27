using NuGet.Common;

namespace Mossad.MiddleWares
{
    public class TokenValidate
    {
         private readonly RequestDelegate _next;
         private static List<string> _tokens;

            public TokenValidate(RequestDelegate next, List<string> tokens )
            {
                _next = next;
                _tokens = tokens;
            }
        //public static void Validate(string token)
        //{
        //    if(token in _tokens)
        //        {
        //        InvokeAsync();
        //        }
        //}
            public async Task InvokeAsync(HttpContext context)
            {
                var reqest = context.Request;
                await this._next(context);
            }   
        }
   
    
        
}

