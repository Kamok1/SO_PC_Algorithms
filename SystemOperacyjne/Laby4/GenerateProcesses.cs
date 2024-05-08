using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemOperacyjne.Laby3;

namespace SystemOperacyjne.Laby4;
public static class GenerateProcesses
{
    public static List<Process> Generate(int processesCount, int virtualMemorySize, int requestsCount, int chancesOfLocality)
    {
        var processes = new List<Process>();
        var random = new Random();
        for (int i = 0; i < processesCount; i++)
        {
            var memorySize = GenerateRandomVirtualMemorySize(virtualMemorySize);
            var process = new Process
            {
                VirtualMemorySize = memorySize,
                PhysicalMemorySize = 0,
                Requests = GenerateRequests(requestsCount, memorySize, chancesOfLocality)
            };
            processes.Add(process);
        }
        return processes;
    }

    private static List<int> GenerateRequests(int count, int memorySize, int chanesOfLocality)
    {
        var requests = new List<int>();
        var random = new Random();
        for (int i = 0; i < count; i++)
        {
            if (random.Next(0, 100) < chanesOfLocality && requests.Any())
            {
                var lastRequest = requests.Last();

                if (lastRequest < 1) lastRequest++;
                if (lastRequest == memorySize) lastRequest--;
                var toAdd = lastRequest + (random.Next(0, 1) * 2 - 1);
                if (toAdd >= 0 && toAdd < memorySize) requests.Add(toAdd);
                else requests.Add(0);
                continue;
            }
            requests.Add(random.Next(memorySize));
        }
        return requests;
    }

    private static int GenerateRandomVirtualMemorySize(int maxSize)
    {
        var random = new Random();
        var chances = random.Next(100);
        if (chances < 10) return random.Next(1, maxSize / 2);
        if (chances < 30) return random.Next(1, maxSize / 4);
        if (chances < 60) return random.Next(1, maxSize / 6);
        return random.Next(1, maxSize / 8);
    }
}