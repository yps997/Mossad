using Microsoft.EntityFrameworkCore;
using Mossad.Data;
using Mossad.Interface;
using Mossad.Models;


namespace Mossad.Services.LogicServices
{
    public static class Matching
    { 
        public static T[] ListMatchingObjects<T>(DbContext context, DbSet<T> table, (Range, Range)area)
            where T : class, IEntity<Location>
        {
            return table
                .Where(entity =>
                    entity._Location.X >= area.Item1.Start.Value &&
                    entity._Location.X <= area.Item1.End.Value &&
                    entity._Location.Y >= area.Item2.Start.Value &&
                    entity._Location.Y <= area.Item2.End.Value)
                .ToArray();

        }


        public static bool MatchingObject((Range, Range) area, Location location)
        {
            return
            location.X >= area.Item1.Start.Value &&
            location.X <= area.Item1.End.Value &&
            location.Y >= area.Item2.Start.Value &&
            location.Y <= area.Item2.End.Value;
        }

    }
}
