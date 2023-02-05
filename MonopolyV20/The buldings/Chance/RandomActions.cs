using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonopolyV20
{
    public class RandomActions : Chances
    {
        public Actions Actions { get; set; }
        public RandomActions(string title,string description, Actions actions) : base(title, description)
        {
            Actions = actions;  
        }
    }
}
