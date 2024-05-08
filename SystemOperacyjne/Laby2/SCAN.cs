using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemOperacyjne.Laby2
{
    public class SCAN
    {
        private List<Request> _requests;
        public SCAN(List<Request> requests)
        {
            _requests = requests.OrderBy(x => x.EnterTime).Select(x => x.Copy()).ToList();
        }
        public void Start()
        {
            var countRequest = _requests.Count;
            var totalWaitingTime = 0;
            var currentHeadPosition = 0;
            var totalDistance = 0;
            var direction = 1;
            while (_requests.Any())
            {

                var availableRequests = _requests.Where(x => x.EnterTime <= totalWaitingTime).ToList();
                if(direction == 1)
                    availableRequests = availableRequests.OrderBy(x => x.Sector).ToList();
                else
                    availableRequests = availableRequests.OrderByDescending(x => x.Sector).ToList();
                foreach (var request in availableRequests)
                {
                    var traveledDistance = Math.Abs(currentHeadPosition - request.Sector);
                    totalWaitingTime += traveledDistance;
                    currentHeadPosition = request.Sector;
                    totalDistance += traveledDistance;
                    //Console.WriteLine($"[{request.Id}] | {request.EnterTime} | {request.Sector} | {request.Deadline}");
                    _requests.Remove(request);
                }
                if (availableRequests.Any() == false)
                    totalWaitingTime++;
                direction = direction == 1 ? 0 : 1;
            }
            Console.WriteLine($"Total waiting time: {totalWaitingTime}");
            Console.WriteLine($"Avg waiting time: {totalWaitingTime / countRequest}");
            Console.WriteLine($"Avg distance traveled by cylinder: {totalDistance / countRequest}");
        }
    }
}
