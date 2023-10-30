//using System;
using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

namespace MonopolyV20
{
    public class Business : Building
    {
        public int Price { get; set; }//Цена бизнеса 
        public int RansomValue { get; set; }//Стоимость выкупа 
        public int ValueOfCollaterel { get; set; }//Стоимость залога
        public int Level { get; set; }//Уровень бизнеса
        public List<int> Rent { get; set; }//Оренда ( сумма выплаты на попадание на поле )
        public int UpgradePrice { get; set; }//Стоимость покупки филиала 
        public char BusinessOwner { get; set; }//Владелец бизнеса
        public BusinessType BusinessType { get; set; } // тип бизнеса 
        public bool Mortgaged { get; set; } //заложен ли бизнес

        public Business()
        {
            Price = 0;
            RansomValue = 0;
            ValueOfCollaterel = 0;
            Level = 0;
            Rent = new List<int>();
            UpgradePrice = 0;
            BusinessOwner = ' ';
            BusinessType = new BusinessType();
            Mortgaged = false;
        }



        public Business(string title, int number, int price, int ransomValue, int valueOfCallaterel, int level, int upgradeprise, List<int> renta, BusinessType businessType) : base(title, number)
        {
            Price = price;
            RansomValue = ransomValue;
            ValueOfCollaterel = valueOfCallaterel;
            Level = level;
            Rent = renta;
            UpgradePrice = upgradeprise;
            BusinessType = businessType;
        }
    }
}
