using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemOperacyjne.Laby2
{
    public class SSFT
    {
    private List<Request> _requests;
        public SSFT(List<Request> requests)
        {
            _requests = requests.OrderBy(x => x.EnterTime).Select(x => x.Copy()).ToList();
        }
        public void Start()
        {
            var countRequest = _requests.Count;
            var totalWaitingTime = 0;
            var currentHeadPosition = 0;
            var totalDistance = 0;
            while (_requests.Any())
            {
                var availableRequests = _requests.Where(x => x.EnterTime <= totalWaitingTime).ToList();
                if (availableRequests.Any())
                {
                    var request = availableRequests.OrderBy(x => Math.Abs(currentHeadPosition - x.Sector)).First();
                    var traveledDistance = Math.Abs(currentHeadPosition - request.Sector);
                    totalWaitingTime += traveledDistance;
                    currentHeadPosition = request.Sector;
                    totalDistance += traveledDistance;
                    //Console.WriteLine($"[{request.Id}] | {request.EnterTime} | {request.Sector} | {request.Deadline}");
                    _requests.Remove(request);
                }
                else
                    totalWaitingTime++;
            }
            Console.WriteLine($"Total waiting time: {totalWaitingTime}");
            Console.WriteLine($"Avg waiting time: {totalWaitingTime / countRequest}");
            Console.WriteLine($"Avg distance traveled by cylinder: {totalDistance / countRequest}");
        }
    }
}
