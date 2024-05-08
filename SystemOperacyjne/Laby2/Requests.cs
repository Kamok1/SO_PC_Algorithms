using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemOperacyjne.Laby2
{
    public static class Requests
    {
        public static List<Request> GenerateRequests(int numberOfRequests, int diskSize)
        {
            var random = new Random();  
            var requests = new List<Request>(); 
            for (int i = 0; i < numberOfRequests; i++)
            {
                requests.Add(new Request
                {
                    EnterTime = random.Next(0, 500),
                    Sector = random.Next(0, diskSize),
                    Deadline = random.Next(0, 100)
                });
            }
            requests[0].EnterTime = 0;
            return requests;
        }
    }
}
