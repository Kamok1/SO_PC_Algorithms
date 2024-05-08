using System.Runtime.CompilerServices;
using SystemOperacyjne.Laby4;
using SystemOperacyjne.Laby4;

const int VirtualMemory = 100; //Ile stron wirtualnej pamięci
const int PhysicalMemory = 40; //Ramki trzymają strony
const int RequestsCount = 10;
const int CBSFrequency = 5;

var requests = GenerateProcesses.Generate(RequestsCount, VirtualMemory, 50, 50);
var requests1 = requests.Clone();


EQL.Run(requests, PhysicalMemory);
var cbsRequests = requests.Clone();
var zoneRequests = requests.Clone();
var eqlErrors = Run(requests);
Console.WriteLine($"EQL: {eqlErrors}");


PROP.Run(requests1, PhysicalMemory);
var propErrors = Run(requests1);
Console.WriteLine($"PROP: {propErrors}");

var cbsErrors = RunCbs(cbsRequests, PhysicalMemory);
Console.WriteLine($"CBS: {cbsErrors}");

var zone = RunZone(zoneRequests, PhysicalMemory);
Console.WriteLine($"ZONE: {zone}");

static int Run(List<Process> processes)
{
    while (processes.Any(x => x.HasAnyRequest()))
    {
        foreach (var process in processes.Where(x => x.HasAnyRequest()))
        {
            var requests = process.Requests.Take(process.PhysicalMemorySize).ToList();
            if (requests.Count == 0) continue;
            process.PageFaults += LRU.Run(requests, process.PhysicalMemorySize, process.VirtualMemorySize);
            process.Requests.RemoveRange(0, requests.Count);
        }
    }
    return processes.Sum(x => x.PageFaults);
}


static int RunCbs(List<Process> processes, int physicalMemorySize)
{
    var cbs = new CBS(10, 90, physicalMemorySize, processes);
    int timeTicks = 0;
    int totalFrames = 0;
    while (processes.Any(x => x.HasAnyRequest()))
    {
        cbs.Run();
        totalFrames += processes.Sum(p => p.Requests.Count);
        foreach (var process in processes.Where(x => x.HasAnyRequest()))
        {
            var requests = process.Requests.Take(process.PhysicalMemorySize).ToList();
            if (requests.Count == 0) continue;
            var errors = LRU.Run(requests, process.PhysicalMemorySize, process.VirtualMemorySize);
            process.PageFaults += errors;
            process.LastPageFaults.Add(errors);
            process.Requests.RemoveRange(0, requests.Count);
            if(process.LastPageFaults.Count > CBSFrequency)
            {
                process.LastPageFaults.RemoveAt(0);
            }
        }
        
    }
    return processes.Sum(x => x.PageFaults);
}


static int RunZone(List<Process> processes, int physicalMemorySize)
{
    var zone = new ZONE(5, PhysicalMemory, processes);

    while (processes.Any(x => x.HasAnyRequest()))
    {
        zone.Run();

        foreach (var process in processes.Where(x => x.HasAnyRequest()))
        {
            var requests = process.Requests.Take(process.PhysicalMemorySize).ToList();
            if (requests.Count == 0) continue;
            var errors = LRU.Run(requests, process.PhysicalMemorySize, process.VirtualMemorySize);
            process.PageFaults += errors;
            process.Requests.RemoveRange(0, requests.Count);
        }
    }
    return processes.Sum(x => x.PageFaults);
}



static class Extensions
{
    public static List<T> Clone<T>(this IList<T> listToClone) where T : ICloneable
    {
        return listToClone.Select(item => (T)item.Clone()).ToList();
    }
}

