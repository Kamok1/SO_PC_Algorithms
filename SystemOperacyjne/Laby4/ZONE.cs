using System;
using System.Collections.Generic;
using System.Linq;

namespace SystemOperacyjne.Laby4;
public class ZONE
{
    private int WindowSize;
    private int PhysicalMemorySize;
    private List<Process> processes;

    public ZONE(int windowSize, int physicalMemorySize, List<Process> processes)
    {
        WindowSize = windowSize;
        PhysicalMemorySize = physicalMemorySize;
        this.processes = processes;
    }

    public void Run()
    {
        foreach (var process in processes)
        {
            var recentRequests = process.Requests.TakeLast(WindowSize).ToList();
            var workingSet = new HashSet<int>(recentRequests);
            process.PhysicalMemorySize = Math.Min(workingSet.Count, PhysicalMemorySize);        
        }
        DistributeFrames();
    }
    

    private void DistributeFrames()
    {
        int totalAllocatedFrames = processes.Sum(p => p.PhysicalMemorySize);
        int availableFrames = PhysicalMemorySize - totalAllocatedFrames;

        while (availableFrames > 0)
        {
            foreach (var process in processes.Where(p => p.PhysicalMemorySize < p.Requests.Count))
            {
                if (availableFrames == 0) break;
                process.PhysicalMemorySize++;
                availableFrames--;
            }
        }
    }
}