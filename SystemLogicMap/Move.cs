using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using Mossad.Interface;
using Mossad.Models;
namespace Mossad.SyetemMap
{
    public static class Move
    {
        public static bool Moved(Location location, string direction, Range range)
        {
            int x = location.X;
            int y = location.Y;

            switch (direction)
            { 
                case "nw":
                    x -= 1;
                    y -= 1;
                    return RangeCheck(location, x, y, range);
                //צפון
                case "n":
                    y -= 1;
                    return RangeCheck(location, x, y, range);
                // צפון מזרח
                case "ne":
                    x += 1;
                    y -= 1;
                    return RangeCheck(location, x, y, range);
                //מזרח
                case "e":
                    x += 1;
                    return RangeCheck(location, x, y, range);
                // דרום מזרח
                case "se":
                    x += 1;
                    y += 1;
                    return RangeCheck(location, x, y, range);
                //דרום 
                case "s":
                    y += 1;
                    return RangeCheck(location, x, y, range);
                // דרום מערב
                case "sw":
                    x -= 1;
                    y += 1;
                    return RangeCheck(location, x, y, range);
                // מערב
                case "w":
                    x -= 1;
                    return RangeCheck(location, x, y, range);
                default: return false;
            }
        }
        public static bool RangeCheck(Location location,int x, int y, Range range)
        {
            if (x >= range.Start.Value && x <= range.End.Value && y >= range.Start.Value && y <= range.End.Value)
            {
                location.X = x;
                location.Y = y;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
