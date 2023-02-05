using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonopolyV20
{
    public class Tax : Building
    {
        public int Summa { get; set; }//налог на роскаш
        public Tax(string title, int number,int summa ) : base(title, number)
        {
            Summa = summa;
        }
    }
}
