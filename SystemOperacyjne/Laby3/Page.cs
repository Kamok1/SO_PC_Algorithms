using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemOperacyjne.Laby3
{
    public class Page
    {
        public string Id { get; set; } = new Guid().ToString();
        public int EnterTime { get; set; } = 0;
        public int LastUsed { get; set; } = 0;
        public int Bit { get; set; } = 0;
    }
}
