using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonopolyV20
{
    public class Lesion : Chances
    {
        public int WriteOffMoney { get; set; }
        public Lesion(int writeOffMoney, string title, string description) : base(title,description)
        {
            WriteOffMoney = writeOffMoney;
        }

    }
}
