using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;

namespace MonopolyV20
{
    public class Player : User
    {
        public Player(string name, char symbol, int balance, bool stepSkip, bool prison) : base(name, symbol, balance, stepSkip, prison)
        {

        }
        public override void Show()
        {
            //Сделать бордер с for
            Console.WriteLine($"Никнейм Игрока [ {Name} ]");
            Console.WriteLine($"Символ Игрока [ {Symbol} ]");
        }
        public bool IsCheckCellNotBis(Building buildings)
        {
            if (buildings.GetType() == typeof(Business))
            {
                return false;
            }
            else if (buildings.GetType() == typeof(CarInterior))
            {
                return false;
            }
            else if (buildings.GetType() == typeof(GamingCompanies))
            {
                return false;
            }
            return true;
        }
        public bool IsCheckCellBy(Business business)//Проверка куплен ли бизнес 
        {
            if (business.BusinessOwner != 0)
            {
                return true;
            }
            return false;
        }
        public bool IsCheckCellBy(CarInterior carInterior)//проверка куплен ли автоцентр
        {
            if (carInterior.BusinessOwner != 0)
            {
                return true;
            }
            return false;
        }
        public bool IsCheckCellBy(GamingCompanies gamingCompanies)//проверка куплена ли игровая компания
        {
            if (gamingCompanies.BusinessOwner != 0)
            {
                return true;
            }
            return false;
        }
        public void IsByCell(Building building)
        {
            if (building.GetType() == typeof(Business))
            {
                if (((Business)building).Price <= Balance)
                {
                    ((Business)building).BusinessOwner = Symbol;
                    Balance -= ((Business)building).Price;
                    Console.WriteLine($"Игрок {Symbol} покупает бизнес {building.Title} цена {((Business)building).Price}");
                    Thread.Sleep(2000);
                }
            }
            else if (building.GetType() == typeof(CarInterior))
            {
                if (((CarInterior)building).Price <= Balance)
                {
                    ((CarInterior)building).BusinessOwner = Symbol;
                    Balance -= ((CarInterior)building).Price;
                    Console.WriteLine($"Игрок {Symbol} покупает бизнес {building.Title} цена {((CarInterior)building).Price}");
                    Thread.Sleep(2000);
                }
            }
            else if (building.GetType() == typeof(GamingCompanies))
            {
                if (((GamingCompanies)building).Price <= Balance)
                {
                    ((GamingCompanies)building).BusinessOwner = Symbol;
                    Balance -= ((GamingCompanies)building).Price;
                    Console.WriteLine($"Игрок {Symbol} покупает бизнес {building.Title} цена {((GamingCompanies)building).Price}");
                    Thread.Sleep(2000);
                }
            }
        }//Покупка ячейки 
        public void PayRent(Building bulding, List<User> users)
        {
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
                            }
                        }

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
                            }
                        }
                    }
                }
            }
            else if (bulding.GetType() == typeof(GamingCompanies))
            {
                if (((GamingCompanies)bulding).Rent[((GamingCompanies)bulding).Level] <= Balance)
                {
                    if (((GamingCompanies)bulding).Mortgaged == false)
                    {
                        Balance -= ((GamingCompanies)bulding).Rent[((GamingCompanies)bulding).Level];
                        for (int i = 0; i < users.Count; i++)
                        {
                            if (((GamingCompanies)bulding).BusinessOwner == users[i].Symbol)
                            {
                                users[i].Balance += ((GamingCompanies)bulding).Rent[((GamingCompanies)bulding).Level];
                                Console.WriteLine($"Игрок {Symbol} выплатил ренту игроку {users[i].Symbol} цена {((GamingCompanies)bulding).Rent[((GamingCompanies)bulding).Level]}");
                                Thread.Sleep(2000);
                            }
                        }
                    }
                }
            }
        }//выплата ренты поля 
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
        public bool LayACell(Building building)
        {
            if (building.GetType() == typeof(Business))
            {
                if (((Business)building).BusinessOwner == Symbol)
                {
                    ((Business)building).Mortgaged = true;
                    Balance += ((Business)building).ValueOfCollaterel;
                    Console.WriteLine($"Игрок {Symbol} закладывает бизнес {building.Title} цена {((Business)building).ValueOfCollaterel}");
                    Thread.Sleep(2000);
                    return true;
                }
            }
            if (building.GetType() == typeof(CarInterior))
            {   
                if (((CarInterior)building).BusinessOwner == Symbol)
                {
                    ((CarInterior)building).Mortgaged = true;
                    Balance += ((CarInterior)building).ValueOfCollaterel;
                    Console.WriteLine($"Игрок {Symbol} закладывает бизнес {building.Title} цена {((CarInterior)building).ValueOfCollaterel}");
                    Thread.Sleep(2000);
                    return true;
                }
            }
            if (building.GetType() == typeof(GamingCompanies))
            {               
                if (((GamingCompanies)building).BusinessOwner == Symbol)
                {
                    ((GamingCompanies)building).Mortgaged = true;
                    Balance += ((GamingCompanies)building).ValueOfCollaterel;
                    Console.WriteLine($"Игрок {Symbol} закладывает бизнес {building.Title} цена {((GamingCompanies)building).ValueOfCollaterel}");
                    Thread.Sleep(2000);
                    return true;
                }
            }
            return false;
        }//заложить бизнес игрока
        public bool BsnBuyout(Building building)//выкуп своего бизнеса
        {
            if (building.GetType() == typeof(Business))
            {
                if (Balance >= ((Business)building).RansomValue)
                {
                    ((Business)building).Mortgaged = false;
                    Balance -= ((Business)building).RansomValue;
                    Console.WriteLine($"Игрок {Symbol} выкупает свой бизнес {building.Title} цена {((Business)building).RansomValue}");
                    Thread.Sleep(2000);
                    return true;
                }
            }
            else if (building.GetType() == typeof(CarInterior))
            {
                if (Balance >= ((CarInterior)building).RansomValue)
                {
                    ((CarInterior)building).Mortgaged = false;
                    Balance -= ((CarInterior)building).RansomValue;
                    Console.WriteLine($"Игрок {Symbol} выкупает свой бизнес {building.Title} цена {((CarInterior)building).RansomValue}");
                    Thread.Sleep(2000);
                    return true;
                }
            }
            else if (building.GetType() == typeof(GamingCompanies))
            {
                if (Balance >= ((GamingCompanies)building).RansomValue)
                {
                    ((GamingCompanies)building).Mortgaged = false;
                    Balance -= ((GamingCompanies)building).RansomValue;
                    Console.WriteLine($"Игрок {Symbol} выкупает свой бизнес {building.Title} цена {((GamingCompanies)building).RansomValue}");
                    Thread.Sleep(2000);
                    return true;
                }
            }
            return false;
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
                Console.WriteLine($"Игрок {Symbol} решил сдатся и покинуть игру ");
                Thread.Sleep(2000);
            }
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
        //public void ShowMonopolyBsn(Field field)//Доделать вывод монополии ( Ряд бизнесов которых можно улучшить )
        //{
        //    List<Business> list = new List<Business>();
        //    for (int i = 0; i < field.Buldings.Count; i++)
        //    {
        //        if (field.Buldings[i].GetType() == typeof(Business))
        //        {
        //            if (((Business)field.Buldings[i]).BusinessOwner == Symbol)
        //            {
        //                for (int j = 0; j < field.Buldings.Count; j++)
        //                {
        //                    if (((Business)field.Buldings[i]).BusinessType == ((Business)field.Buldings[j]).BusinessType &&
        //                        ((Business)field.Buldings[j]).BusinessOwner == Symbol)
        //                    {

        //                    }
        //                }
        //            }
        //        }
        //    }

        //}
        public void ChanceAnalysis(Chances chances,Field field)
        {
            if (chances.GetType() == typeof(Profit))
            {
                Balance += ((Profit)chances).GettingMoney;
            }
            else if (chances.GetType() == typeof(Lesion))
            {
                Balance -= ((Lesion)chances).WriteOffMoney;
            }
            else if (chances.GetType() == typeof(RandomActions))
            {
                switch (((RandomActions)chances).Actions)
                {
                    case Actions.Teleport:
                        {
                            field.Buldings[CordinationPlayer].Symbol.Remove(Symbol);
                            Random random = new Random();
                            CordinationPlayer += random.Next(0, 20);
                            field.Buldings[CordinationPlayer].Symbol.Add(Symbol);
                        }
                        break;
                    case Actions.WalkBackWards:
                        {
                            ReverseStroke = true;
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
                                }
                            }
                        }
                        break;
                    case Actions.Skipping:
                        {
                            StepSkip = true;
                        }
                        break;
                    case Actions.Empty:
                        {

                        }
                        break;
                }
            }
        }//анализ шанса 
        public bool IsMonopolyByType(BusinessType type, List<Building> building)
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
            for (int i = (int)BusinessType.Airlines; i <= (int)BusinessType.GameCorparation; i++)
            {
                if (IsMonopolyByType((BusinessType)i, buldings)) return true;
            }
            return false;
        }//проверка есть ли хоть одна купленная монополия 
        public void ShowBsn(List<Building> buldings) //переделать список бизнесов которые можно улучшить 
        {
            int min = 0, max = 0;
            int index = 0;
            List<Building> monopolyBusiness = buldings;
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
                for (int j = 0; j < monopolyBusiness.Count; j++)
                {
                    if (((Business)monopolyBusiness[i]).Level > ((Business)monopolyBusiness[j]).Level)
                    {
                        max = ((Business)monopolyBusiness[i]).Level;
                    }
                    else
                    {
                        min = ((Business)monopolyBusiness[j]).Level;
                    }
                }
            }
            for (int i = 0; i < monopolyBusiness.Count; i++)
            {
                if (max != min)
                {
                    if (((Business)monopolyBusiness[index]).Level == max)
                    {
                        monopolyBusiness.RemoveAt(index);
                        index = 0;
                    }
                    else
                    {
                        index++;
                    }
                }
            }
            for (int i = 0; i < monopolyBusiness.Count; i++)
            {
                Console.WriteLine($"Название: {monopolyBusiness[i].Title} Номер: {monopolyBusiness[i].Number} {((Business)monopolyBusiness[i]).UpgradePrice}");
            }
        }//добовление бизнесов которых можно улучшить 
        public void MonoopolyImprovement(Business business)
        {
            business.Level += 1;
            Balance -= business.UpgradePrice;
        }//улучшение бизнеса 
        public bool CheckCell(Building buldings, Field field)//проверк ячейки на которую попал бот 
        {
            if (buldings.GetType() == typeof(Jackpot))
            {
                Jackpot = true;
            }//проверка что ячейка джекпот //доделать боту логику джекпота
            else if (buldings.GetType() == typeof(Bank))
            {
                if (Balance >= ((Bank)buldings).Summa)
                {
                    Balance -= ((Bank)buldings).Summa;
                }

            }//проверка что ячейка налог
            else if (buldings.GetType() == typeof(Tax))
            {
                if (Balance >= ((Tax)buldings).Summa)
                {
                    Balance -= ((Tax)buldings).Summa;
                }
            }//проверка что ячейка налог на богадство 
            else if (buldings.GetType() == typeof(PoliceStation))
            {
                for (int i = 0; i < field.Buldings.Count; i++)
                {
                    if (field.Buldings[i].GetType() == typeof(Prison))
                    {
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
            }//проверка что ячейка старт 
            else if (buldings.GetType() == typeof(Chance))
            {
                ((Chance)buldings).AddChance();
                Random random = new Random();
                ChanceAnalysis(((Chance)buldings).Chances[random.Next(0, ((Chance)buldings).Chances.Count)],field);

            }//проверка что ячейка шанс //доделать боту шанс 
            return false;
        }
    }
}
