using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonopolyV20
{
    public class CarInterior : Business
    {
        public CarInterior() : base() { }
        public CarInterior(string title, int number,int price,int ransomValue,int valueOfCollaterel,int levl,List<int> renta,BusinessType businessType) 
            : base(title, number, price, ransomValue, valueOfCollaterel, levl, 0, renta, businessType)
        {

        }
    }
}
