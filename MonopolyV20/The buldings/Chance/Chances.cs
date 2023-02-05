using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonopolyV20
{
    public abstract class Chances
    {
        public string Title { get; set; }
        public string Description { get; set; }

        public Chances() { }
        public Chances(string title, string description)
        {
            Title = title;
            Description = description;
        }
    }
}
