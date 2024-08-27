using Mossad.Models;
using System.Net.WebSockets;
using Mossad.Services.LogicServices;
using Mossad.Enum;
using Mossad.Data;

using Microsoft.EntityFrameworkCore;



namespace Mossad.Services.ControllersServices

{
    public static class MissionsService
    {
        public static void StartedMission(Agent agent, Target target, Mission mission)
        {
            agent.Status = true;

            target.Status = TargetEnum.tracking;

            mission.Status = MissionsStatus.progress;
            mission.Timer = GetTime.Time(agent._Location, target._Location);
            mission.StartTime = DateTime.Now;
        }
        

        public static void FinishMission(Agent agent, Target target, Mission mission)
        {
            agent.Status = false;
            agent.BodyCount += 1;

            target.Status = TargetEnum.eliminated ;

            mission.Status = MissionsStatus.completed;
            mission.ActualTime  = DateTime.Now - mission.StartTime;       
        }


        public static void UpdateMission(string diraction, Agent agent, Target target, Mission mission)
        {
            Move.Moved(agent._Location, diraction, new Range(0, 1000));
            mission.Timer = GetTime.Time(agent._Location, target._Location);          
        }

      
       

    }
}
