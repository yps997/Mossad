using Microsoft.EntityFrameworkCore;
using Mossad.Models;
using Mossad.Services.LogicServices;
using Mossad.Data;
using Mossad.Interface;
using Mossad.Enum;

namespace Mossad.Services.ControllersServices
{
    public static class EntityServices
    {

        public static T[] ChackMatchig<T>(IEntity<Location> entity, DbContext context, DbSet<T> table, (Range, Range) area)
            where T : class, IEntity<Location>
        {
            return Matching.ListMatchingObjects(context, table, GetRange.Range(entity._Location));
        }
        public static Mission CreateMission(Target target, Agent agent)
        {
            Mission mission = new Mission();
            mission.TargetId = target.Id;
            mission.AgentId = agent.Id;
            mission.Status = MissionsStatus.offer;
            mission.Timer = GetTime.Time(agent._Location, target._Location);
            mission.StartTime = DateTime.Now;
            
            return mission;
        }
    }
}

