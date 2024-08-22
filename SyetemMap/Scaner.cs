using Mossad.Data;
using Mossad.Modles;

namespace Mossad.SyetemArea
{
    public static class Scaner
    { 
        public static int GetObject ((Range, Range)area)
        {
            List<Target> targetsInRange = DBConnect.Targets(context, area);
            return null;
        }
    }
}
