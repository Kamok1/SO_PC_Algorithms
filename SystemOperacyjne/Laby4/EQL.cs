using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SystemOperacyjne.Laby4;
public static class EQL
{   
    public static void Run(List<Process> processes, int physicalMemorySize)
    {
        var equalPageSize = physicalMemorySize / processes.Count;
        foreach (var process in processes)
        {
            process.PhysicalMemorySize = equalPageSize;
        }
    }

}
