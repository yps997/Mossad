using Mossad.Interface;
using Mossad.Modles;
namespace Mossad.SyetemMap
{
    public static class Move
    {
        public static void Moved(Location location, string direction)
        {
            switch (direction)
            {
                //צפון מערב
                case "nw":
                    location.X -= 1;
                    location.Y -= 1;
                    break;
                //צפון
                case "n":
                    location.Y -= 1;
                    break;
                // צפון מזרח
                case "ne":
                    location.X += 1;
                    location.Y -= 1;
                    break;
                //מזרח
                case "e":
                    location.X += 1;
                    break;
                // דרום מזרח
                case "se":
                    location.X += 1;
                    location.Y += 1;
                    break;
                //דרום 
                case "s":
                    location.Y += 1;
                    break;
                // דרום מערב
                case "sw":
                    location.X -= 1;
                    location.Y += 1;
                    break;
                // מערב
                case "w":
                    location.X -= 1;
                    break;
            }
        }
    }
}
