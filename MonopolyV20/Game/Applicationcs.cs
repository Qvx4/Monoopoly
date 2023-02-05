using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonopolyV20
{
    public class Applicationcs
    {
        public char Palyer { get; set; }
        public int Money { get; set; }
        public List<Building> Buldings { get; set; }
        public int[] NumberCell { get; set; }
        public ConsoleColor PlayerColor { get; set; }
        public Applicationcs()
        {
            Buldings = new List<Building>();
        }
    }
}
