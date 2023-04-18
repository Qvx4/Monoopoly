using System;
using System.Collections.Generic;
using System.Linq;
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
        public bool LayACell(Building building,int index)
        {
            if (building.GetType() == typeof(Business))
            {
                if (((Business)building).BusinessOwner == Symbol && ((Business)building).Number == index)
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
            }
            if (building.GetType() == typeof(CarInterior))
            {
                if (((CarInterior)building).BusinessOwner == Symbol && ((CarInterior)building).Number == index)
                {
                    ((CarInterior)building).Mortgaged = true;
                    Balance += ((CarInterior)building).ValueOfCollaterel;
                    Console.WriteLine($"Игрок {Symbol} закладывает бизнес {building.Title} цена {((CarInterior)building).ValueOfCollaterel}");
                    Thread.Sleep(2000);
                    return false;
                }
            }
            if (building.GetType() == typeof(GamingCompanies))
            {
                if (((GamingCompanies)building).BusinessOwner == Symbol && ((GamingCompanies)building).Number == index)
                {
                    ((GamingCompanies)building).Mortgaged = true;
                    Balance += ((GamingCompanies)building).ValueOfCollaterel;
                    Console.WriteLine($"Игрок {Symbol} закладывает бизнес {building.Title} цена {((GamingCompanies)building).ValueOfCollaterel}");
                    Thread.Sleep(2000);
                    return false;
                }
            }
            return true;
        }//заложить бизнес игрока
        public bool BsnBuyout(Building building,int index)//выкуп своего бизнеса
        {
            if (building.GetType() == typeof(Business))
            {
                if (Balance >= ((Business)building).RansomValue && ((Business)building).Number == index && ((Business)building).BusinessOwner == Symbol)
                {
                    ((Business)building).Mortgaged = false;
                    Balance -= ((Business)building).RansomValue;
                    Console.WriteLine($"Игрок {Symbol} выкупает свой бизнес {building.Title} цена {((Business)building).RansomValue}");
                    Thread.Sleep(2000);
                    return false;
                }
            }
            else if (building.GetType() == typeof(CarInterior))
            {
                if (Balance >= ((CarInterior)building).RansomValue && ((CarInterior)building).Number == index && ((CarInterior)building).BusinessOwner == Symbol)
                {
                    ((CarInterior)building).Mortgaged = false;
                    Balance -= ((CarInterior)building).RansomValue;
                    Console.WriteLine($"Игрок {Symbol} выкупает свой бизнес {building.Title} цена {((CarInterior)building).RansomValue}");
                    Thread.Sleep(2000);
                    return false;
                }
            }
            else if (building.GetType() == typeof(GamingCompanies))
            {
                if (Balance >= ((GamingCompanies)building).RansomValue && ((GamingCompanies)building).Number == index && ((GamingCompanies)building).BusinessOwner == Symbol)
                {
                    ((GamingCompanies)building).Mortgaged = false;
                    Balance -= ((GamingCompanies)building).RansomValue;
                    Console.WriteLine($"Игрок {Symbol} выкупает свой бизнес {building.Title} цена {((GamingCompanies)building).RansomValue}");
                    Thread.Sleep(2000);
                    return false;
                }
            }
            return true;
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
        public List<Building> ShowMonopolyBsn(List<Building> buldings)//Доделать вывод монополии ( Ряд бизнесов которых можно улучшить )
        {
            List<Building> list = new List<Building>();
            for (int i = (int)BusinessType.Airlines; i <= (int)BusinessType.GameCorparation; i++)
            {
                if (IsMonopolyByType((BusinessType)i, buldings))
                {
                    list = buldings.Where(x => x.GetType() == typeof(Business)).
                        Where(x => ((Business)x).BusinessType == (BusinessType)i).ToList();
                    break;
                }
            }
            return list;
        }
        public void ChanceAnalysis(Chances chances, Field field)
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
        public List<Building> ShowBsn(List<Building> buldings) //переделать список бизнесов которые можно улучшить 
        {
            int min = int.MaxValue, max = int.MinValue;
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
                if ((((Business)monopolyBusiness[i]).Level) > max)
                {
                    max = ((Business)monopolyBusiness[i]).Level;
                }
                if ((((Business)monopolyBusiness[i]).Level) < min)
                {
                    min = ((Business)monopolyBusiness[i]).Level;
                }
            }
            for (int i = monopolyBusiness.Count - 1; i >= 0; i--)
            {
                if (max != min)
                {
                    if (((Business)monopolyBusiness[i]).Level == max)
                    {
                        monopolyBusiness.RemoveAt(i);
                    }
                }
            }
            for (int i = 0; i < buldings.Count; i++)
            {
                Console.WriteLine($"Название: {monopolyBusiness[i].Title} Номер: {monopolyBusiness[i].Number} {((Business)monopolyBusiness[i]).UpgradePrice}");
            }
            return monopolyBusiness;
        }//добовление бизнесов которых можно улучшить 
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
                ChanceAnalysis(((Chance)buldings).Chances[random.Next(0, ((Chance)buldings).Chances.Count)], field);

            }//проверка что ячейка шанс //доделать боту шанс 
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
                    if (((Business)result[i]).Level == max)
                    {
                        result.RemoveAt(i);
                    }
                }
            }
            return result;
        }//поиск улучшеных бизнесов монополии
        public void ShowImprovedBsn(List<Business> businesses)
        {
            #region Test
            //int min = int.MaxValue, max = int.MinValue;
            //for (int i = 0; i < businesses.Count; i++)
            //{
            //    if ((((Business)businesses[i]).Level) > max)
            //    {
            //        max = ((Business)businesses[i]).Level;
            //    }
            //    if ((((Business)businesses[i]).Level) < min)
            //    {
            //        min = ((Business)businesses[i]).Level;
            //    }
            //}
            //for (int i = businesses.Count - 1; i >= 0; i--)
            //{
            //    if (max != min)
            //    {
            //        if (((Business)businesses[i]).Level == max)
            //        {
            //            businesses.RemoveAt(i);
            //        }
            //    }
            //}
            #endregion
            for (int i = 0; i < businesses.Count; i++)
            {
                Console.WriteLine($"Название: {businesses[i].Title} Номер: {businesses[i].Number} {((Business)businesses[i]).UpgradePrice}");
            }
        }//вывод улучшеных бизнесов 
        public bool BranchSale(List<Business> businesses,int index)
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
        }
    }
}
