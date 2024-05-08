using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemOperacyjne.Laby4;
public class PROP
{
    public static void Run(List<Process> processes, int physicalMemorySize)
    {
        var totalVirtualMemorySize = processes.Sum(x => x.VirtualMemorySize);
        foreach (var process in processes)
        {
            process.PhysicalMemorySize = (int)Math.Round((double)process.VirtualMemorySize / totalVirtualMemorySize * physicalMemorySize);
        }
    }
}
