using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemOperacyjne.Laby1;
public class FCFS
{
    private List<Process> _processes;
    public FCFS(List<Process> proccesses)
    {
        _processes =  proccesses.Select(x => x.Copy()).OrderBy(x => x.EnterTime).ToList();
    }
    public void Start()
    {
        double time = 0;
        for (int i = 0; i < _processes.Count; i++)
        {
            var process = _processes[i];
            if (time < process.EnterTime)
            {
                time= process.EnterTime + process.PhaseLenght;
                process.WaitingTime = 0;
            }
            else
            {
                time += process.PhaseLenght;
                process.WaitingTime = time - process.EnterTime -process.PhaseLenght; //czas zakoneczenia - czas wejscia - czas trwania
            }
            //Console.WriteLine(process.ToString());
        }
        Console.WriteLine($"FCFS avg time: {_processes.Sum(x => x.WaitingTime) / _processes.Count}");

    }
}
