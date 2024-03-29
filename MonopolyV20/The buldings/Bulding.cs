﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonopolyV20
{
    public abstract class Building
    {
        public string Title { get; set; }//название 
        public int Number { get; set; }//номер ячейки 
        public List<char> Symbol { get; set; }//фигурка игрока
        public int BusinessDowntrun { get; set; } //cчет когда спадет бизнес
        public Building() { }
        public Building(string title,int number)
        {
            BusinessDowntrun = 15;
            Title = title;
            Number = number;
            Symbol = new List<char>();
        }

    }
}
