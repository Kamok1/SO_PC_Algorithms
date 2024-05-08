using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemOperacyjne.Laby4;
public class CBS
{
    private int LowerThreshold;
    private int UpperThreshold;
    private int PhysicalMemorySize;
    private List<Process> Processes;
    public CBS(int lowerThreshold, int upperThresHold, int physicalMemorySize, List<Process> processes)
    {
        LowerThreshold = lowerThreshold;
        UpperThreshold = upperThresHold;
        PhysicalMemorySize = physicalMemorySize;
        Processes = processes;
    }

    int GetUnusedFrames() 
    {
        return PhysicalMemorySize - Processes.Sum(x => x.PhysicalMemorySize); 
    }
    public void Run()
    {
        foreach (var process in Processes.Where(x => x.HasAnyRequest() == false).ToList())
        {
            Processes.Remove(process);
        }
        foreach (var process in Processes.Where(x => x.HasAnyRequest()))
        {
            if (process.LastPageFaults.Sum() < LowerThreshold && process.PhysicalMemorySize > 0 && GetNeedyProcesses().Count > 0)
            {
                process.PhysicalMemorySize--;
                DistributeFramesToNeedyProcesses();
            }
            else if (process.LastPageFaults.Sum() > UpperThreshold && GetUnusedFrames() > 0)
            {
                process.PhysicalMemorySize++;
            }
        }
    }

    List<Process> GetNeedyProcesses()
    {
        return Processes.Where(p => p.LastPageFaults.Sum() > UpperThreshold).ToList();
    }
    void DistributeFramesToNeedyProcesses()
    {
        foreach (var process in GetNeedyProcesses())
        {
            if (GetUnusedFrames() > 0)
            {
                process.PhysicalMemorySize++;
            }
        }
    }

}
