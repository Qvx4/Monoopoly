﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

namespace MonopolyV20
{
    public class Bot : User
    {
        public bool Auction { get; set; }
        public Bot(string name, char symbol, int balance, bool stepSkip, bool prison, int countCarBsn, int countGameBsn, int numberOfLaps) : base(name, symbol, balance, stepSkip, prison, countCarBsn, countGameBsn, numberOfLaps)
        {

        }
        public override void Show()
        {
            //Сделать бордер с for
            Console.WriteLine($"Никнейм Бота [ {Name} ]");
            Console.WriteLine($"Символ Бота [ {Symbol} ]");
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
        public bool IsCheckBsnHaveBot(Building building)
        {
            if (building.GetType() == typeof(Business))
            {
                if (((Business)building).BusinessOwner == Symbol)
                {
                    return true;
                }
            }
            else if (building.GetType() == typeof(CarInterior))
            {
                if (((CarInterior)building).BusinessOwner == Symbol)
                {
                    return true;
                }
            }
            else if (building.GetType() == typeof(GamingCompanies))
            {
                if (((GamingCompanies)building).BusinessOwner == Symbol)
                {
                    return true;
                }
            }
            return false;
        }//проверка бизнеса что он куплен ботом 
        public bool IsCheckBsnTypeIsMonoopoly(BusinessType businessType, List<Building> buildings)
        {
            int bsnTypeCount = 0;
            for (int i = 0; i < buildings.Count; i++)
            {
                if (bsnTypeCount > 0)
                {
                    return true;
                }
                if (buildings[i].GetType() == typeof(Business))
                {
                    if (((Business)buildings[i]).BusinessOwner == Symbol)
                    {
                        if (((Business)buildings[i]).BusinessType == businessType && ((Business)buildings[i]).Level > 0)
                        {
                            bsnTypeCount += 1;
                        }
                    }
                }
            }
            return false;
        }
        public bool IsCheckMonoopollyLvl(List<Building> buildings)
        {
            bool checkLvlTry = true;
            List<Business> businessList = new List<Business>();
            for (int i = 0; i < buildings.Count; i++)
            {
                if (buildings[i].GetType() == typeof(Business))
                {
                    if (((Business)buildings[i]).BusinessOwner == Symbol)
                    {
                        if (IsCheckBsnTypeIsMonoopoly(((Business)buildings[i]).BusinessType, buildings))
                        {
                            businessList.Add(((Business)buildings[i]));
                        }
                    }
                }
            }
            for (int i = 0; i < businessList.Count; i++)
            {
                if (businessList[i].Level < 5)
                {
                    checkLvlTry = false;
                }
            }
            return checkLvlTry;
        }//проверка уровня монополии
        public bool IsByCell(Building building, List<Building> buildings)
        {
            if (building.GetType() == typeof(Business))
            {
                if (((Business)building).Price < Balance)
                {
                    ((Business)building).BusinessOwner = Symbol;
                    Balance -= ((Business)building).Price;
                    Console.WriteLine($"Игрок {Symbol} покупает бизнес {building.Title} цена {((Business)building).Price}");
                    Thread.Sleep(2000);
                    return true;
                }
            }
            else if (building.GetType() == typeof(CarInterior))
            {
                if (((CarInterior)building).Price < Balance)
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
                    return true;
                }
            }
            else if (building.GetType() == typeof(GamingCompanies))
            {
                if (((GamingCompanies)building).Price < Balance)
                {
                    ((GamingCompanies)building).BusinessOwner = Symbol;
                    Balance -= ((GamingCompanies)building).Price;
                    for (int i = 0; i < buildings.Count; i++)
                    {
                        if (buildings[i].GetType() == typeof(CarInterior))
                        {
                            if (((CarInterior)buildings[i]).BusinessOwner == Symbol)
                            {
                                ((CarInterior)buildings[i]).Level = CountGameBsn;
                            }
                        }
                    }
                    CountGameBsn += 1;
                    Console.WriteLine($"Игрок {Symbol} покупает бизнес {building.Title} цена {((GamingCompanies)building).Price}");
                    Thread.Sleep(2000);
                    return true;
                }
            }
            return false;
        }//Покупка ячейки 
        public void PayRent(Building bulding, List<User> user, Field field)
        {
            if (bulding.GetType() == typeof(Business))
            {
                if (((Business)bulding).Rent[((Business)bulding).Level] < Balance)
                {
                    if (((Business)bulding).Mortgaged == false)
                    {
                        Balance -= ((Business)bulding).Rent[((Business)bulding).Level];
                        for (int i = 0; i < user.Count; i++)
                        {
                            if (((Business)bulding).BusinessOwner == user[i].Symbol)
                            {
                                user[i].Balance += ((Business)bulding).Rent[((Business)bulding).Level];
                                Console.WriteLine($"Игрок {Symbol} выплатил ренту игроку {user[i].Symbol} цена {((Business)bulding).Rent[((Business)bulding).Level]}");
                                Thread.Sleep(2000);
                                return;
                            }
                        }
                    }
                }
                else
                {
                    if (CheckBsnAllMortagaged(BotBusinesses(field.Buldings)))
                    {
                        SurrenderLogic(field.Buldings);
                        return;
                    }
                    MortagageBusiness(BotBusinesses(field.Buldings), user, field.Buldings);
                }
            }
            else if (bulding.GetType() == typeof(CarInterior))
            {
                if (((CarInterior)bulding).Rent[((CarInterior)bulding).Level] < Balance)
                {
                    if (((CarInterior)bulding).Mortgaged == false)
                    {
                        Balance -= ((CarInterior)bulding).Rent[((CarInterior)bulding).Level];
                        for (int i = 0; i < user.Count; i++)
                        {
                            if (((CarInterior)bulding).BusinessOwner == user[i].Symbol)
                            {
                                user[i].Balance += ((CarInterior)bulding).Rent[((CarInterior)bulding).Level];
                                Console.WriteLine($"Игрок {Symbol} выплатил ренту игроку {user[i].Symbol} цена {((CarInterior)bulding).Rent[((CarInterior)bulding).Level]}");
                                Thread.Sleep(2000);
                                return;
                            }
                        }
                    }
                }
                else
                {
                    if (CheckBsnAllMortagaged(BotBusinesses(field.Buldings)))
                    {
                        SurrenderLogic(field.Buldings);
                        return;
                    }
                    MortagageBusiness(BotBusinesses(field.Buldings), user, field.Buldings);
                }
            }
            else if (bulding.GetType() == typeof(GamingCompanies))
            {
                if (((GamingCompanies)bulding).Rent[((GamingCompanies)bulding).Level] < Balance)
                {
                    if (((GamingCompanies)bulding).Mortgaged == false)
                    {
                        Balance -= ((GamingCompanies)bulding).Rent[((GamingCompanies)bulding).Level];
                        for (int i = 0; i < user.Count; i++)
                        {
                            if (((GamingCompanies)bulding).BusinessOwner == user[i].Symbol)
                            {
                                user[i].Balance += ((GamingCompanies)bulding).Rent[((GamingCompanies)bulding).Level];
                                Console.WriteLine($"Игрок {Symbol} выплатил ренту игроку {user[i].Symbol} цена {((GamingCompanies)bulding).Rent[((GamingCompanies)bulding).Level]}");
                                Thread.Sleep(2000);
                                return;
                            }
                        }
                    }
                }
                else
                {
                    if (CheckBsnAllMortagaged(BotBusinesses(field.Buldings)))
                    {
                        SurrenderLogic(field.Buldings);
                        return;
                    }
                    MortagageBusiness(BotBusinesses(field.Buldings), user, field.Buldings);
                }
            }
        }//выплата ренты поля 
        public void ChanceAnalysis(Chances chances, Field field, List<User> users)
        {
            if (chances.GetType() == typeof(Profit))
            {
                Balance += ((Profit)chances).GettingMoney;
                Console.WriteLine($"Игроку {Symbol} выпал шанс {((Profit)chances).Title} {((Profit)chances).Description}");
                Thread.Sleep(2000);
            }
            else if (chances.GetType() == typeof(Lesion))
            {
                if (Balance > ((Lesion)chances).WriteOffMoney)
                {
                    Balance -= ((Lesion)chances).WriteOffMoney;
                    Console.WriteLine($"Игроку {Symbol} выпал шанс {((Lesion)chances).Title} {((Lesion)chances).Description}");
                    Thread.Sleep(2000);
                }
                else
                {
                    MortagageBusiness(BotBusinesses(field.Buldings), users, field.Buldings);
                }
            }
            else if (chances.GetType() == typeof(RandomActions))
            {
                switch (((RandomActions)chances).Actions)
                {
                    case Actions.Teleport:
                        {
                            field.Buldings[CordinationPlayer].Symbol.Remove(Symbol);
                            Random random = new Random();
                            int number = random.Next(1, 15);
                            if (CordinationPlayer + number >= field.Buldings.Count)
                            {
                                CordinationPlayer += number - field.Buldings.Count;
                            }
                            else
                            {
                                CordinationPlayer += number;
                            }
                            field.Buldings[CordinationPlayer].Symbol.Add(Symbol);
                            CheckCell(field.Buldings[CordinationPlayer], users, field);
                            Console.WriteLine($"Игроку {Symbol} выпал шанс {((RandomActions)chances).Title} {((RandomActions)chances).Description} телепорт на {number}");
                            Thread.Sleep(2000);
                        }
                        break;
                    case Actions.WalkBackWards:
                        {
                            ReverseStroke = true;
                            Console.WriteLine($"Игроку {Symbol} выпал шанс {((RandomActions)chances).Title} {((RandomActions)chances).Description} ");
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
                                    Console.WriteLine($"Игроку {Symbol} выпал шанс {((RandomActions)chances).Title} {((RandomActions)chances).Description}");
                                    Thread.Sleep(2000);
                                }
                            }
                        }
                        break;
                    case Actions.Skipping:
                        {
                            StepSkip = true;
                            Console.WriteLine($"Игроку {Symbol} выпал шанс {((RandomActions)chances).Title} {((RandomActions)chances).Description}");
                            Thread.Sleep(2000);
                        }
                        break;
                    case Actions.Birthday:
                        {
                            int birthdayPrice = 150;
                            int summa = 0;
                            for (int i = 0; i < users.Count; i++)
                            {
                                if (users[i].Symbol != Symbol)
                                {
                                    if (users[i].Balance >= birthdayPrice)
                                    {
                                        users[i].Balance -= birthdayPrice;
                                        summa += birthdayPrice;
                                    }
                                }
                            }
                            Balance += summa;
                            Console.WriteLine($"Игроку {Symbol} выпал шанс {((RandomActions)chances).Title} {((RandomActions)chances).Description}");
                            Thread.Sleep(2000);

                        }
                        break;
                    case Actions.Empty:
                        {
                            Console.WriteLine($"Игроку {Symbol} выпал шанс {((RandomActions)chances).Title} {((RandomActions)chances).Description}");
                            Thread.Sleep(2000);
                        }
                        break;
                }
            }
        }//выпадения рандомного шанса //
        public bool IsHaveBusinessThisType(BusinessType businessType, List<Building> buldings)
        {
            for (int i = 0; i < buldings.Count; i++)
            {
                if (buldings[i].GetType() == typeof(Business))
                {
                    if (((Business)buldings[i]).BusinessOwner == Symbol)
                    {
                        if (((Business)buldings[i]).BusinessType == businessType)
                        {
                            return true;
                        }
                    }
                }
                else if (buldings[i].GetType() == typeof(CarInterior))
                {
                    if (((CarInterior)buldings[i]).BusinessOwner == Symbol)
                    {
                        if (((CarInterior)buldings[i]).BusinessType == businessType)
                        {
                            return true;
                        }
                    }
                }
                else if (buldings[i].GetType() == typeof(GamingCompanies))
                {
                    if (((GamingCompanies)buldings[i]).BusinessOwner == Symbol)
                    {
                        if (((GamingCompanies)buldings[i]).BusinessType == businessType)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }//проверка куплен ли хоть один бизнес этого типа
        public bool IsHaveEnemyBusinessType(List<User> user, BusinessType businessType, List<Building> buldings)
        {
            int nextPlayer = 0;
            for (int i = 0; i < buldings.Count; i++)
            {
                if (buldings[i].GetType() == typeof(Business))
                {
                    if (((Business)buldings[i]).BusinessOwner == user[nextPlayer].Symbol)
                    {
                        if (((Business)buldings[i]).BusinessType == businessType)
                        {
                            return true;
                        }
                    }
                }
                else if (buldings[i].GetType() == typeof(CarInterior))
                {
                    if (((CarInterior)buldings[i]).BusinessOwner == user[nextPlayer].Symbol)
                    {
                        if (((CarInterior)buldings[i]).BusinessType == businessType)
                        {
                            return true;
                        }
                    }
                }
                else if (buldings[i].GetType() == typeof(GamingCompanies))
                {
                    if (((GamingCompanies)buldings[i]).BusinessOwner == user[nextPlayer].Symbol)
                    {
                        if (((GamingCompanies)buldings[i]).BusinessType == businessType)
                        {
                            return true;
                        }
                    }
                }
                if (nextPlayer >= user.Count - 1)
                {
                    nextPlayer = 0;
                }
                nextPlayer++;
            }
            return false;
        }//проверка куплен ли хоть один бизнес этого типа у противника
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
        }//проверка типа бизнеса и бизнеса
        public bool IsHaveMeMonoopoly(List<Building> buldings)
        {
            for (int i = (int)BusinessType.Restaurants; i <= (int)BusinessType.GameCorparation; i++)
            {
                if (IsMonopolyByType((BusinessType)i, buldings)) return true;
            }
            return false;
        }//проверка есть ли хоть одна купленная монополия 
        public List<Building> MonoopolyImprovement(List<Building> buldings)
        {
            List<Building> monopolyBusiness = new List<Building>();
            for (int i = (int)BusinessType.Restaurants; i <= (int)BusinessType.GameCorparation; i++)
            {
                if (IsMonopolyByType((BusinessType)i, buldings))
                {
                    monopolyBusiness = buldings.Where(x => x.GetType() == typeof(Business)).
                        Where(x => ((Business)x).BusinessType == (BusinessType)i).ToList();
                    break;
                }
            }
            return monopolyBusiness;
        }//добовление бизнесов которых можно улучшить 
        public void MonoopolyImprovement1(List<Building> buildings)
        {
            if (buildings.Count == 0)
            {
                return;
            }
            BusinessType bsnTypeMortgaged = 0;
            bool checkBsnMortgaged = false;
            int numberCell;
            Random random = new Random();
            int min = int.MaxValue, max = int.MinValue;
            List<Building> monopolyBusiness = buildings;
            #region TestCode
            //for (int i = (int)BusinessType.Airlines; i <= (int)BusinessType.GameCorparation; i++)
            //{
            //    if (IsMonopolyByType((BusinessType)i, buildings))
            //    {
            //        monopolyBusiness = buildings.Where(x => x.GetType() == typeof(Business)).
            //            Where(x => ((Business)x).BusinessType == (BusinessType)i).ToList();
            //        break;
            //    }
            //}
            #endregion
            for (int i = monopolyBusiness.Count - 1; i >= 0; i--)
            {
                if (((Business)monopolyBusiness[i]).Mortgaged)
                {
                    bsnTypeMortgaged = ((Business)monopolyBusiness[i]).BusinessType;
                    monopolyBusiness.Remove(monopolyBusiness[i]);
                    checkBsnMortgaged = true;
                }
                if (checkBsnMortgaged)
                {
                    for (int j = monopolyBusiness.Count - 1; j >= 0; j--)
                    {
                        if (bsnTypeMortgaged == ((Business)monopolyBusiness[j]).BusinessType)
                        {
                            monopolyBusiness.Remove(monopolyBusiness[j]);
                        }
                    }
                }
                if (monopolyBusiness.Count == 0)
                {
                    return;
                }
            }
            //if (monopolyBusiness.Count == 0)
            //{
            //    return;
            //}
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
                #region Test
                //for (int j = 0; j < monopolyBusiness.Count; j++)
                //{
                //    if (((Business)monopolyBusiness[i]).Level > ((Business)monopolyBusiness[j]).Level)
                //    {
                //        max = ((Business)monopolyBusiness[i]).Level;
                //    }
                //    else
                //    {
                //        min = ((Business)monopolyBusiness[j]).Level;
                //    }
                //}
                #endregion
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

            if (min == 5) return;

            numberCell = random.Next(monopolyBusiness.Count);
            if (((Business)monopolyBusiness[numberCell]).UpgradePrice <= Balance)
            {
                ((Business)(monopolyBusiness[numberCell])).Level += 1;
                Balance -= ((Business)monopolyBusiness[numberCell]).UpgradePrice;
                Console.WriteLine($"Игрок {Symbol} строит филиал цена {((Business)monopolyBusiness[numberCell]).UpgradePrice}");
                Thread.Sleep(2000);
            }
            //Balance -= ((Business)buildings[numberCell]).Upgradeprise;
        }//улучшение бизнеса //fix
        public bool CheckCell(Building buldings, List<User> users, Field field)//проверк ячейки на которую попал бот 
        {
            if (buldings.GetType() == typeof(Business))
            {
                if (IsCheckBsnHaveBot(buldings))
                {
                    return true;
                }
                if (IsCheckCellBy((Business)buldings))
                {
                    PayRent(buldings, users, field);
                    return true;
                }
                if (!IsByCell(buldings, field.Buldings))
                {
                    Auction = true;
                }

            }//проверка что ячейка бизнес 
            else if (buldings.GetType() == typeof(CarInterior))
            {
                if (IsCheckBsnHaveBot(buldings))
                {
                    return true;
                }
                if (IsCheckCellBy((CarInterior)buldings))
                {
                    PayRent(buldings, users, field);
                    return true;
                }
                if (!IsByCell(buldings, field.Buldings))
                {
                    Auction = true;
                }
                //IsByCell(buldings, field.Buldings);

            }//проверка что ячейка автоцентр
            else if (buldings.GetType() == typeof(GamingCompanies))
            {
                if (IsCheckBsnHaveBot(buldings))
                {
                    return true;
                }
                if (IsCheckCellBy((GamingCompanies)buldings))
                {
                    PayRent(buldings, users, field);
                    return true;
                }
                if (!IsByCell(buldings, field.Buldings))
                {
                    Auction = true;
                }
                //IsByCell(buldings, field.Buldings);

            }//проверка что ячейка игровая компания 
            else if (buldings.GetType() == typeof(Jackpot))
            {
                Random random = new Random();
                int numberCubs = 0;
                int priceGame = 1000;
                if (random.Next(0, 1) == 0)
                {
                    if (Balance > priceGame)
                    {
                        Console.WriteLine($"Бот попал на джекпот и начинает игру запалтив {priceGame}");
                        Thread.Sleep(2000);
                        Balance -= priceGame;
                        int[] arrayCell = new int[random.Next(1, 4)];
                        for (int i = 0; i < arrayCell.Length; i++)
                        {
                            numberCubs = random.Next(1, 7);
                            for (int j = 0; j < arrayCell.Length; j++)
                            {
                                if (numberCubs == arrayCell[j])
                                {
                                    break;
                                }
                                else if (j == arrayCell.Length - 1)
                                {
                                    arrayCell[i] = random.Next(1, 7);
                                }
                            }
                        }
                        Console.WriteLine($"Бот кинул кубик и ему выпало {numberCubs}");
                        Thread.Sleep(2000);
                        //firstCube = RollTheCube(rand);    
                        for (int i = 0; i < arrayCell.Length; i++)
                        {
                            if (arrayCell[i] == /*firstCube*/numberCubs)
                            {
                                Console.WriteLine("Кубики совпали");
                                Thread.Sleep(2000);
                                if (arrayCell.Length == 3)
                                {
                                    Balance += 2000;
                                    Console.WriteLine("Бот выиграл 2000 ");
                                    Thread.Sleep(2000);
                                }
                                else if (arrayCell.Length == 2)
                                {
                                    Balance += 3000;
                                    Console.WriteLine("Бот выиграл 3000 ");
                                    Thread.Sleep(2000);
                                }
                                else if (arrayCell.Length == 1)
                                {
                                    Balance += 6000;
                                    Console.WriteLine("Бот выиграл 6000 ");
                                    Thread.Sleep(2000);
                                }
                                break;
                            }
                            Console.WriteLine("Кубики не совпали бот проиграл");
                            Thread.Sleep(2000);
                            break;
                        }
                        return true;
                    }
                    Console.WriteLine("У бота не хватило денег на игру в джекпот");
                    Thread.Sleep(2000);
                    return true;
                }
                Console.WriteLine("бот отказался от игры в джекпот");
                Thread.Sleep(2000);
                return true;

            }//проверка что ячейка джекпот 
            else if (buldings.GetType() == typeof(Bank))
            {
                if (Balance > ((Bank)buldings).Summa)
                {
                    Balance -= ((Bank)buldings).Summa;
                    Console.WriteLine($"Игрок {Symbol} попал на ячейку банк сумма списания {((Bank)buldings).Summa}");
                    Thread.Sleep(2000);
                }
                else
                {
                    MortagageBusiness(BotBusinesses(field.Buldings), users, field.Buldings);
                }
            }//проверка что ячейка налог 
            else if (buldings.GetType() == typeof(Tax))
            {
                if (Balance > ((Tax)buldings).Summa)
                {
                    Balance -= ((Tax)buldings).Summa;
                    Console.WriteLine($"Игрок {Symbol} попал на ячейку налог сумма списания {((Tax)buldings).Summa}");
                    Thread.Sleep(2000);
                }
                else
                {
                    MortagageBusiness(BotBusinesses(field.Buldings), users, field.Buldings);
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
                Console.WriteLine($"Игрок {Symbol} попал на старт и получает {((Start)buldings).Summa}");
                Thread.Sleep(2000);
            }//проверка что ячейка старт 
            else if (buldings.GetType() == typeof(Chance))
            {
                Random random = new Random();
                Chances chance = ((Chance)buldings).Chances[random.Next(0, ((Chance)buldings).Chances.Count)];
                ChanceAnalysis(chance, field, users);
            }//проверка что ячейка шанс
            else if (buldings.GetType() == typeof(Prison))
            {
                Console.WriteLine($"Бот {Symbol} попал на ячейку тюрьма в качестве прогулки");
                Thread.Sleep(2000);
            }//проверка если бот попал в тюрьму на прогулке 
            return false;
        }
        public List<Building> BotBusinesses(List<Building> building)
        {
            List<Building> bsn = new List<Building>();
            for (int i = 0; i < building.Count; i++)
            {
                if (building[i].GetType() == typeof(Business))
                {
                    if (((Business)building[i]).BusinessOwner == Symbol && !((Business)building[i]).Mortgaged /*&& !IsMonopolyByType(((Business)building[i]).BusinessType, building)*/)
                    {
                        bsn.Add(building[i]);
                    }
                }
                else if (building[i].GetType() == typeof(CarInterior))
                {
                    if (((CarInterior)building[i]).BusinessOwner == Symbol && !((CarInterior)building[i]).Mortgaged/* && IsMonopolyByType(((CarInterior)building[i]).BusinessType, building)*/)
                    {
                        bsn.Add(building[i]);
                    }
                }
                else if (building[i].GetType() == typeof(GamingCompanies))
                {
                    if (((GamingCompanies)building[i]).BusinessOwner == Symbol && !((GamingCompanies)building[i]).Mortgaged /*&& IsMonopolyByType(((GamingCompanies)building[i]).BusinessType, building)*/)
                    {
                        bsn.Add(building[i]);
                    }
                }
            }
            return bsn;
        } //купленные бизнесы бота
        public void MortagageBusiness(List<Building> myBuilding, List<User> users, List<Building> allBuilding)//заложить бизнес 
        {
            if (myBuilding.Count == 0)
            {
                return;
            }
            List<int> businessValue = new List<int>();
            for (int i = 0; i < myBuilding.Count; i++)
            {
                businessValue.Add(BusinessValuation(myBuilding[i], myBuilding, allBuilding, users));
            }
            int min = businessValue.Min();
            for (int i = 0; i < myBuilding.Count; i++)
            {
                if (min == businessValue[i])
                {
                    if (myBuilding[i].GetType() == typeof(Business))
                    {
                        if (((Business)myBuilding[i]).Level == 0)
                        {
                            ((Business)myBuilding[i]).Mortgaged = true;
                            Balance += ((Business)myBuilding[i]).ValueOfCollaterel;
                            Console.WriteLine($"Игрок {Symbol} заложил бизнес {myBuilding[i].Title} цена {((Business)myBuilding[i]).ValueOfCollaterel}");
                            Thread.Sleep(2000);
                            return;
                        }
                        else
                        {
                            ((Business)myBuilding[i]).Level -= 1;
                            Balance += ((Business)myBuilding[i]).UpgradePrice;
                            Console.WriteLine($"Игрок {Symbol} продал филиал бизнеса цена {((Business)myBuilding[i]).UpgradePrice}");
                            Thread.Sleep(2000);
                        }
                    }
                    if (myBuilding[i].GetType() == typeof(CarInterior))
                    {
                        ((CarInterior)myBuilding[i]).Mortgaged = true;
                        Balance += ((CarInterior)myBuilding[i]).ValueOfCollaterel;
                        Console.WriteLine($"Игрок {Symbol} заложил бизнес {myBuilding[i].Title} цена {((CarInterior)myBuilding[i]).ValueOfCollaterel}");
                        Thread.Sleep(2000);
                        return;
                    }
                    if (myBuilding[i].GetType() == typeof(GamingCompanies))
                    {
                        ((GamingCompanies)myBuilding[i]).Mortgaged = true;
                        Balance += ((GamingCompanies)myBuilding[i]).ValueOfCollaterel;
                        Console.WriteLine($"Игрок {Symbol} заложил бизнес {myBuilding[i].Title} цена {((GamingCompanies)myBuilding[i]).ValueOfCollaterel}");
                        Thread.Sleep(2000);
                        return;
                    }
                }
            }
        } //fix 50/50
        public bool IsMonopolyPossible(Building business, List<Building> building)
        {
            for (int i = 0; i < building.Count; i++)
            {
                if (business.GetType() == typeof(Business) && building[i].GetType() == typeof(Business))
                {
                    if (((Business)business).BusinessType == ((Business)building[i]).BusinessType && business.Number != building[i].Number)
                    {
                        return true;
                    }
                }
            }
            return false;
        }//проверка может ли быть монополия 
        public bool CanEnemyHaveMonopoly(BusinessType businessType, List<Building> building, List<User> users)
        {
            for (int i = 0; i < users.Count; i++)
            {
                if (users[i].Symbol != Symbol)
                {
                    if (users[i].GetType() == typeof(Player))
                    {
                        if (((Player)users[i]).IsMonopolyByType(businessType, building))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }//проверка на монополию у противника возможна ли она 
        public int BusinessValuation(Building business, List<Building> myBuilding, List<Building> allBuilding, List<User> users)//оценка бизнеса 
        {
            int businessValue = 0;
            if (business.GetType() == typeof(Business))
            {
                if (IsMonopolyByType(((Business)business).BusinessType, myBuilding))
                {
                    businessValue += 3;
                }
                if (IsMonopolyPossible(business, myBuilding))
                {
                    businessValue += 2;
                }
                if (IsHaveEnemyBusinessType(users, ((Business)business).BusinessType, allBuilding))
                {
                    businessValue += 1;
                }
                if (CanEnemyHaveMonopoly(((Business)business).BusinessType, allBuilding, users))
                {
                    businessValue += 3;
                }
            }
            if (business.GetType() == typeof(CarInterior))
            {
                if (IsMonopolyByType(((CarInterior)business).BusinessType, myBuilding))
                {
                    businessValue += 3;
                }
                if (IsMonopolyPossible(business, myBuilding))
                {
                    businessValue += 2;
                }
                if (IsHaveEnemyBusinessType(users, ((CarInterior)business).BusinessType, allBuilding))
                {
                    businessValue += 1;
                }
                if (CanEnemyHaveMonopoly(((CarInterior)business).BusinessType, allBuilding, users))
                {
                    businessValue += 3;
                }
            }
            if (business.GetType() == typeof(GamingCompanies))
            {
                if (IsMonopolyByType(((GamingCompanies)business).BusinessType, myBuilding))
                {
                    businessValue += 3;
                }
                if (IsMonopolyPossible(business, myBuilding))
                {
                    businessValue += 2;
                }
                if (IsHaveEnemyBusinessType(users, ((GamingCompanies)business).BusinessType, allBuilding))
                {
                    businessValue += 1;
                }
                if (CanEnemyHaveMonopoly(((GamingCompanies)business).BusinessType, allBuilding, users))
                {
                    businessValue += 3;
                }
            }
            return businessValue;
        }
        #region NotuseMetod
        public bool BusinessBuyout(List<Building> building)//выкуп заложенного бизнеса
        {
            Random random = new Random();
            //List<Building> listBuilding = new List<Building>();
            List<Building> listBuilding = building;
            int pos;
            const int minBalance = 3000;
            //if (IsCheckMonoopollyLvl(building))
            //{
            //    for (int i = 0; i < building.Count; i++)
            //    {
            //        if (building[i].GetType() == typeof(Business))
            //        {
            //            if (((Business)building[i]).BusinessOwner == Symbol)
            //            {
            //                if (((Business)building[i]).Mortgaged)
            //                {
            //                    listBuilding.Add(building[i]);
            //                }
            //            }
            //        }
            //        else if (building[i].GetType() == typeof(CarInterior))
            //        {
            //            if (((CarInterior)building[i]).Mortgaged)
            //            {
            //                listBuilding.Add(building[i]);
            //            }
            //        }
            //        else if (building[i].GetType() == typeof(GamingCompanies))
            //        {
            //            if (((GamingCompanies)building[i]).Mortgaged)
            //            {
            //                listBuilding.Add(building[i]);
            //            }
            //        }
            //    }
            //}
            //else if (IsHaveMeMonoopoly(building))
            //{
            //    return false;
            //}
            pos = random.Next(listBuilding.Count);
            if (listBuilding.Count == 0)
            {
                return false;
            }
            if (Balance >= minBalance)
            {
                if (listBuilding[pos].GetType() == typeof(Business))
                {
                    ((Business)listBuilding[pos]).Mortgaged = false;
                    ((Business)listBuilding[pos]).BusinessDowntrun = 15;
                    Balance -= ((Business)listBuilding[pos]).RansomValue;
                    Console.WriteLine($"Бот {Symbol} выкупает свой бизнес {((Business)listBuilding[pos]).Title} за цену {((Business)listBuilding[pos]).RansomValue}");
                    Thread.Sleep(2000);
                    return true;
                }
                if (listBuilding[pos].GetType() == typeof(CarInterior))
                {
                    ((CarInterior)listBuilding[pos]).Mortgaged = false;
                    ((CarInterior)listBuilding[pos]).BusinessDowntrun = 15;
                    Balance -= ((CarInterior)listBuilding[pos]).RansomValue;
                    Console.WriteLine($"Бот {Symbol} выкупает свой бизнес {((CarInterior)listBuilding[pos]).Title} за цену {((CarInterior)listBuilding[pos]).RansomValue}");
                    Thread.Sleep(2000);
                    return true;
                }
                if (listBuilding[pos].GetType() == typeof(GamingCompanies))
                {
                    ((GamingCompanies)listBuilding[pos]).Mortgaged = false;
                    ((GamingCompanies)listBuilding[pos]).BusinessDowntrun = 15;
                    Balance -= ((GamingCompanies)listBuilding[pos]).RansomValue;
                    Console.WriteLine($"Бот {Symbol} выкупает свой бизнес {((GamingCompanies)listBuilding[pos]).Title} за цену {((GamingCompanies)listBuilding[pos]).RansomValue}");
                    Thread.Sleep(2000);
                    return true;
                }
            }
            return false;
        }
        public bool MortagagedBusinesses(List<Building> building)
        {
            for (int i = 0; i < building.Count; i++)
            {
                if (building[i].GetType() == typeof(Business) && ((Business)building[i]).BusinessOwner == Symbol && ((Business)building[i]).Mortgaged == true)
                {
                    return true;
                }
                if (building[i].GetType() == typeof(CarInterior) && ((CarInterior)building[i]).BusinessOwner == Symbol && ((CarInterior)building[i]).Mortgaged == true)
                {
                    return true;
                }
                if (building[i].GetType() == typeof(GamingCompanies) && ((GamingCompanies)building[i]).BusinessOwner == Symbol && ((GamingCompanies)building[i]).Mortgaged == true)
                {
                    return true;
                }
            }
            return false;
        }//проверка если заложенный бизнес 
        public List<Building> AllMortagagedBusinesses(List<Building> building)
        {
            List<Building> result = new List<Building>();
            for (int i = 0; i < building.Count; i++)
            {
                if (building[i].GetType() == typeof(Business) && ((Business)building[i]).BusinessOwner == Symbol && ((Business)building[i]).Mortgaged)
                {
                    result.Add(building[i]);
                }
                if (building[i].GetType() == typeof(CarInterior) && ((CarInterior)building[i]).BusinessOwner == Symbol && ((CarInterior)building[i]).Mortgaged)
                {
                    result.Add(building[i]);
                }
                if (building[i].GetType() == typeof(GamingCompanies) && ((GamingCompanies)building[i]).BusinessOwner == Symbol && ((GamingCompanies)building[i]).Mortgaged)
                {
                    result.Add(building[i]);
                }
            }
            return result;
        }//все заложенные бизнесы 
        #endregion
        public bool Bsn(List<Building> buildings)
        {
            for (int i = 0; i < buildings.Count; i++)
            {
                if (buildings[i].GetType() == typeof(Business))
                {
                    if (((Business)buildings[i]).BusinessOwner == Symbol)
                    {
                        return true;
                    }
                }
                if (buildings[i].GetType() == typeof(CarInterior))
                {
                    if (((CarInterior)buildings[i]).BusinessOwner == Symbol)
                    {
                        return true;
                    }
                }
                if (buildings[i].GetType() == typeof(GamingCompanies))
                {
                    if (((GamingCompanies)buildings[i]).BusinessOwner == Symbol)
                    {
                        return true;
                    }
                }
            }
            return false;
        }//проверка куплен ли хоть один бизнес 
        public bool CheckBsnAllMortagaged(List<Building> buildings)
        {
            bool checkBsnAllMortagaged = true; ;
            for (int i = 0; i < buildings.Count; i++)
            {
                if (buildings[i].GetType() == typeof(Business))
                {
                    if (!((Business)buildings[i]).Mortgaged)
                    {
                        return checkBsnAllMortagaged = false;
                    }
                }
                else if (buildings[i].GetType() == typeof(CarInterior))
                {
                    if (!((CarInterior)buildings[i]).Mortgaged)
                    {
                        return checkBsnAllMortagaged = false;
                    }
                }
                else if (buildings[i].GetType() == typeof(GamingCompanies))
                {
                    if (!((GamingCompanies)buildings[i]).Mortgaged)
                    {
                        return checkBsnAllMortagaged = false;
                    }
                }
            }
            return checkBsnAllMortagaged;
        }
        public bool SurrenderLogic(List<Building> buildings)
        {

            buildings[CordinationPlayer].Symbol.Remove(Symbol);
            CordinationPlayer = 0;
            Surrender = true;
            Console.WriteLine($"Игрок {Symbol} Сдался ");
            Thread.Sleep(2000);
            return true;
        } //бот сдаётся 
        public int CountBsn(Business business, List<Building> buildings)//проверка сколько бизнесов типа у бота 
        {
            int countBsn = 0;
            int[] interest = new int[] { 20, 50, 85 };
            for (int i = 0; i < buildings.Count; i++)
            {
                if (buildings[i].GetType() == typeof(Business))
                {
                    if (((Business)buildings[i]).BusinessOwner == Symbol && ((Business)buildings[i]).BusinessType == business.BusinessType)
                    {
                        countBsn++;
                    }
                }
            }
            return Balance / 100 * interest[countBsn] + business.Price;
        }
    }
}

