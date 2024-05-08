using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemOperacyjne.Laby2
{
    public class FCFS
    {
        private List<Request> _requests;
        public FCFS(List<Request> requests)
        {
            _requests = requests.OrderBy(x => x.EnterTime).Select(x => x.Copy()).ToList();
        }
        public void Start()
        {
            var totalWaitingTime = 0;
            var currentHeadPosition = 0;
            var totalDistance = 0;
            foreach (var request in _requests)
            {
                if (totalWaitingTime < request.EnterTime)
                    totalWaitingTime = request.EnterTime;


                var traveledDistance = Math.Abs(currentHeadPosition - request.Sector);
                totalWaitingTime += traveledDistance;
                currentHeadPosition = request.Sector;
                totalDistance += traveledDistance;
                //Console.WriteLine($"[{request.Id}] | {request.EnterTime} | {request.Sector} | {request.Deadline}");
            }
            Console.WriteLine($"Total waiting time: {totalWaitingTime}");
            Console.WriteLine($"Avg waiting time: {totalWaitingTime / _requests.Count}");
            Console.WriteLine($"Avg distance traveled by cylinder: {totalDistance / _requests.Count}");
        }
    }
}
