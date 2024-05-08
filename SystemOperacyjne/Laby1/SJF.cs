using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemOperacyjne.Laby1
{
    public class SJF
    {
        private List<Process> _processes { get; set; }
        public SJF(List<Process> proccesses)
        {
            _processes = proccesses.Select(x => x.Copy()).ToList();
        }
        public void Start()
        {
            var processesToExecute = _processes.ToList();
            double time = 0;
            do
            {
                var process = processesToExecute.Where(x => x.EnterTime <= time).OrderBy(x => x.PhaseLenght).FirstOrDefault();
                if(process.Equals(null))
                {
                    time++;
                    continue;
                }
                time += process.PhaseLenght;
                process.WaitingTime = time - process.EnterTime - process.PhaseLenght;
                processesToExecute.Remove(process);
                //Console.WriteLine(process.ToString());
            } while (processesToExecute.Any());

            Console.WriteLine($"SJF avg time: {_processes.Sum(x => x.WaitingTime) / _processes.Count}");

        }
    }
}
