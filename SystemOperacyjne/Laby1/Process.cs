using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemOperacyjne.Laby1
{
    public class Process
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public double PhaseLenght { get; set; }
        public double WaitingTime { get; set; }
        public double EnterTime { get; set; }
        public override string ToString()
        {
            return $"[{Id}] | {PhaseLenght} | {EnterTime} | {WaitingTime}";
        }
        public Process Copy(){
            return new Process
            {
                EnterTime = EnterTime,
                Id = Id,
                PhaseLenght = PhaseLenght,
                WaitingTime = WaitingTime
            };
        }
    }
}
