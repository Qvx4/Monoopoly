using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonopolyV20
{
    public class CarInterior : Building
    {
        public int Price { get; set; }//Цена бизнеса 
        public int RansomValue { get; set; }//Стоимость выкупа 
        public int ValueOfCollaterel { get; set; }//Стоимость залога
        public int Level { get; set; }//Уровень бизнеса
        public List<int> Rent { get; set; }//Оренда ( сумма выплаты на попадание на поле )
        public char BusinessOwner { get; set; }//Владелец Автосалона
        public bool Mortgaged { get; set; } //заложен ли бизнес
        public BusinessType BusinessType { get; set; } // тип бизнеса 
        public CarInterior() { }
        public CarInterior(string title, int number,int price,int ransomValue,int valueOfCollaterel,int levl,List<int> renta,BusinessType businessType) : base(title, number)
        {
            Price = price;
            RansomValue = ransomValue;
            ValueOfCollaterel = valueOfCollaterel;
            Level = levl;
            Rent = renta;
            BusinessType = businessType;
        }
    }
}
