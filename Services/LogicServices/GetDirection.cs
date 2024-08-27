using Mossad.Models;

namespace Mossad.Services.LogicServices

{
    public static class GetDirection
    {
        public static string Direction(Location target, Location agent)
        {
            return (target.X - agent.X, target.Y - agent.Y) switch
            {
                ( > 0, < 0) => "ne",  // צפון מזרח
                ( < 0, < 0) => "nw",  // צפון מערב
                (0, < 0) => "n",   // צפון
                ( > 0, 0) => "e",   // מזרח
                ( > 0, > 0) => "se",  // דרום מזרח
                (0, > 0) => "s",   // דרום
                ( < 0, > 0) => "sw",  // דרום מערב
                ( < 0, 0) => "w",   // מערב
                _ => "same" // אותו מיקום
            };

        }
    }
}

