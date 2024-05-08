using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemOperacyjne.Laby1
{
    public class SJFW
    {
        private List<Process> _processes { get; set; }
        public SJFW(List<Process> proccesses)
        {
            _processes = proccesses.Select(x => x.Copy()).ToList();
        }
        public void Start()
        {
            var processesToExecute = _processes.ToList();
            var phasesLenght = processesToExecute.ToDictionary(x => x.Id, x => x.PhaseLenght);
            double time = 0;
            do
            {
                var process = processesToExecute.Where(x => x.EnterTime <= time).OrderBy(x => x.PhaseLenght).FirstOrDefault();
                if (process.Equals(null))
                {
                    time++;
                    continue;
                }
                time++;
                process.PhaseLenght--;
                if (process.PhaseLenght == 0){
                    processesToExecute.Remove(process);
                    process.WaitingTime = time - process.EnterTime - phasesLenght.GetValueOrDefault(process.Id);
                }
                //Console.WriteLine(process.ToString());
            } while (processesToExecute.Any());

            Console.WriteLine($"SJFW avg time: {_processes.Sum(x => x.WaitingTime) / _processes.Count}");

        }
    }
}
