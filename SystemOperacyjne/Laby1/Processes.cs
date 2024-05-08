using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemOperacyjne.Laby1
{
    public class Processes
    {
        public static List<Process> GenerateProccesses(int numberOfProcesses,int minPhaseLenght, int maxPhaseLenght)
        {
            var processes = new List<Process>();
            var random = new Random();
            for (int i = 0; i < numberOfProcesses; i++)
            {
                processes.Add(new Process
                {
                    PhaseLenght = random.Next(minPhaseLenght, maxPhaseLenght),
                    EnterTime = i == 0 ? 0 : random.Next(0, 20)
                });
            }
            if(processes.Any(x => x.EnterTime == 0) == false)
                processes[0].EnterTime = 0;
            return processes;
        }
    }
}
