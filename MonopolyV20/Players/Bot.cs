using System;
using System.Collections.Generic;
using System.Linq;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

namespace MonopolyV20
{
    public class Bot : User
    {
        public bool Auction { get; set; }
        public Bot(string name, char symbol, int balance, bool stepSkip, bool prison) : base(name, symbol, balance, stepSkip, prison)
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
        public bool IsByCell(Building bulding)
        {
            if (bulding.GetType() == typeof(Business))
            {
                if (((Business)bulding).Price < Balance)
                {
                    ((Business)bulding).BusinessOwner = Symbol;
                    Balance -= ((Business)bulding).Price;
                    return true;
                }
            }
            else if (bulding.GetType() == typeof(CarInterior))
            {
                if (((CarInterior)bulding).Price < Balance)
                {
                    ((CarInterior)bulding).BusinessOwner = Symbol;
                    Balance -= ((CarInterior)bulding).Price;
                    return true;
                }
            }
            else if (bulding.GetType() == typeof(GamingCompanies))
            {
                if (((GamingCompanies)bulding).Price < Balance)
                {
                    ((GamingCompanies)bulding).BusinessOwner = Symbol;
                    Balance -= ((GamingCompanies)bulding).Price;
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
                            }
                        }
                    }
                }
                else
                {
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
                            }
                        }
                    }
                }
                else
                {
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
                            }
                        }
                    }
                }
                else
                {
                    MortagageBusiness(BotBusinesses(field.Buldings), user, field.Buldings);
                }
            }
        }//выплата ренты поля 
        public void ChanceAnalysis(Chances chances, Field field, List<User> users)
        {
            if (chances.GetType() == typeof(Profit))
            {
                Balance += ((Profit)chances).GettingMoney;
            }
            else if (chances.GetType() == typeof(Lesion))
            {
                if (Balance > ((Lesion)chances).WriteOffMoney)
                {
                    Balance -= ((Lesion)chances).WriteOffMoney;
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
                            field.Buldings[CordinationPlayer].Symbol.Add(Symbol);//fix
                            CheckCell(field.Buldings[CordinationPlayer], users, field);
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
            for (int i = (int)BusinessType.Airlines; i <= (int)BusinessType.GameCorparation; i++)
            {
                if (IsMonopolyByType((BusinessType)i, buldings)) return true;
            }
            return false;
        }//проверка есть ли хоть одна купленная монополия 
        public List<Building> MonoopolyImprovement(List<Building> buldings)
        {
            int min = 0, max = 0;
            List<Building> monopolyBusiness = new List<Building>();
            for (int i = (int)BusinessType.Airlines; i <= (int)BusinessType.GameCorparation; i++)
            {
                if (IsMonopolyByType((BusinessType)i, buldings))
                {
                    monopolyBusiness = buldings.Where(x => x.GetType() == typeof(Business)).
                        Where(x => ((Business)x).BusinessType == (BusinessType)i).ToList();
                    break;
                }
            }
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
                    if (((Business)monopolyBusiness[i]).Level == max)
                    {
                        monopolyBusiness.RemoveAt(i);
                    }
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
            int numberCell;
            Random random = new Random();
            int min = 0, max = 0;
            List<Building> monopolyBusiness = new List<Building>();
            for (int i = (int)BusinessType.Airlines; i <= (int)BusinessType.GameCorparation; i++)
            {
                if (IsMonopolyByType((BusinessType)i, buildings))
                {
                    monopolyBusiness = buildings.Where(x => x.GetType() == typeof(Business)).
                        Where(x => ((Business)x).BusinessType == (BusinessType)i).ToList();
                    break;
                }
            }
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
                    if (((Business)monopolyBusiness[i]).Level == max)
                    {
                        monopolyBusiness.RemoveAt(i);
                    }
                }
            }
            numberCell = random.Next(0, buildings.Count);
            if (((Business)buildings[numberCell]).UpgradePrice <= Balance)
            {
                if (((Business)buildings[numberCell]).Level != 5)
                {
                    ((Business)buildings[numberCell]).Level += 1;
                }
                Balance -= ((Business)buildings[numberCell]).UpgradePrice;
            }
            else
            {
                return;
            }
            //Balance -= ((Business)buildings[numberCell]).Upgradeprise;
        }//улучшение бизнеса //fix
        public bool CheckCell(Building buldings, List<User> users, Field field)//проверк ячейки на которую попал бот 
        {
            if (buldings.GetType() == typeof(Business))
            {
                if (IsCheckCellBy((Business)buldings))
                {
                    PayRent(buldings, users, field);
                    return true;
                }
                if (!IsByCell(buldings))
                {
                    Auction = true;
                }

            }//проверка что ячейка бизнес 
            else if (buldings.GetType() == typeof(CarInterior))
            {
                if (IsCheckCellBy((CarInterior)buldings))
                {
                    PayRent(buldings, users, field);
                    return true;
                }
                IsByCell(buldings);

            }//проверка что ячейка автоцентр
            else if (buldings.GetType() == typeof(GamingCompanies))
            {
                if (IsCheckCellBy((GamingCompanies)buldings))
                {
                    PayRent(buldings, users, field);
                    return true;
                }
                IsByCell(buldings);

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
                        //firstCube = RollTheCube(rand);    
                        for (int i = 0; i < arrayCell.Length; i++)
                        {
                            if (arrayCell[i] == /*firstCube*/numberCubs)
                            {
                                if (arrayCell.Length == 3)
                                {
                                    Balance += 2000;
                                }
                                else if (arrayCell.Length == 2)
                                {
                                    Balance += 3000;
                                }
                                else if (arrayCell.Length == 1)
                                {
                                    Balance += 6000;
                                }
                                break;
                            }
                        }
                    }
                }
            }//проверка что ячейка джекпот 
            else if (buldings.GetType() == typeof(Bank))
            {
                if (Balance > ((Bank)buldings).Summa)
                {
                    Balance -= ((Bank)buldings).Summa;
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
            }//проверка что ячейка старт 
            else if (buldings.GetType() == typeof(Chance))
            {
                ((Chance)buldings).AddChance();
                Random random = new Random();
                ChanceAnalysis(((Chance)buldings).Chances[random.Next(0, ((Chance)buldings).Chances.Count)], field, users);
            }//проверка что ячейка шанс 
            return false;
        }
        public bool BusinessBuyout(List<Building> building)//выкуп заложенного бизнеса
        {
            Random random = new Random();
            List<Building> listBuilding = new List<Building>();
            int pos;
            const int minBalance = 3000;
            if (IsHaveMeMonoopoly(building))
            {
                return false;
            }
            else
            {
                for (int i = 0; i < building.Count; i++)
                {
                    if (building[i].GetType() == typeof(Business))
                    {
                        if (((Business)building[i]).BusinessOwner == Symbol)
                        {
                            if (((Business)building[i]).Mortgaged)
                            {
                                listBuilding.Add(building[i]);
                            }
                        }
                    }
                    else if (building[i].GetType() == typeof(CarInterior))
                    {
                        if (((CarInterior)building[i]).Mortgaged)
                        {
                            listBuilding.Add(building[i]);
                        }
                    }
                    else if (building[i].GetType() == typeof(GamingCompanies))
                    {
                        if (((GamingCompanies)building[i]).Mortgaged)
                        {
                            listBuilding.Add(building[i]);
                        }
                    }
                }
            }
            pos = random.Next(listBuilding.Count);
            if (Balance >= minBalance)
            {
                if (listBuilding[pos].GetType() == typeof(Business))
                {
                    ((Business)listBuilding[pos]).Mortgaged = false;
                    Balance -= ((Business)listBuilding[pos]).RansomValue;
                    return true;
                }
                if (listBuilding[pos].GetType() == typeof(CarInterior))
                {
                    ((CarInterior)listBuilding[pos]).Mortgaged = false;
                    Balance -= ((CarInterior)listBuilding[pos]).RansomValue;
                    return true;
                }
                if (listBuilding[pos].GetType() == typeof(GamingCompanies))
                {
                    ((GamingCompanies)listBuilding[pos]).Mortgaged = false;
                    Balance -= ((GamingCompanies)listBuilding[pos]).RansomValue;
                    return true;
                }
            }
            else
            {
                return false;
            }
            return true;
        } //fix
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
            List<int> businessValue = new List<int>();//доделать заложить бизнес выбрать минимальный 
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
                            return;
                        }
                        else
                        {
                            ((Business)myBuilding[i]).Level -= 1;
                            Balance -= ((Business)myBuilding[i]).UpgradePrice;

                        }
                    }
                    if (myBuilding[i].GetType() == typeof(CarInterior))
                    {
                        ((CarInterior)myBuilding[i]).Mortgaged = true;
                        Balance += ((CarInterior)myBuilding[i]).ValueOfCollaterel;
                        return;
                    }
                    if (myBuilding[i].GetType() == typeof(GamingCompanies))
                    {
                        ((GamingCompanies)myBuilding[i]).Mortgaged = true;
                        Balance += ((GamingCompanies)myBuilding[i]).ValueOfCollaterel;
                        return;
                    }
                }
            }
        }
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
        public void BotsBusinessDownturn(List<Building> building)
        {
           const int numberLaps = 15;
            for (int i = 0; i < building.Count; i++)
            {
                if (building[i].GetType() == typeof(Business))
                {
                    if (((Business)building[i]).BusinessDowntrun < numberLaps)
                    {
                        ((Business)building[i]).BusinessDowntrun += 1;
                    }
                    else
                    {
                        ((Business)building[i]).Mortgaged = false;
                        ((Business)building[i]).BusinessOwner = ' ';
                    }
                }
                if (building[i].GetType() == typeof(CarInterior))
                {
                    if (((CarInterior)building[i]).BusinessDowntrun < numberLaps)
                    {
                        ((CarInterior)building[i]).BusinessDowntrun += 1;
                    }
                    else
                    {
                        ((CarInterior)building[i]).Mortgaged = false;
                        ((CarInterior)building[i]).BusinessOwner = ' ';
                    }
                }
                if (building[i].GetType() == typeof(GamingCompanies))
                {
                    if (((GamingCompanies)building[i]).BusinessDowntrun < numberLaps)
                    {
                        ((GamingCompanies)building[i]).BusinessDowntrun += 1;
                    }
                    else
                    {
                        ((GamingCompanies)building[i]).Mortgaged = false;
                        ((GamingCompanies)building[i]).BusinessOwner = ' ';
                    }

                }
            }
        }//логика спада бизнеса у бота 
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
    }
}

