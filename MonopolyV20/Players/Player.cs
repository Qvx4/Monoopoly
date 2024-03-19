using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace MonopolyV20
{
    public class Player : User
    {
        public bool BankCheck { get; set; }
        public bool TaxCheck { get; set; }
        public Player(string name, char symbol, int balance, bool stepSkip, bool prison, bool bankCheck, bool taxCheck, int countCarBsn, int countGameBsn, int numberOfLaps) :
            base(name, symbol, balance, stepSkip, prison, countCarBsn, countGameBsn, numberOfLaps)
        {
            BankCheck = bankCheck;
            TaxCheck = taxCheck;
        }
        public override void Show()
        {
            //Сделать бордер с for
            Console.WriteLine($"Никнейм Игрока [ {Name} ]");
            Console.WriteLine($"Символ Игрока [ {Symbol} ]");
        }
        public bool IsCheckCellNotBsn(Building buildings)
        {
            if (buildings.GetType() == typeof(Tax) ||
                buildings.GetType() == typeof(Bank) ||
                buildings.GetType() == typeof(Chance))
            {
                return true;
            }
            return false;
        }//проверка что это не бизнес 
        public bool IsCheckCellBsn(Building buildings)
        {
            if (buildings.GetType() == typeof(Business) |
              buildings.GetType() == typeof(CarInterior) ||
              buildings.GetType() == typeof(GamingCompanies))
            {
                return true;
            }
            return false;
        }
        public bool IsCheckCellBy(Business business)//Проверка куплен ли бизнес 
        {
            if (business.BusinessOwner == '\0')
            {
                return true;
            }
            return false;
        }
        public bool IsCheckCellBy(CarInterior carInterior)//проверка куплен ли автоцентр
        {
            if (carInterior.BusinessOwner == '\0')
            {
                return true;
            }
            return false;
        }
        public bool IsCheckCellBy(GamingCompanies gamingCompanies)//проверка куплена ли игровая компания
        {
            if (gamingCompanies.BusinessOwner == '\0')
            {
                return true;
            }
            return false;
        }
        public bool IsCheckCellChance(Building buldings)
        {
            if (buldings.GetType() == typeof(Chance))
            {
                return true;
            }
            return false;
        }//проверка что ячейка шанс 
        public bool IsCheckChanceIsLesion(Chances chances)//проверка что шанс снятие денег
        {
            if (chances.GetType() == typeof(Lesion))
            {
                Console.WriteLine($"Игрок {Symbol} {((Lesion)chances).Description} сумма {((Lesion)chances).WriteOffMoney}");
                Thread.Sleep(2000);
                return true;
            }
            return false;
        }
        public bool IsCheckChanceIsTepeport(Chances chances)
        {
            if (chances.GetType() == typeof(RandomActions) && ((RandomActions)chances).Actions == Actions.Teleport)
            {
                return true;
            }
            return false;
        }
        public bool IsByCell(Building building, List<Building> buildings)
        {
            bool checkBalanc = true;
            if (building.GetType() == typeof(Business))
            {
                if (((Business)building).Price <= Balance)
                {
                    ((Business)building).BusinessOwner = Symbol;
                    Balance -= ((Business)building).Price;
                    Console.WriteLine($"Игрок {Symbol} покупает бизнес {building.Title} цена {((Business)building).Price}");
                    Thread.Sleep(2000);
                    checkBalanc = false;
                }
            }
            else if (building.GetType() == typeof(CarInterior))
            {
                if (((CarInterior)building).Price <= Balance)
                {
                    ((CarInterior)building).BusinessOwner = Symbol;
                    Balance -= ((CarInterior)building).Price;
                    for (int i = 0; i < buildings.Count; i++)
                    {
                        if (buildings[i].GetType() == typeof(CarInterior))
                        {
                            if (((CarInterior)buildings[i]).BusinessOwner == Symbol)
                            {
                                ((CarInterior)buildings[i]).Level = CountCarBsn;
                            }
                        }
                    }
                    CountCarBsn += 1;
                    Console.WriteLine($"Игрок {Symbol} покупает бизнес {building.Title} цена {((CarInterior)building).Price}");
                    Thread.Sleep(2000);
                    checkBalanc = false;
                }
            }
            else if (building.GetType() == typeof(GamingCompanies))
            {
                if (((GamingCompanies)building).Price <= Balance)
                {
                    ((GamingCompanies)building).BusinessOwner = Symbol;
                    Balance -= ((GamingCompanies)building).Price;
                    for (int i = 0; i < buildings.Count; i++)
                    {
                        if (buildings[i].GetType() == typeof(GamingCompanies))
                        {
                            if (((GamingCompanies)buildings[i]).BusinessOwner == Symbol)
                            {
                                ((GamingCompanies)buildings[i]).Level = CountGameBsn;
                            }
                        }
                    }
                    CountGameBsn += 1;
                    Console.WriteLine($"Игрок {Symbol} покупает бизнес {building.Title} цена {((GamingCompanies)building).Price}");
                    Thread.Sleep(2000);
                    checkBalanc = false;
                }
            }
            return checkBalanc;
        }//Покупка ячейки 
        public bool ChanceIsWork(Chances chances)
        {
            if (chances.GetType() == typeof(Lesion))
            {
                if (Balance >= ((Lesion)chances).WriteOffMoney)
                {
                    Balance -= ((Lesion)chances).WriteOffMoney;
                    return true;
                }
            }
            return false;
        }//шанс снятие денег
        public int PayRent(Building bulding, List<User> users, int firstCube, int secondCube)
        {
            int summa = 0;
            if (bulding.GetType() == typeof(Business))
            {
                if (((Business)bulding).Rent[((Business)bulding).Level] <= Balance)
                {
                    if (((Business)bulding).Mortgaged == false)
                    {
                        Balance -= ((Business)bulding).Rent[((Business)bulding).Level];
                        for (int i = 0; i < users.Count; i++)
                        {
                            if (((Business)bulding).BusinessOwner == users[i].Symbol)
                            {
                                users[i].Balance += ((Business)bulding).Rent[((Business)bulding).Level];
                                Console.WriteLine($"Игрок {Symbol} выплатил ренту игроку {users[i].Symbol} цена {((Business)bulding).Rent[((Business)bulding).Level]}");
                                Thread.Sleep(2000);
                                return 0;
                            }
                        }

                    }
                    else
                    {
                        return 1;
                    }

                }
            }
            else if (bulding.GetType() == typeof(CarInterior))
            {
                if (((CarInterior)bulding).Rent[((CarInterior)bulding).Level] <= Balance)
                {
                    if (((CarInterior)bulding).Mortgaged == false)
                    {
                        Balance -= ((CarInterior)bulding).Rent[((CarInterior)bulding).Level];
                        for (int i = 0; i < users.Count; i++)
                        {
                            if (((CarInterior)bulding).BusinessOwner == users[i].Symbol)
                            {
                                users[i].Balance += ((CarInterior)bulding).Rent[((CarInterior)bulding).Level];
                                Console.WriteLine($"Игрок {Symbol} выплатил ренту игроку {users[i].Symbol} цена {((CarInterior)bulding).Rent[((CarInterior)bulding).Level]}");
                                Thread.Sleep(2000);
                                return 0;
                            }
                        }
                    }
                    else
                    {
                        return 1;
                    }
                }
            }
            else if (bulding.GetType() == typeof(GamingCompanies))
            {
                summa = (firstCube + secondCube) * ((GamingCompanies)bulding).Rent[((GamingCompanies)bulding).Level];
                if (summa <= Balance)
                {
                    if (((GamingCompanies)bulding).Mortgaged == false)
                    {
                        Balance -= summa;
                        for (int i = 0; i < users.Count; i++)
                        {
                            if (((GamingCompanies)bulding).BusinessOwner == users[i].Symbol)
                            {
                                users[i].Balance += summa;
                                Console.WriteLine($"Игрок {Symbol} выплатил ренту игроку {users[i].Symbol} цена {summa}");
                                Thread.Sleep(2000);
                                return 0;
                            }
                        }
                    }
                    else
                    {
                        return 1;
                    }

                }
            }
            return -1;
        }//выплата ренты поля
        public bool CheckHaveBsn(Building building)
        {
            if (building.GetType() == typeof(Business))
            {
                if (((Business)building).BusinessOwner == Symbol)
                {
                    return false;
                }
            }
            else if (building.GetType() == typeof(CarInterior))
            {
                if (((CarInterior)building).BusinessOwner == Symbol)
                {
                    return false;
                }
            }
            else if (building.GetType() == typeof(GamingCompanies))
            {
                if (((GamingCompanies)building).BusinessOwner == Symbol)
                {
                    return false;
                }
            }
            return true;
        }//проверка если ли бизнес 
        public bool IsCehckByCell(Building bulding)
        {
            if (bulding.GetType() == typeof(Business))
            {
                return IsCheckCellBy((Business)bulding);
            }
            else if (bulding.GetType() == typeof(CarInterior))
            {
                return IsCheckCellBy((CarInterior)bulding);
            }
            else if (bulding.GetType() == typeof(GamingCompanies))
            {
                return IsCheckCellBy((GamingCompanies)bulding);
            }
            return false;
        }//Проверка ячейки киплина ли она 
        public bool LayACell(Building building, int index, List<Building> buildings)
        {
            if (building.GetType() == typeof(Business))
            {
                if (((Business)building).BusinessOwner == Symbol && ((Business)building).Number == index && !((Business)building).Mortgaged)
                {
                    if (((Business)building).Level > 0)
                    {
                        return true;
                    }
                    else
                    {
                        ((Business)building).Mortgaged = true;
                        Balance += ((Business)building).ValueOfCollaterel;
                        Console.WriteLine($"Игрок {Symbol} закладывает бизнес {building.Title} цена {((Business)building).ValueOfCollaterel}");
                        Thread.Sleep(2000);
                    }
                    return false;
                }
                return true;
            }
            if (building.GetType() == typeof(CarInterior))
            {
                if (((CarInterior)building).BusinessOwner == Symbol && ((CarInterior)building).Number == index && !((CarInterior)building).Mortgaged)
                {
                    ((CarInterior)building).Mortgaged = true;
                    Balance += ((CarInterior)building).ValueOfCollaterel;
                    //((Business)building).Level -= 1;
                    if (((CarInterior)building).Level > 0)
                    {
                        ((CarInterior)building).Level -= 1;
                    }
                    for (int j = 0; j < buildings.Count; j++)
                    {
                        if (buildings[j].GetType() == typeof(CarInterior))
                        {
                            if (buildings[j].Number != building.Number && ((Business)buildings[j]).BusinessOwner == Symbol)
                            {
                                do
                                {
                                    ((Business)buildings[j]).Level -= 1;
                                }
                                while (((Business)buildings[j]).Level < 0 && ((Business)building).Level < 0);

                            }
                        }
                    }
                    Console.WriteLine($"Игрок {Symbol} закладывает бизнес {building.Title} цена {((CarInterior)building).ValueOfCollaterel}");
                    Thread.Sleep(2000);
                    return false;
                }
                return true;
            }
            if (building.GetType() == typeof(GamingCompanies))
            {
                if (((GamingCompanies)building).BusinessOwner == Symbol && ((GamingCompanies)building).Number == index && !((GamingCompanies)building).Mortgaged)
                {
                    ((GamingCompanies)building).Mortgaged = true;
                    Balance += ((GamingCompanies)building).ValueOfCollaterel;
                    for (int j = 0; j < buildings.Count; j++)
                    {
                        if (buildings[j].GetType() == typeof(GamingCompanies))
                        {
                            if (buildings[j].Number != index && ((Business)buildings[j]).BusinessOwner == Symbol)
                            {
                                if (((Business)buildings[j]).Level > 0)
                                {
                                    ((Business)buildings[j]).Level -= 1;
                                }
                                else if (((Business)building).Level > 0)
                                {
                                    ((Business)building).Level -= 1;
                                }
                            }
                        }
                    }
                    Console.WriteLine($"Игрок {Symbol} закладывает бизнес {building.Title} цена {((GamingCompanies)building).ValueOfCollaterel}");
                    Thread.Sleep(2000);
                    return false;
                }
                return true;
            }
            return true;
        }//заложить бизнес игрока
        public int BsnBuyout(Building building, int index, List<Building> buildings)//выкуп своего бизнеса
        {
            int maxcount = 0;
            if (building.GetType() == typeof(Business))
            {
                if (Balance >= ((Business)building).RansomValue && ((Business)building).Number == index && ((Business)building).BusinessOwner == Symbol && ((Business)building).Mortgaged)
                {
                    ((Business)building).Mortgaged = false;
                    ((Business)building).BusinessDowntrun = 15;
                    Balance -= ((Business)building).RansomValue;
                    Console.WriteLine($"Игрок {Symbol} выкупает свой бизнес {building.Title} цена {((Business)building).RansomValue}");
                    Thread.Sleep(2000);
                    return 0;
                }
                return 1;
            }
            else if (building.GetType() == typeof(CarInterior))
            {
                if (Balance >= ((CarInterior)building).RansomValue &&
                    ((CarInterior)building).BusinessOwner == Symbol &&
                    ((CarInterior)building).Mortgaged)
                {
                    ((CarInterior)building).Mortgaged = false;
                    ((CarInterior)building).BusinessDowntrun = 15;
                    Balance -= ((CarInterior)building).RansomValue;
                    for (int j = 0; j < buildings.Count; j++)
                    {
                        if (buildings[j].GetType() == typeof(CarInterior))
                        {
                            if (/*buildings[j].Number != index &&*/ ((Business)buildings[j]).BusinessOwner == Symbol && !((Business)buildings[j]).Mortgaged)
                            {
                                maxcount += 1;
                            }
                        }
                    }
                    maxcount -= 1;
                    for (int j = 0; j < buildings.Count; j++)
                    {
                        if (buildings[j].GetType() == typeof(CarInterior))
                        {
                            if (/*buildings[j].Number != index &&*/ ((Business)buildings[j]).BusinessOwner == Symbol && !((Business)buildings[j]).Mortgaged)
                            {
                                ((Business)building).Level = maxcount;
                                ((Business)buildings[j]).Level = maxcount;
                            }
                        }
                    }
                    Console.WriteLine($"Игрок {Symbol} выкупает свой бизнес {building.Title} цена {((CarInterior)building).RansomValue}");
                    Thread.Sleep(2000);
                    return 0;
                }
                return 1;
            }
            else if (building.GetType() == typeof(GamingCompanies))
            {
                if (Balance >= ((GamingCompanies)building).RansomValue &&
                    ((GamingCompanies)building).BusinessOwner == Symbol &&
                    ((GamingCompanies)building).Mortgaged)
                {
                    ((GamingCompanies)building).Mortgaged = false;
                    ((GamingCompanies)building).BusinessDowntrun = 15;
                    Balance -= ((GamingCompanies)building).RansomValue;
                    for (int j = 0; j < buildings.Count; j++)
                    {
                        if (buildings[j].GetType() == typeof(GamingCompanies))
                        {
                            if (buildings[j].Number != index && ((Business)buildings[j]).BusinessOwner == Symbol && !((Business)buildings[j]).Mortgaged)
                            {
                                if (((Business)buildings[j]).Level < 1)
                                {
                                    ((Business)buildings[j]).Level += 1;
                                }
                                else if (((Business)building).Level < 0)
                                {
                                    ((Business)building).Level += 1;
                                }
                            }
                        }
                    }
                    Console.WriteLine($"Игрок {Symbol} выкупает свой бизнес {building.Title} цена {((GamingCompanies)building).RansomValue}");
                    Thread.Sleep(2000);
                    return 0;
                }
                return 1;
            }
            return -1;
        }
        public void Surrendered(Field field)
        {
            for (int i = 0; i < field.Buldings.Count; i++)
            {
                if (field.Buldings[i].GetType() == typeof(Business))
                {
                    if (((Business)field.Buldings[i]).BusinessOwner == Symbol)
                    {
                        ((Business)field.Buldings[i]).BusinessOwner = '\0';
                    }
                }
                else if (field.Buldings[i].GetType() == typeof(CarInterior))
                {
                    if (((CarInterior)field.Buldings[i]).BusinessOwner == Symbol)
                    {
                        ((CarInterior)field.Buldings[i]).BusinessOwner = '\0';
                    }
                }
                if (field.Buldings[i].GetType() == typeof(GamingCompanies))
                {
                    if (((GamingCompanies)field.Buldings[i]).BusinessOwner == Symbol)
                    {
                        ((GamingCompanies)field.Buldings[i]).BusinessOwner = '\0';
                    }
                }
            }
            Console.WriteLine($"Игрок {Symbol} решил сдатся и покинуть игру ");
            Thread.Sleep(2000);
        }//сдатся
        public bool IsMonopolyContains(Field field)
        {
            for (int i = 0; i < field.Buldings.Count; i++)
            {
                if (field.Buldings[i].GetType() == typeof(Business))
                {
                    if (((Business)field.Buldings[i]).BusinessOwner == Symbol)
                    {
                        bool isMonopoly = true;
                        for (int j = 0; j < field.Buldings.Count; j++)
                        {
                            if (((Business)field.Buldings[i]).BusinessType == ((Business)field.Buldings[j]).BusinessType &&
                                ((Business)field.Buldings[j]).BusinessOwner != Symbol)
                            {
                                isMonopoly = false;
                            }
                        }
                        if (isMonopoly)
                        {
                            return isMonopoly;
                        }
                        return isMonopoly;
                    }
                }
            }
            return false;
        }
        public List<Building> CreatMonopolyBsn(List<Building> buldings)//Доделать вывод монополии ( Ряд бизнесов которых можно улучшить )
        {
            List<Building> list = new List<Building>();
            for (int i = (int)BusinessType.Restaurants; i <= (int)BusinessType.GameCorparation; i++)
            {
                if (IsMonopolyByType((BusinessType)i, buldings))
                {
                    list.AddRange(buldings.Where(x => x.GetType() == typeof(Business)).
                        Where(x => ((Business)x).BusinessType == (BusinessType)i).ToList());
                }
            }
            return list;
        }
        public void ChanceCheck(Chances chances, Field field, List<User> users)
        {
            if (chances.GetType() == typeof(Profit))
            {
                Balance += ((Profit)chances).GettingMoney;
                Console.WriteLine($"Игрок {Symbol} {((Profit)chances).Description}");
                Thread.Sleep(2000);
            }
            #region Test
            //else if (chances.GetType() == typeof(Lesion))
            //{
            //    Balance -= ((Lesion)chances).WriteOffMoney;
            //    Console.WriteLine($"Игрок {Symbol} {((Lesion)chances).Description}");
            //    Thread.Sleep(2000);
            //}
            #endregion
            else if (chances.GetType() == typeof(RandomActions))
            {
                switch (((RandomActions)chances).Actions)
                {
                    case Actions.Teleport:
                        {
                            Random random = new Random();
                            int number = random.Next(0, 20);
                            field.Buldings[CordinationPlayer].Symbol.Remove(Symbol);
                            if ((CordinationPlayer += number) > field.Buldings.Count)
                            {
                                CordinationPlayer += number - field.Buldings.Count;
                                field.Buldings[CordinationPlayer].Symbol.Add(Symbol);
                                Console.WriteLine($"Игрок {Symbol} {((RandomActions)chances).Description}");
                                Thread.Sleep(2000);
                            }
                            else
                            {
                                CordinationPlayer += number;
                                field.Buldings[CordinationPlayer].Symbol.Add(Symbol);
                                Console.WriteLine($"Игрок {Symbol} {((RandomActions)chances).Description}");
                                Thread.Sleep(2000);
                            }
                        }
                        break;
                    case Actions.WalkBackWards:
                        {
                            ReverseStroke = true;
                            Console.WriteLine($"Игрок {Symbol} {((RandomActions)chances).Description}");
                            Thread.Sleep(2000);
                        }
                        break;
                    case Actions.GoToJail:
                        {
                            for (int i = 0; i < field.Buldings.Count; i++)
                            {
                                if (field.Buldings[i].GetType() == typeof(Prison))
                                {
                                    field.Buldings[CordinationPlayer].Symbol.Remove(Symbol);
                                    field.Buldings[i].Symbol.Add(Symbol);
                                    CordinationPlayer = field.Buldings[i].Number;
                                    Prison = true;
                                    Console.WriteLine($"Игрок {Symbol} {((RandomActions)chances).Description}");
                                    Thread.Sleep(2000);
                                }
                            }
                        }
                        break;
                    case Actions.Skipping:
                        {
                            StepSkip = true;
                            Console.WriteLine($"Игрок {Symbol} {((RandomActions)chances).Description}");
                            Thread.Sleep(2000);
                        }
                        break;
                    case Actions.Birthday:
                        {
                            Console.WriteLine($"У игрок {Symbol} {((RandomActions)chances).Description}");
                            Thread.Sleep(2000);
                            int priceBirthdayParty = 150;
                            for (int i = 0; i < users.Count; i++)
                            {
                                if (users[i].Balance > 150 && users[i].Symbol != Symbol && !users[i].Surrender)
                                {
                                    Console.WriteLine($"Игрок {users[i].Symbol} подарил игроку {Symbol} 150 ");
                                    Thread.Sleep(2000);
                                    users[i].Balance -= priceBirthdayParty;
                                    Balance += priceBirthdayParty;
                                }
                            }
                        }
                        break;
                    case Actions.Empty:
                        {
                            Console.WriteLine($"Игрок {Symbol} {((RandomActions)chances).Description}");
                            Thread.Sleep(2000);
                        }
                        break;
                }
            }
        }//анализ шанса 
        public bool IsMonopolyByType(BusinessType type, List<Building> building) //проверка типа моноплии
        {
            for (int i = 0; i < building.Count; i++)
            {
                if (building[i].GetType() == typeof(Business) &&
                    ((Business)building[i]).BusinessType == type &&
                    ((Business)building[i]).BusinessOwner != Symbol)
                {
                    return false;
                }
                else if (building[i].GetType() == typeof(CarInterior) &&
                    ((CarInterior)building[i]).BusinessType == type &&
                    ((CarInterior)building[i]).BusinessOwner != Symbol)
                {
                    return false;
                }
                else if (building[i].GetType() == typeof(GamingCompanies) &&
                    ((GamingCompanies)building[i]).BusinessType == type &&
                    ((GamingCompanies)building[i]).BusinessOwner != Symbol)
                {
                    return false;
                }
            }
            return true;
        }
        public bool IsHaveMeMonoopoly(List<Building> buldings)
        {
            for (int i = (int)BusinessType.Restaurants; i <= (int)BusinessType.GameCorparation; i++)
            {
                if (IsMonopolyByType((BusinessType)i, buldings)) return true;
            }
            return false;
        }//проверка есть ли хоть одна купленная монополия 
        public bool ReturnMonopolyTypes(BusinessType type, List<Building> buldings)
        {
            for (int i = 0;i < buldings.Count; i++)
            {
                if (buldings[i].GetType() == typeof(Business))
                {
                    if (((Business)buldings[i]).BusinessType == type)
                    {
                        if (((Business)buldings[i]).BusinessOwner != Symbol)
                        {
                            return false;
                        }
                    }
                }
                else if(type== BusinessType.Car || type == BusinessType.GameCorparation)
                {
                    return false;
                }
            }
            return true;
        }
        public List<Building> ShowBsn(List<Building> buldings,List<Building> Allbuildings) //переделать список бизнесов которые можно улучшить // поменять название метода 
        {
            int min = int.MaxValue, max = int.MinValue;
            List<Building> businesses = new List<Building>();
            List<BusinessType> businessTypes = new List<BusinessType>();
            for (int i = (int)BusinessType.Restaurants; i <= (int)BusinessType.GameCorparation; i++)
            {
                if (ReturnMonopolyTypes((BusinessType)i, Allbuildings))
                 {
                    businessTypes.Add((BusinessType)i);
                }
            }
            List<Building> monopolyBusiness = buldings;
            int maxLvl = 5;
            #region Test
            //for (int i = (int)BusinessType.Airlines; i <= (int)BusinessType.GameCorparation; i++)
            //{
            //    if (IsMonopolyByType((BusinessType)i, buldings))
            //    { // nen 
            //        monopolyBusiness = buldings.Where(x => x.GetType() == typeof(Business)).
            //            Where(x => ((Business)x).BusinessType == (BusinessType)i).ToList();
            //        break;
            //    }
            //}
            #endregion
            for (int i = 0; i < monopolyBusiness.Count; i++)
            {
                if (((Business)monopolyBusiness[i]).Level == maxLvl)
                {
                    monopolyBusiness.RemoveAt(i);
                }
            }
            #region Other
            //for (int i = 0; i < monopolyBusiness.Count; i++)
            //{
            //    if ((((Business)monopolyBusiness[i]).Level) > max)
            //    {
            //        max = ((Business)monopolyBusiness[i]).Level;
            //    }
            //    if ((((Business)monopolyBusiness[i]).Level) < min)
            //    {
            //        min = ((Business)monopolyBusiness[i]).Level;
            //    }
            //}
            //for (int i = monopolyBusiness.Count - 1; i >= 0; i--)
            //{
            //    if (max != min)
            //    {
            //        if (((Business)monopolyBusiness[i]).Level == max)
            //        {
            //            monopolyBusiness.RemoveAt(i);
            //        }
            //    }
            //}
            //for (int i = 0; i < buldings.Count; i++)
            //{
            //    if (buldings[i].GetType() == typeof(Business) && ((Business)buldings[i]).Mortgaged == true)
            //    {
            //        return new List<Building>();
            //    }
            //}
            //for (int i = monopolyBusiness.Count - 1; i >= 0; i--)
            //{
            //    if (((Business)monopolyBusiness[i]).Level == max)
            //    {
            //        monopolyBusiness.RemoveAt(i);
            //    }
            //}
            #endregion
            return monopolyBusiness;
        }//добовление бизнесов которых можно улучшить
        public void ShowUpdateBsn(List<Building> buldings)
        {
            for (int i = 0; i < buldings.Count; i++)
            {
                Console.WriteLine($"Название: {{{buldings[i].Title}}} Номер: {{{buldings[i].Number}}} цена: {{{((Business)buldings[i]).UpgradePrice}}}");
            }
        } //бизнесы которые можно улучшить 
        public void MonoopolyImprovement(Business business)
        {
            business.Level += 1;
            Balance -= business.UpgradePrice;
        }//улучшение бизнеса 
        public bool CheckCell(Building buldings, Field field)//проверк ячейки на которую попал игрок 
        {
            if (buldings.GetType() == typeof(Jackpot))
            {
                Jackpot = true;
            }//проверка что ячейка джекпот
            #region Test
            //else if (buldings.GetType() == typeof(Bank))
            //{
            //    if (Balance >= ((Bank)buldings).Summa)
            //    {
            //        Balance -= ((Bank)buldings).Summa;
            //        Console.WriteLine($"Игрок {Symbol} попал на банк и у него снимают списывают {((Bank)buldings).Summa}");
            //        Thread.Sleep(2000);
            //    }

            //}//проверка что ячейка налог
            //else if (buldings.GetType() == typeof(Tax))
            //{
            //    if (Balance >= ((Tax)buldings).Summa)
            //    {
            //        Balance -= ((Tax)buldings).Summa;
            //        Console.WriteLine($"Игрок {Symbol} вы попали на ячейку налог {((Tax)buldings).Summa}");
            //        Thread.Sleep(2000);
            //    }
            //}//проверка что ячейка налог на богадство 
            #endregion
            else if (buldings.GetType() == typeof(PoliceStation))
            {
                for (int i = 0; i < field.Buldings.Count; i++)
                {
                    if (field.Buldings[i].GetType() == typeof(Prison))
                    {
                        Console.WriteLine($"Игрок {Symbol} попал на клетку тюрьмы и по этому он отправляется в тюрьму");
                        Thread.Sleep(2000);
                        field.Buldings[CordinationPlayer].Symbol.Remove(Symbol);
                        field.Buldings[i].Symbol.Add(Symbol);
                        CordinationPlayer = field.Buldings[i].Number;
                        Prison = true;
                    }
                }
            }//проверка что ячейка полицейский участок
            else if (buldings.GetType() == typeof(Start))
            {
                Balance += ((Start)buldings).Summa;
                Console.WriteLine($"Игрок {Symbol} попал на старт и получает {((Start)buldings).Summa}");
                Thread.Sleep(2000);
            }//проверка что ячейка старт 
            #region Test
            //else if (buldings.GetType() == typeof(Chance))
            //{
            //    ((Chance)buldings).AddChance();
            //    Random random = new Random();
            //    ChanceAnalysis(((Chance)buldings).Chances[random.Next(0, ((Chance)buldings).Chances.Count)], field);
            //}//проверка что ячейка шанс //доделать боту шанс
            #endregion
            else if (buldings.GetType() == typeof(Prison))
            {
                Console.WriteLine($"Игрок {Symbol} попал на ячейку тюрьма в качестве прогулки");
                Thread.Sleep(2000);
            }//проверка если игрок попал в тюрьму на прогулке 
            return false;
        }
        public bool BusinessLiquidityCheck(List<Building> buldings, int index)//проверка на номер бизнеса
        {
            if (index == 0)
            {
                return true;
            }
            for (int i = 0; i < buldings.Count; i++)
            {
                if (buldings[i].Number == index)
                {
                    return false;
                }
            }
            return true;
        }
        public List<Business> SerchImporvedBsn(List<Building> buldings)
        {
            int min = int.MaxValue, max = int.MinValue;
            List<Business> result = new List<Business>();
            for (int i = 0; i < buldings.Count; i++)
            {
                if (buldings[i].GetType() == typeof(Business))
                {
                    if (((Business)buldings[i]).BusinessOwner == Symbol)
                    {
                        if (((Business)buldings[i]).Level > 0)
                        {
                            result.Add((Business)buldings[i]);
                        }
                    }
                }
            }
            for (int i = 0; i < result.Count; i++)
            {
                if ((((Business)result[i]).Level) > max)
                {
                    max = ((Business)result[i]).Level;
                }
                if ((((Business)result[i]).Level) < min)
                {
                    min = ((Business)result[i]).Level;
                }
            }
            for (int i = result.Count - 1; i >= 0; i--)
            {
                if (max != min)
                {
                    if (((Business)result[i]).Level == min)
                    {
                        result.RemoveAt(i);
                    }
                }
            }
            return result;
        }//поиск улучшеных бизнесов монополии
        public bool ShowImprovedBsn(List<Business> businesses)
        {
            for (int i = 0; i < businesses.Count; i++)
            {
                if (businesses[i].Mortgaged == true)
                {
                    return true;
                }
                Console.WriteLine($"Название: {businesses[i].Title} Номер: {businesses[i].Number} {((Business)businesses[i]).UpgradePrice}");
            }
            return false;
        }//вывод улучшеных бизнесов 
        public bool BranchSale(List<Business> businesses, int index)
        {
            for (int i = 0; i < businesses.Count; i++)
            {
                if (businesses[i].Number == index)
                {
                    businesses[i].Level--;
                    Balance += businesses[i].UpgradePrice;
                    return true;
                }
            }
            return true;
        } // продажа филиала 
        public bool GetBsnWithBranch(List<Building> buldings)
        {
            for (int i = 0; i < buldings.Count; i++)
            {
                if (buldings[i].GetType() == typeof(Business))
                {
                    if (((Business)buldings[i]).BusinessOwner == Symbol)
                    {
                        if (((Business)buldings[i]).Level > 0)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        } //проверка бизнесов на филиалы
        public bool CheckingBranchImproved(List<Building> buldings)
        {
            if (buldings.Count == 0)
            {
                return true;
            }
            for (int i = 0; i < buldings.Count; i++)
            {
                if (buldings[i].GetType() == typeof(Business) && ((Business)buldings[i]).Mortgaged == true)
                {
                    return true;
                }
            }
            return false;
        }
        public bool CheckingMonopolyCollected(BusinessType businessType, List<Building> buldings)
        {
            int countBsn = 0;
            for (int i = 0; i < buldings.Count; i++)
            {
                if (buldings[i].GetType() == typeof(Business))
                {
                    if (((Business)buldings[i]).BusinessType == businessType && ((Business)buldings[i]).BusinessOwner == Symbol)
                    {
                        countBsn++;
                    }
                }
            }
            if (countBsn == 3 || countBsn == 2 || countBsn == 1)
            {
                return true;
            }
            return false;
        }//проверка собрана ли монополия 
        public List<Building> GetAllBsn(List<Building> buldings)
        {
            BusinessType businessType;
            List<Building> result = new List<Building>();
            for (int i = 0; i < buldings.Count; i++)
            {
                if (buldings[i].GetType() == typeof(Business))
                {
                    if (((Business)buldings[i]).BusinessOwner == Symbol && ((Business)buldings[i]).Mortgaged != true)
                    {
                        result.Add(buldings[i]);
                    }
                }
                else if (buldings[i].GetType() == typeof(CarInterior))
                {
                    if (((CarInterior)buldings[i]).BusinessOwner == Symbol && ((CarInterior)buldings[i]).Mortgaged != true)
                    {
                        result.Add(buldings[i]);
                    }
                }
                else if (buldings[i].GetType() == typeof(GamingCompanies))
                {
                    if (((GamingCompanies)buldings[i]).BusinessOwner == Symbol && ((GamingCompanies)buldings[i]).Mortgaged != true)
                    {
                        result.Add(buldings[i]);
                    }
                }
            }
            for (int i = 0; i < result.Count; i++)
            {
                if (IsHaveMeMonoopoly(buldings))
                {
                    if (result[i].GetType() == typeof(Business))
                    {
                        if (CheckingMonopolyCollected(((Business)result[i]).BusinessType, result) && ((Business)result[i]).Level > 0)
                        {
                            businessType = ((Business)result[i]).BusinessType;
                            for (int j = result.Count - 1; j >= 0; j--)
                            {
                                if (result[j].GetType() == typeof(Business))
                                {
                                    if (((Business)result[j]).BusinessType == businessType)
                                    {
                                        result.Remove(result[j]);
                                    }
                                }
                            }
                        }

                    }
                }
            }
            return result;
        }//все бизнесы игрока
        public bool ShowALlBsn(List<Building> buldings)
        {
            if (buldings.Count == 0)
            {
                return true;
            }
            for (int i = 0; i < buldings.Count; i++)
            {
                Console.WriteLine($"номер {{{buldings[i].Number}}} название {{{buldings[i].Title}}}");

            }
            return false;
        }//вывод всех бизнесов которые можно заложить 
        public bool CheckingIsMortgaged(Building building)
        {
            if (((Business)building).Mortgaged == true)
            {
                return true;
            }
            return false;
        }//проверка что бизнес заложен у другого игрока 
    }
}
