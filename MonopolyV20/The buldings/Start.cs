using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonopolyV20
{
    public class Start : Building
    {
        public int Summa { get; set; }//Сумма получения при попадание на ячейку
        public Start() { }
        public Start(string title, int number,int summa) : base(title, number)
        {
            Summa = summa;
        }
    }
}
