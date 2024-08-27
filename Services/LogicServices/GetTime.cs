using Mossad.Models;
namespace Mossad.Services.LogicServices
{
    public class GetTime
    {
        public static string Time(Location location1, Location location2)
        {
            double calculation = Math.Sqrt(Math.Pow(location2.X - location1.X, 2) + Math.Pow(location2.Y - location1.Y, 2)) / 5;

            int hours = (int)(calculation / 60);
            int minutes = (int)(calculation % 60);

            return $"{hours:D2}:{minutes:D2}";
        }
    }
}
