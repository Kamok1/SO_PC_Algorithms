using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemOperacyjne.Laby3
{
    public static class Requests
    {
        public static List<int> GenerateRequests(int count, int virtualMemorySize, float chancesOfLocality)
        {
            var requests = new List<int>();
            var random = new Random();
            for (int i = 0; i < count; i++)
            {
                if(random.Next(0,100) < chancesOfLocality && requests.Any())
                {
                    var lastRequest = requests.Last();

                    if (lastRequest < 1) lastRequest++;
                    if (lastRequest == virtualMemorySize) lastRequest--;
                    requests.Add(lastRequest + (random.Next(0, 1) * 2 - 1));
                    continue;
                }
                requests.Add(random.Next(0, virtualMemorySize));
            }
            return requests;
        }
    }
}
