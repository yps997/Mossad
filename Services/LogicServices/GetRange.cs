using Mossad.Models;

namespace Mossad.Services.LogicServices
{
    public static class GetRange
    {
      
        public static (Range, Range) Range(Location location)
        {
            Range range_x = Math.Max(0, location.X - 200)..Math.Min(1000, location.X + 200);
            Range range_y = Math.Max(0, location.Y - 200)..Math.Min(1000, location.Y + 200);
            return (range_x, range_y);
        }
    }
}
