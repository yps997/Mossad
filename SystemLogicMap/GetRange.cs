using Mossad.Modles;

namespace Mossad.SyetemArea
{
    public static class GetRange
    {
      
        public static (Range, Range) Range(Location location)
        {
            Range range_x = Math.Min(0, location.X - 200)..Math.Max(1000, location.X + 200);
            Range range_y = Math.Min(0, location.Y - 200)..Math.Max(1000, location.Y + 200);
            return (range_x, range_y);
        }
    }
}
