using Microsoft.EntityFrameworkCore;
using Mossad.Data;
using Mossad.Interface;
using Mossad.Models;
using Mossad.SyetemLogicMap;

namespace Mossad.SyetemArea
{
    public static class Scaner
    { 
        public static List<IEntity<Location>> GetObject(DbContext context, DbSet<IEntity<Location>> table, (Range, Range)area)
        {
            return table
                .Where(entity =>
                    entity._Location.X >= area.Item1.Start.Value &&
                    entity._Location.X <= area.Item1.End.Value &&
                    entity._Location.Y >= area.Item2.Start.Value &&
                    entity._Location.Y <= area.Item2.End.Value)
                .ToList();

        }
    }
}
