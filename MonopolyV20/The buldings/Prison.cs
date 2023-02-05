using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonopolyV20
{
    public class Prison : Building
    {
        public int ExitCost { get; set; }
        public int Attempts { get; set; }
        public Prison() { }
        public Prison(string title, int number,int exitCost,int attempts) : base(title, number)
        {
            ExitCost = exitCost;
            Attempts = attempts;
        }
    }
}
