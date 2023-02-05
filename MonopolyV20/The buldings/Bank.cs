using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonopolyV20
{
    public class Bank : Building 
    {
        public int Summa { get; set; }//Сумма списания денег при попадание на ячейку
        public Bank() { }
        public Bank(string title, int number,int summa) : base(title, number)
        {
            Summa = summa;
        }
    }
}
