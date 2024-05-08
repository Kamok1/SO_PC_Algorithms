using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemOperacyjne.Laby3
{
    public class OPT
    {
        public static int Run(List<int> requests, int physicalMemorySize, int virtualMemorySize)
        {
            var physicalMemory = new List<int>();
            var pages = new List<Page>();
            for (int i = 0; i < virtualMemorySize; i++)
            {
                pages.Add(new Page { Id = i.ToString() });
            }

            var pageFaults = 0;
            var time = 0;
            for (int i = 0; i < requests.Count; i++)
            {
                int request = requests[i];
                if (physicalMemory.Contains(request))
                {
                    pages[request].LastUsed = time;
                    time++;
                    continue;
                }
                else
                {
                    pageFaults++;
                    if (physicalMemory.Count < physicalMemorySize)
                    {
                        physicalMemory.Add(request);
                        pages[request].EnterTime = time;
                        time++;
                        continue;
                    }
                    else
                    {

                        var undoneRequestes = requests.Skip(i).ToList();
                        var uselessPages = pages.Select(x => int.Parse(x.Id)).ToList().Except(undoneRequestes);
                        if (uselessPages.Any())
                            physicalMemory.Remove(uselessPages.Last());
                        else
                            physicalMemory.Remove(undoneRequestes.Last());
                        
                        physicalMemory.Add(request);
                        pages[request].EnterTime = time;
                        time++;
                    }
                }
            }
            return pageFaults;
        }
    }
}
