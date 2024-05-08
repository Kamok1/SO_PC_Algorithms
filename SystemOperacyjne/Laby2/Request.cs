using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemOperacyjne.Laby2
{
    public class Request
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public int EnterTime { get; set; } 
        public int Sector { get; set; }
        public int Deadline { get; set; }
        public Request Copy(){
            return new Request
            {
                EnterTime = EnterTime,
                Id = Id,
                Sector = Sector,
                Deadline = Deadline
            };
        }
    }
}
