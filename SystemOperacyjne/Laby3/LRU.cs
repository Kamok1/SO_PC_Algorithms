using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemOperacyjne.Laby3
{
    public static class LRU
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
            foreach (var request in requests)
            {
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
                        pages[request].LastUsed = time;
                        time++;
                        continue;
                    }
                    else
                    {
                        var pageToReplace = physicalMemory.OrderBy(x => pages[x].LastUsed).First();
                        physicalMemory.Remove(pageToReplace);
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
