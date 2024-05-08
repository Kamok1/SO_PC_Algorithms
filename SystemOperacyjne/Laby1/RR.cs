using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemOperacyjne.Laby1
{
    public class RR
    {
        private List<Process> _processes { get; set; }
        private int _k = 0;
        public RR(List<Process> proccesses, int k)
        {
            _processes = proccesses.Select(x => x.Copy()).OrderBy(x => x.EnterTime).ToList();
            _k = k;
        }
        public void Start()
        {
            var processesToExecute = _processes.ToList();
            var phasesLenght = processesToExecute.ToDictionary(x => x.Id, x => x.PhaseLenght);
            double time = 0;
            do
            {
                var processes = processesToExecute.OrderBy(x=> x.EnterTime).ToList();
                foreach (var process in processes)
                {
                    if(process.PhaseLenght - _k <= 0){
                        time+= _k - process.PhaseLenght == 0 ? _k : process.PhaseLenght;
                        process.WaitingTime = time - process.EnterTime - phasesLenght.GetValueOrDefault(process.Id);
                        processesToExecute.Remove(process);
                        continue;
                    }
                    time += _k;
                    process.PhaseLenght -=_k;
                    //Console.WriteLine(process.ToString());
                }
            } while (processesToExecute.Any());

            Console.WriteLine($"RR avg time: {_processes.Sum(x => x.WaitingTime) / _processes.Count}");

        }
    }
}
