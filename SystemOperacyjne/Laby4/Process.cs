using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemOperacyjne.Laby4;
public class Process : ICloneable
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public int VirtualMemorySize { get; set; }
    public int PhysicalMemorySize { get; set; }
    public List<int> Requests { get; set; }
    public int PageFaults { get; set; }
    public List<int> LastPageFaults { get; set; } = new List<int>();
    public List<int> LastRequests { get; set; } = new List<int>();
    public HashSet<int> CurrentPages { get; set; } = new HashSet<int>(); 


    public bool HasAnyRequest()
    {
        return Requests.Any(x => x != 0);
    }

    public object Clone()
    {
        Process process = new Process
        {
            Id = this.Id,
            VirtualMemorySize = this.VirtualMemorySize,
            PhysicalMemorySize = this.PhysicalMemorySize,
            Requests = new List<int>(this.Requests),
            PageFaults = this.PageFaults
        };
        return process;
    }
}
