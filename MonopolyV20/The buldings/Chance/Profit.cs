using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonopolyV20
{
    public class Profit : Chances
    {
        public int GettingMoney { get; set; }
        public Profit(int gettingMoney,string title,string description) : base(title, description)
        {
            GettingMoney = gettingMoney;
        }

    }
}
