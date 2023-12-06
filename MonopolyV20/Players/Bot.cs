using System;
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
        }//проверка уровня монополии /// не используется 
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
                    return true;
                }
            }
            return false;
        }//Покупка ячейки 
        public void PayRent(Building bulding, List<User> user, Field field, int firstCube, int secondCube)
        {
            int summa = 0;
            if (bulding.GetType() == typeof(Business) && ((Business)bulding).Rent[((Business)bulding).Level] < Balance)
            {
                if (((Business)bulding).Mortgaged == false)
                {
                    if (bulding.GetType() == typeof(GamingCompanies))
                    {
                        summa = (firstCube + secondCube) * ((GamingCompanies)bulding).Rent[((GamingCompanies)bulding).Level];
                        Balance -= summa;
                    }
                    else
                    {
                        Balance -= ((Business)bulding).Rent[((Business)bulding).Level];
                    }
                    for (int i = 0; i < user.Count; i++)
                    {
                        if (((Business)bulding).BusinessOwner == user[i].Symbol)
                        {
                            if (bulding.GetType() == typeof(GamingCompanies))
                            {
                                user[i].Balance += summa;
                                Console.WriteLine($"Игрок {Symbol} выплатил ренту игроку {user[i].Symbol} цена {summa}");
                                Thread.Sleep(2000);
                                return;
                            }
                            else
                            {
                                user[i].Balance += ((Business)bulding).Rent[((Business)bulding).Level];
                                Console.WriteLine($"Игрок {Symbol} выплатил ренту игроку {user[i].Symbol} цена {((Business)bulding).Rent[((Business)bulding).Level]}");
                                Thread.Sleep(2000);
                                return;

                            }
                        }
                    }
                }
                #region Test1
                //else
                //{
                //    do
                //    {
                //        if (CheckBsnAllMortagaged(BotBusinesses(field.Buldings)))
                //        {
                //            SurrenderLogic(field.Buldings);
                //            return;
                //        }
                //        MortagageBusiness(BotBusinesses(field.Buldings), user, field.Buldings);
                //    }
                //    while (((Business)bulding).Rent[((Business)bulding).Level] >= Balance);
                //    if (((Business)bulding).Mortgaged == false)
                //    {
                //        Balance -= ((Business)bulding).Rent[((Business)bulding).Level];
                //        for (int i = 0; i < user.Count; i++)
                //        {
                //            if (((Business)bulding).BusinessOwner == user[i].Symbol)
                //            {
                //                user[i].Balance += ((Business)bulding).Rent[((Business)bulding).Level];
                //                Console.WriteLine($"Игрок {Symbol} выплатил ренту игроку {user[i].Symbol} цена {((Business)bulding).Rent[((Business)bulding).Level]}");
                //                Thread.Sleep(2000);
                //                return;
                //            }
                //        }
                //    }
                //}
                #endregion
            }
            #region Test
            //else if (bulding.GetType() == typeof(CarInterior))
            //{
            //    if (((CarInterior)bulding).Rent[((CarInterior)bulding).Level] < Balance)
            //    {
            //        if (((CarInterior)bulding).Mortgaged == false)
            //        {
            //            Balance -= ((CarInterior)bulding).Rent[((CarInterior)bulding).Level];
            //            for (int i = 0; i < user.Count; i++)
            //            {
            //                if (((CarInterior)bulding).BusinessOwner == user[i].Symbol)
            //                {
            //                    user[i].Balance += ((CarInterior)bulding).Rent[((CarInterior)bulding).Level];
            //                    Console.WriteLine($"Игрок {Symbol} выплатил ренту игроку {user[i].Symbol} цена {((CarInterior)bulding).Rent[((CarInterior)bulding).Level]}");
            //                    Thread.Sleep(2000);
            //                    return;
            //                }
            //            }
            //        }
            //    }
            //    else
            //    {
            //        do
            //        {
            //            if (CheckBsnAllMortagaged(BotBusinesses(field.Buldings)))
            //            {
            //                SurrenderLogic(field.Buldings);
            //                return;
            //            }
            //            MortagageBusiness(BotBusinesses(field.Buldings), user, field.Buldings);
            //        }
            //        while (((Business)bulding).Rent[((Business)bulding).Level] >= Balance);
            //    }
            //}
            //else if (bulding.GetType() == typeof(GamingCompanies))
            //{
            //    if (((GamingCompanies)bulding).Rent[((GamingCompanies)bulding).Level] < Balance)
            //    {
            //        if (((GamingCompanies)bulding).Mortgaged == false)
            //        {
            //            summa = (firstCube + secondCube) * ((GamingCompanies)bulding).Rent[((GamingCompanies)bulding).Level];
            //            Balance -= summa;
            //            for (int i = 0; i < user.Count; i++)
            //            {
            //                if (((GamingCompanies)bulding).BusinessOwner == user[i].Symbol)
            //                {
            //                    user[i].Balance += summa;
            //                    Console.WriteLine($"Игрок {Symbol} выплатил ренту игроку {user[i].Symbol} цена {summa}");
            //                    Thread.Sleep(2000);
            //                    return;
            //                }
            //            }
            //        }
            //    }
            //    else
            //    {
            //        if (CheckBsnAllMortagaged(BotBusinesses(field.Buldings)))
            //        {
            //            SurrenderLogic(field.Buldings);
            //            return;
            //        }
            //        MortagageBusiness(BotBusinesses(field.Buldings), user, field.Buldings);
            //    }
            //}
            #endregion
            else
            {
                do
                {
                    if (CheckBsnAllMortagaged(BotBusinesses(field.Buldings)))
                    {
                        SurrenderLogic(field.Buldings);
                        return;
                    }
                    MortagageBusiness(BotBusinesses(field.Buldings), user, field.Buldings);
                }
                while (((Business)bulding).Rent[((Business)bulding).Level] >= Balance);
                if (((Business)bulding).Mortgaged == false)
                {
                    if (bulding.GetType() == typeof(GamingCompanies))
                    {
                        summa = (firstCube + secondCube) * ((GamingCompanies)bulding).Rent[((GamingCompanies)bulding).Level];
                        Balance -= summa;
                    }
                    else
                    {
                        Balance -= ((Business)bulding).Rent[((Business)bulding).Level];
                    }
                    for (int i = 0; i < user.Count; i++)
                    {
                        if (((Business)bulding).BusinessOwner == user[i].Symbol)
                        {
                            if (bulding.GetType() == typeof(GamingCompanies))
                            {
                                user[i].Balance += summa;
                                Console.WriteLine($"Игрок {Symbol} выплатил ренту игроку {user[i].Symbol} цена {summa}");
                                Thread.Sleep(2000);
                                return;
                            }
                            else
                            {
                                user[i].Balance += ((Business)bulding).Rent[((Business)bulding).Level];
                                Console.WriteLine($"Игрок {Symbol} выплатил ренту игроку {user[i].Symbol} цена {((Business)bulding).Rent[((Business)bulding).Level]}");
                                Thread.Sleep(2000);
                                return;

                            }
                        }
                    }
                }
            }

        }//выплата ренты поля // баг
        public void ChanceCheck(Chances chances, Field field, List<User> users, int firstCube, int secondCube)
        {
            if (chances.GetType() == typeof(Profit))
            {
                Balance += ((Profit)chances).GettingMoney;
                Console.WriteLine($"Игроку {Symbol} выпал шанс {((Profit)chances).Title} {((Profit)chances).Description}");
                Thread.Sleep(2000);
            }
            else if (chances.GetType() == typeof(Lesion))
            {
                Console.WriteLine($"Игроку {Symbol} выпал шанс {((Lesion)chances).Title} {((Lesion)chances).Description}");
                Thread.Sleep(2000);
                if (Balance > ((Lesion)chances).WriteOffMoney)
                {
                    Console.WriteLine($"Игроку {Symbol} выплатил шанс {((Lesion)chances).Title} {((Lesion)chances).Description}");
                    Thread.Sleep(2000);
                    Balance -= ((Lesion)chances).WriteOffMoney;
                }
                else
                {
                    Console.WriteLine($"у Игроку {Symbol} не хватило деняг");
                    Thread.Sleep(2000);
                    do
                    {
                        if (CheckBsnAllMortagaged(BotBusinesses(field.Buldings)))
                        {
                            SurrenderLogic(field.Buldings);
                            Console.WriteLine($"У Бота {Symbol} нету деняг и бизнесов которые можно заложить по этому он решил сдаться");
                            Thread.Sleep(2000);
                            return;
                        }
                        MortagageBusiness(BotBusinesses(field.Buldings), users, field.Buldings);
                    }
                    while (((Lesion)chances).WriteOffMoney >= Balance);
                    Console.WriteLine($"Игроку {Symbol} выплатил шанс {((Lesion)chances).Title} {((Lesion)chances).Description}");
                    Balance -= ((Lesion)chances).WriteOffMoney;
                    Thread.Sleep(2000);
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
                            CheckCell(field.Buldings[CordinationPlayer], users, field, firstCube, secondCube);
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
        }//улучшение бизнеса
        public bool CheckCell(Building buldings, List<User> users, Field field, int firstCube, int secondCube)//проверк ячейки на которую попал бот 
        {
            if (buldings.GetType() == typeof(Business))
            {
                if (IsCheckBsnHaveBot(buldings))
                {
                    return true;
                }
                if (IsCheckCellBy((Business)buldings))
                {
                    PayRent(buldings, users, field, firstCube, secondCube);
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
                    PayRent(buldings, users, field, firstCube, secondCube);
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
                    PayRent(buldings, users, field, firstCube, secondCube);
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
                int numberCubs = random.Next(1, 7);
                int priceGame = 1000;
                bool JackpotWin = false;
                int intermediateRusult = 0;
                bool intermediate = false;
                if (random.Next(0, 1) == 0)
                {
                    if (Balance > priceGame)
                    {
                        Console.WriteLine($"Бот попал на джекпот и начинает игру запалтив {priceGame}");
                        Thread.Sleep(2000);
                        Balance -= priceGame;
                        int[] arrayCell = new int[random.Next(1, 4)];
                        Console.WriteLine($"Бот {Symbol} поставил количество кубиков {arrayCell.Length}   ");
                        Thread.Sleep(2000);
                        for (int i = 0; i < arrayCell.Length; i++)
                        {
                            intermediateRusult = random.Next(1, 7);
                            do
                            {
                                if (intermediate)
                                {
                                    intermediateRusult = random.Next(1, 7);
                                    intermediate = false;
                                }
                                for (int j = 0; j < arrayCell.Length; j++)
                                {
                                    if (arrayCell[j] == intermediateRusult)
                                    {
                                        intermediateRusult = random.Next(1, 7);
                                        j = 0;
                                        intermediate = true;
                                        break;

                                    }
                                }
                                if (!intermediate)
                                {
                                    arrayCell[i] = intermediateRusult;
                                    Console.WriteLine($"Бот {Symbol} ввел число {arrayCell[i]} в ячейке {i}");
                                    Thread.Sleep(2000);
                                }
                            }
                            while (intermediate);
                        }
                        Console.WriteLine($"В казино выпал рандомный кубик {numberCubs}");
                        Thread.Sleep(2000);
                        for (int i = 0; i < arrayCell.Length; i++)
                        {
                            if (arrayCell[i] == numberCubs)
                            {
                                Console.WriteLine("Кубики совпали");
                                Thread.Sleep(2000);
                                if (arrayCell.Length == 3)
                                {
                                    Balance += 2000;
                                    Console.WriteLine("Бот выиграл 2000 ");
                                    Thread.Sleep(2000);
                                    JackpotWin = true;
                                }
                                else if (arrayCell.Length == 2)
                                {
                                    Balance += 3000;
                                    Console.WriteLine("Бот выиграл 3000 ");
                                    Thread.Sleep(2000);
                                    JackpotWin = true;
                                }
                                else if (arrayCell.Length == 1)
                                {
                                    Balance += 6000;
                                    Console.WriteLine("Бот выиграл 6000 ");
                                    Thread.Sleep(2000);
                                    JackpotWin = true;
                                }
                                break;
                            }
                        }
                        if (!JackpotWin)
                        {
                            Console.WriteLine("Кубики не совпали бот проиграл");
                            Thread.Sleep(2000);
                            return true;
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
                Console.WriteLine($"Игрок {Symbol} попал на ячейку банк сумма списания {((Bank)buldings).Summa}");
                Thread.Sleep(2000);
                if (Balance > ((Bank)buldings).Summa)
                {
                    Console.WriteLine($"Игрок {Symbol} Выплатил {((Bank)buldings).Summa}");
                    Thread.Sleep(2000);
                    Balance -= ((Bank)buldings).Summa;
                }
                else
                {
                    Console.WriteLine($"Игрок {Symbol} не хватило деняг");
                    Thread.Sleep(2000);
                    do
                    {
                        if (CheckBsnAllMortagaged(BotBusinesses(field.Buldings)))
                        {
                            SurrenderLogic(field.Buldings);
                            Console.WriteLine($"У Бота {Symbol} нету деняг и бизнесов которые можно заложить по этому он решил сдаться");
                            Thread.Sleep(2000);
                            return true;
                        }
                        MortagageBusiness(BotBusinesses(field.Buldings), users, field.Buldings);
                    }
                    while (((Bank)buldings).Summa >= Balance);
                    Console.WriteLine($"Игрок {Symbol} Выплатил {((Bank)buldings).Summa}");
                    Thread.Sleep(2000);
                    Balance -= ((Bank)buldings).Summa;

                }
            }//проверка что ячейка налог 
            else if (buldings.GetType() == typeof(Tax))
            {
                Console.WriteLine($"Игрок {Symbol} попал на ячейку налог сумма списания {((Tax)buldings).Summa}");
                Thread.Sleep(2000);
                if (Balance > ((Tax)buldings).Summa)
                {
                    Balance -= ((Tax)buldings).Summa;
                    Console.WriteLine($"Игрок {Symbol} попал на ячейку налог сумма списания {((Tax)buldings).Summa}");
                    Thread.Sleep(2000);
                }
                else
                {
                    Console.WriteLine($"Игрок {Symbol} не хватило деняг");
                    Thread.Sleep(2000);
                    do
                    {
                        if (CheckBsnAllMortagaged(BotBusinesses(field.Buldings)))
                        {
                            SurrenderLogic(field.Buldings);
                            Console.WriteLine($"У Бота {Symbol} нету деняг и бизнесов которые можно заложить по этому он решил сдаться");
                            Thread.Sleep(2000);
                            return true;
                        }
                        MortagageBusiness(BotBusinesses(field.Buldings), users, field.Buldings);
                    }
                    while (((Tax)buldings).Summa >= Balance);
                    Console.WriteLine($"Игрок {Symbol} Выплатил {((Bank)buldings).Summa}");
                    Balance -= ((Tax)buldings).Summa;
                    Thread.Sleep(2000);
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
                ChanceCheck(chance, field, users, firstCube, secondCube);
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
        public bool MortagageBusiness(List<Building> myBuilding, List<User> users, List<Building> allBuilding)//заложить бизнес 
        {
            if (myBuilding.Count == 0)
            {
                return false;
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
                            return true;
                        }
                        else
                        {
                            ((Business)myBuilding[i]).Level -= 1;
                            Balance += ((Business)myBuilding[i]).UpgradePrice;
                            Console.WriteLine($"Игрок {Symbol} продал филиал бизнеса {myBuilding[i].Title} цена {((Business)myBuilding[i]).UpgradePrice}");
                            Thread.Sleep(2000);
                        }
                    }
                    if (myBuilding[i].GetType() == typeof(CarInterior))
                    {
                        ((CarInterior)myBuilding[i]).Mortgaged = true;
                        Balance += ((CarInterior)myBuilding[i]).ValueOfCollaterel;
                        for (int j = 0; j < allBuilding.Count; j++)
                        {
                            if (allBuilding[j].GetType() == typeof(CarInterior))
                            {
                                if (allBuilding[j].Number != myBuilding[i].Number && ((Business)allBuilding[j]).BusinessOwner == Symbol)
                                {
                                    if (((Business)allBuilding[j]).Level > 0)
                                    {
                                        ((Business)allBuilding[j]).Level -= 1;
                                    }
                                    else if (((Business)myBuilding[i]).Level > 0)
                                    {
                                        ((Business)myBuilding[i]).Level -= 1;
                                    }
                                }
                            }
                        }
                        Console.WriteLine($"Игрок {Symbol} заложил бизнес {myBuilding[i].Title} цена {((CarInterior)myBuilding[i]).ValueOfCollaterel}");
                        Thread.Sleep(2000);
                        return true;
                    }
                    if (myBuilding[i].GetType() == typeof(GamingCompanies))
                    {
                        ((GamingCompanies)myBuilding[i]).Mortgaged = true;
                        Balance += ((GamingCompanies)myBuilding[i]).ValueOfCollaterel;
                        for (int j = 0; j < allBuilding.Count; j++)
                        {
                            if (allBuilding[j].GetType() == typeof(GamingCompanies))
                            {
                                if (allBuilding[j].Number != myBuilding[i].Number && ((Business)allBuilding[j]).BusinessOwner == Symbol)
                                {
                                    if (((Business)allBuilding[j]).Level > 0)
                                    {
                                        ((Business)allBuilding[j]).Level -= 1;
                                    }
                                    else if (((Business)myBuilding[i]).Level > 0)
                                    {
                                        ((Business)myBuilding[i]).Level -= 1;
                                    }
                                }
                            }
                        }
                        Console.WriteLine($"Игрок {Symbol} заложил бизнес {myBuilding[i].Title} цена {((GamingCompanies)myBuilding[i]).ValueOfCollaterel}");
                        Thread.Sleep(2000);
                        return true;
                    }
                }
            }
            return false;
        } //fix 50/50 //проверить работает ли переделка с функцией продажи бизнесса геймкомпания
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
            return businessValue;
        }
        #region NotuseMetod
        public bool BusinessBuyout(List<Building> buildings)//выкуп заложенного бизнеса
        {
            Random random = new Random();
            List<Building> listBuilding = buildings;
            int pos;
            const int minBalance = 3000;
            pos = random.Next(listBuilding.Count);
            if (listBuilding.Count == 0)
            {
                return false;
            }
            if (Balance >= minBalance)
            {
                int maxcount = 0;
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
                    ((Business)listBuilding[pos]).Level += 1;
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
                                ((Business)listBuilding[pos]).Level = maxcount;
                                ((Business)buildings[j]).Level = maxcount;
                            }
                        }
                    }
                    Console.WriteLine($"Бот {Symbol} выкупает свой бизнес {((CarInterior)listBuilding[pos]).Title} за цену {((CarInterior)listBuilding[pos]).RansomValue}");
                    Thread.Sleep(2000);
                    return true;
                }
                if (listBuilding[pos].GetType() == typeof(GamingCompanies))
                {
                    ((GamingCompanies)listBuilding[pos]).Mortgaged = false;
                    ((GamingCompanies)listBuilding[pos]).BusinessDowntrun = 15;
                    Balance -= ((GamingCompanies)listBuilding[pos]).RansomValue;
                    //((Business)listBuilding[pos]).Level += 1;
                    for (int j = 0; j < buildings.Count; j++)
                    {
                        if (buildings[j].GetType() == typeof(GamingCompanies))
                        {
                            if (buildings[j].Number != ((Business)listBuilding[pos]).Number && ((Business)buildings[j]).BusinessOwner == Symbol && !((Business)buildings[j]).Mortgaged)
                            {
                                if (((Business)buildings[j]).Level < 1)
                                {
                                    ((Business)buildings[j]).Level += 1;
                                }
                                else if (((Business)listBuilding[pos]).Level < 0)
                                {
                                    ((Business)listBuilding[pos]).Level += 1;
                                }
                            }
                        }
                    }
                    Console.WriteLine($"Бот {Symbol} выкупает свой бизнес {((GamingCompanies)listBuilding[pos]).Title} за цену {((GamingCompanies)listBuilding[pos]).RansomValue}");
                    Thread.Sleep(2000);
                    return true;
                }
            }
            return false;
        }// доделать с бизнесами кар центр и геймп центр сделать что бы цена вернулась в исходную после выкупа
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
        //public bool Bsn(List<Building> buildings)
        //{
        //    for (int i = 0; i < buildings.Count; i++)
        //    {
        //        if (buildings[i].GetType() == typeof(Business))
        //        {
        //            if (((Business)buildings[i]).BusinessOwner == Symbol)
        //            {
        //                return true;
        //            }
        //        }
        //        if (buildings[i].GetType() == typeof(CarInterior))
        //        {
        //            if (((CarInterior)buildings[i]).BusinessOwner == Symbol)
        //            {
        //                return true;
        //            }
        //        }
        //        if (buildings[i].GetType() == typeof(GamingCompanies))
        //        {
        //            if (((GamingCompanies)buildings[i]).BusinessOwner == Symbol)
        //            {
        //                return true;
        //            }
        //        }
        //    }
        //    return false;
        //}//проверка куплен ли хоть один бизнес 
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
        public int CountBsn(Business business, List<Building> buildings, List<User> users, char symbol)//проверка сколько бизнесов типа у бота 
        {
            double summa = 0;
            int countBsn = 0;
            int countBsnEnemy = 0;
            int interimAccount = 0;
            double[] interest = new double[] { 1.35, 1.75, 1.95 };
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
            for (int i = 0; i < users.Count; i++)
            {
                if (users[i].Symbol != symbol)
                {
                    for (int j = 0; j < buildings.Count; j++)
                    {
                        if (buildings[j].GetType() == typeof(Business))
                        {
                            if (((Business)buildings[j]).BusinessOwner == users[i].Symbol && ((Business)buildings[j]).BusinessType == business.BusinessType)
                            {
                                interimAccount += 1;
                            }
                        }
                    }
                    if (interimAccount > countBsnEnemy)
                    {
                        countBsnEnemy = interimAccount;
                    }
                }
            }
            if (countBsnEnemy == 0)
            {
                summa = business.Price * interest[countBsn] + (Balance / 25);
                return (int)summa;
            }
            else
            {
                summa = business.Price * (interest[countBsnEnemy] - 0.10) + (Balance / 25);
                return (int)summa;
            }
        }
    }
}

