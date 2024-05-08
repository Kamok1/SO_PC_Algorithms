using System;
using System.Collections.Generic;
using System.Linq;

namespace SystemOperacyjne.Laby3
{
    public class ALRU
    {
        public static int Run(List<int> requests, int physicalMemorySize, int virtualMemorySize)
        {
            var physicalMemory = new List<Page>();
            var queue = new Queue<Page>();
            var pages = new List<Page>();
            for (int i = 0; i < virtualMemorySize; i++)
            {
                pages.Add(new Page { Id = i.ToString() });
            }

            var pageFaults = 0;
            foreach (var request in requests)
            {
                var requestedPage = pages[request];
                if (physicalMemory.Any(p => p.Id == requestedPage.Id))
                {
                    requestedPage.Bit = 1; 
                    continue;
                }
                else
                {
                    pageFaults++;
                    if (physicalMemory.Count < physicalMemorySize)
                    {
                        physicalMemory.Add(requestedPage);
                    }
                    else
                    {
                        Page pageToRemove = null;
                        while (queue.Count > 0)
                        {
                            var page = queue.Dequeue();
                            if (page.Bit == 0)
                            {
                                pageToRemove = page;
                                break;
                            }
                            page.Bit = 0; 
                            queue.Enqueue(page); 
                        }

                        if (pageToRemove == null)
                        {
                            pageToRemove = queue.Dequeue(); 
                        }

                        physicalMemory.Remove(pageToRemove);
                        physicalMemory.Add(requestedPage);
                    }
                    requestedPage.Bit = 1;
                    queue.Enqueue(requestedPage); 
                }
            }
            return pageFaults;
        }
    }
}
