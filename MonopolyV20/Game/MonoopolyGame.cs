
using System;
using System.Collections.Generic;
using System.Threading;

namespace MonopolyV20
{
    public class MonopolyGame
    {
        public List<User> Users { get; set; }
        public Field Field { get; set; }
        public Trade Trade { get; set; }
        public MonopolyGame()
        {
            Users = new List<User>();
            Field = new Field();
            Trade = new Trade();
        }
        //Method
        public bool IsCheckBotNull()
        {
            int countBot = 0;
            for (int i = 0; i < Users.Count; i++)
            {
                if (Users[i].GetType() == typeof(Bot))
                {
                    countBot += 1;
                }
            }
            if (countBot > 0)
            {
                return false;
            }
            return true;
        }//Метод проверки если ли Боты в игре
        public bool IsCheckPlayerNull()
        {
            int countPlayer = 0;
            for (int i = 0; i < Users.Count; i++)
            {
                if (Users[i].GetType() == typeof(Player))
                {
                    countPlayer += 1;
                }
            }
            if (countPlayer > 0)
            {
                return false;
            }
            return true;
        }//Метод проверки если ли Игроки в игре
        public bool IsCheckSymbol(char symbol)
        {
            if (symbol == '\0' || symbol == ' ')
            {
                return true;
            }
            for (int i = 0; i < Users.Count; i++)
            {
                if (Users[i].Symbol == symbol)
                {
                    return true;
                }
            }
            return false;
        }//Проверка не занят ли символ 
        public bool IsCheckName(string name)
        {
            const int length = 22;
            for (int i = 0; i < name.Length; i++)
            {
                if (!char.IsLetterOrDigit(name[i]))
                {
                    return true;
                }
            }
            if (name == "" || name == " " || name == "\0" || name.Length >= length)
            {
                return true;
            }
            for (int i = 0; i < Users.Count; i++)
            {
                if (Users[i].Name == name)
                {
                    return true;
                }
            }
            return false;
        }//Проверка не занят ли никнейм
        public int RollTheCube(Random rand)//Метод бросания кубиков
        {
            int cube = rand.Next(1, 7);
            return cube;
        }
        public bool ShowMyBsn(char symbol)
        {
            bool checkBsn = true;
            for (int i = 0; i < Field.Buldings.Count; i++)
            {
                if (Field.Buldings[i].GetType() == typeof(Business))
                {
                    if (((Business)Field.Buldings[i]).BusinessOwner == symbol)
                    {
                        if (((Business)Field.Buldings[i]).Mortgaged == false && ((Business)Field.Buldings[i]).Level == 0)
                        {
                            Console.WriteLine($"{i} > [ {Field.Buldings[i].Title} ] ");
                            checkBsn = false;
                        }
                    }
                }
                else if (Field.Buldings[i].GetType() == typeof(CarInterior))
                {
                    if (((CarInterior)Field.Buldings[i]).BusinessOwner == symbol && ((CarInterior)Field.Buldings[i]).Level == 0)
                    {
                        if (((CarInterior)Field.Buldings[i]).Mortgaged == false)
                        {
                            Console.WriteLine($"{i} > [ {Field.Buldings[i].Title} ] ");
                            checkBsn = false;
                        }
                    }
                }
                else if (Field.Buldings[i].GetType() == typeof(GamingCompanies))
                {
                    if (((GamingCompanies)Field.Buldings[i]).BusinessOwner == symbol && ((GamingCompanies)Field.Buldings[i]).Level == 0)
                    {
                        if (((GamingCompanies)Field.Buldings[i]).Mortgaged == false)
                        {
                            Console.WriteLine($"{i} > [ {Field.Buldings[i].Title} ] ");
                            checkBsn = false;
                        }
                    }
                }
            }
            return checkBsn;
        }//Вывод бизнесов игрока которые не заложенные
        public bool ShowMortgagedBsn(char symbol)
        {
            int countBsn = 0;
            for (int i = 0; i < Field.Buldings.Count; i++)
            {
                if (Field.Buldings[i].GetType() == typeof(Business))
                {
                    if (((Business)Field.Buldings[i]).BusinessOwner == symbol)
                    {
                        if (((Business)Field.Buldings[i]).Mortgaged == true)
                        {
                            Console.WriteLine($"{i} > [ {Field.Buldings[i].Title} ]");
                            countBsn++;
                        }
                    }
                }
                if (Field.Buldings[i].GetType() == typeof(CarInterior))
                {
                    if (((CarInterior)Field.Buldings[i]).BusinessOwner == symbol)
                    {
                        if (((CarInterior)Field.Buldings[i]).Mortgaged == true)
                        {
                            Console.WriteLine($"{i} > [ {Field.Buldings[i].Title} ]");
                            countBsn++;
                        }
                    }
                }
                if (Field.Buldings[i].GetType() == typeof(GamingCompanies))
                {
                    if (((GamingCompanies)Field.Buldings[i]).BusinessOwner == symbol)
                    {
                        if (((GamingCompanies)Field.Buldings[i]).Mortgaged == true)
                        {
                            Console.WriteLine($"{i} > [ {Field.Buldings[i].Title} ]");
                            countBsn++;
                        }
                    }
                }
            }
            if (countBsn == 0)
            {
                return true;
            }
            return false;
        }//вывод заложенных бизнесов
        public bool IsCheckWinGame()
        {
            int playerIsWin = 0;
            List<User> lastUser = new List<User>();
            for (int i = 0; i < Users.Count; i++)
            {
                if (Users[i].Surrender != true)
                {
                    playerIsWin++;
                    lastUser.Add(Users[i]);
                }
            }
            if (playerIsWin == 1)
            {
                Console.Clear();
                ShowWinGame();
                Console.WriteLine($"Никнейм [ {lastUser[0].Name} ][ {lastUser[0].Symbol} ] Символ");
                return true;
            }
            return false;
        }//проверка на победу 
        public void Auction(Building bulding, char symbol)
        {
            List<User> user = new List<User>();
            List<int> SummIn = new List<int>();
            int summaNext = 0;
            int chouce;
            int nextPlayer = 0;
            int bsnPrice = 0;
            bool isWork = true;
            Random random = new Random();
            BusinessType businessType = (BusinessType)0;
            bsnPrice = ((Business)bulding).Price + 100;
            businessType = ((Business)bulding).BusinessType;
            Console.WriteLine($"Аукцион начинается на бизнес {bulding.Title} начальная цена {bsnPrice}");
            Thread.Sleep(2000);
            for (int i = 0; i < Users.Count; i++)
            {
                if (Users[i].Symbol != symbol && !Users[i].Surrender)
                {

                    if (Users[i].GetType() == typeof(Bot))
                    {
                        if (Users[i].Balance <= bsnPrice)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine($"Игрок {Users[i].Symbol} отказался принять участие в аукцион потому что нету деняг");
                            Console.ForegroundColor = ConsoleColor.Gray;
                            Thread.Sleep(2000);
                            continue;
                        }
                        if (((Bot)Users[i]).IsHaveMeMonoopoly(Field.Buldings))
                        {
                            if (((Bot)Users[i]).IsCheckMonoopollyLvl(((Bot)Users[i]).MonoopolyImprovement(Field.Buldings)))
                            {
                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                Console.WriteLine($"Игрок {Users[i].Symbol} отказался от участия на аукционе");
                                Console.ForegroundColor = ConsoleColor.Gray;
                                Thread.Sleep(2000);
                                continue;
                            }
                        }
                        if (((Bot)Users[i]).IsHaveBusinessThisType(businessType, Field.Buldings) == true)
                        {
                            if (Users[i].Symbol != symbol && !Users[i].Surrender)
                            {
                                Console.ForegroundColor = ConsoleColor.DarkGreen;
                                Console.WriteLine($"Игрок {Users[i].Symbol} согласился принять участие в аукционе");
                                Console.ForegroundColor = ConsoleColor.Gray;
                                Thread.Sleep(2000);
                                user.Add(Users[i]);
                                SummIn.Add(((Bot)Users[i]).CountBsnPrice((Business)bulding, Field.Buldings, Users, Users[i].Symbol));
                                continue;
                            }
                        }
                        if (Users[i].Symbol != symbol && !Users[i].Surrender && Users[i].Balance > bsnPrice)
                        {
                            if (((Bot)Users[i]).CountBsnPrice((Business)bulding, Field.Buldings, Users, Users[i].Symbol) != 0)
                            {
                                Console.ForegroundColor = ConsoleColor.DarkGreen;
                                Console.WriteLine($"Игрок {Users[i].Symbol} согласился принять участие в аукционе");
                                Console.ForegroundColor = ConsoleColor.Gray;
                                Thread.Sleep(2000);
                                user.Add(Users[i]);
                                SummIn.Add(((Bot)Users[i]).CountBsnPrice((Business)bulding, Field.Buldings, Users, Users[i].Symbol));
                                continue;
                            }
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine($"Игрок {Users[i].Symbol} отказался от участия на аукционе");
                            Console.ForegroundColor = ConsoleColor.Gray;
                            Thread.Sleep(2000);
                            continue;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine($"Игрок {Users[i].Symbol} не может принять участие в аукционе");
                            Console.ForegroundColor = ConsoleColor.Gray;
                            Thread.Sleep(2000);
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Игрок {Users[i].Symbol} выбирает ");
                        Console.WriteLine("Принять участие в аукционе");
                        Console.WriteLine("1) Принять");
                        Console.WriteLine("2) Отказаться");
                        do
                        {
                            Console.Write("Ввод >> ");
                            int.TryParse(Console.ReadLine(), out chouce);
                        }
                        while (chouce > 2 || chouce < 1);
                        switch (chouce)
                        {
                            case 1:
                                {
                                    if (bsnPrice > Users[i].Balance)
                                    {
                                        Console.ForegroundColor = ConsoleColor.DarkRed;
                                        Console.WriteLine("!!! У игрока не хватает деняг на аукцион и он не может принять участие !!!");
                                        Console.ForegroundColor = ConsoleColor.Gray;
                                        Thread.Sleep(2000);
                                        break;
                                    }
                                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                                    Console.WriteLine($"Игрок {Users[i].Symbol} принял участие в аукционе");
                                    Console.ForegroundColor = ConsoleColor.Gray;
                                    Thread.Sleep(2000);
                                    user.Add(Users[i]);
                                }
                                break;
                            case 2:
                                {
                                    Console.ForegroundColor = ConsoleColor.DarkRed;
                                    Console.WriteLine($"Игрок {Users[i].Symbol} отказался от участие в аукционе");
                                    Console.ForegroundColor = ConsoleColor.Gray;
                                    Thread.Sleep(2000);
                                }
                                break;
                        }
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine($"Игрок {symbol} не принимает участие в аукционе ");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Thread.Sleep(2000);
                }
            }
            while (isWork)
            {
                if (summaNext >= SummIn.Count)
                {
                    summaNext = 0;
                }
                if (nextPlayer >= user.Count)
                {
                    nextPlayer = 0;
                }
                if (user.Count <= 1)
                {
                    isWork = false;
                    break;
                }
                if (user[nextPlayer].GetType() == typeof(Bot))
                {
                    if (user.Count == 1)
                    {
                        isWork = false;
                        break;
                    }
                    if ((bsnPrice + 100) >= SummIn[summaNext] /*&& user.Count > 1*/)
                    {
                        Console.WriteLine($"Игрок {user[nextPlayer].Symbol} отказался от попкупки бизнеса ");
                        Thread.Sleep(2000);
                        user.RemoveAt(nextPlayer);
                        continue;
                    }
                    Console.WriteLine($"Игрок {user[nextPlayer].Symbol} повышает поднимает ставку {bsnPrice} + 100");
                    Thread.Sleep(2000);
                    bsnPrice += 100;
                }
                else
                {
                    if (!user[nextPlayer].Surrender)
                    {
                        Console.WriteLine($"Ставка игрока {user[nextPlayer].Symbol}");
                        Console.WriteLine($"Цена бизнеса = {bsnPrice}");
                        Console.WriteLine("{ 1 } Поднять ставку | { 2 } отказатся ");
                        Console.Write("{ Ввод } >> ");
                        int.TryParse(Console.ReadLine(), out int choise);
                        while (choise < 1 || choise > 2)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.Write("{ Ввод } >> ");
                            int.TryParse(Console.ReadLine(), out choise);
                            Console.ForegroundColor = ConsoleColor.Gray;
                        }
                        if (choise == 1)
                        {
                            Console.WriteLine($"Игрок {user[nextPlayer].Symbol} поднимает ставку {bsnPrice} + 100");
                            Thread.Sleep(2000);
                            bsnPrice += 100;
                        }
                        else
                        {
                            Console.WriteLine($"Игрок {user[nextPlayer].Symbol} отказался от участия на аукционе");
                            Thread.Sleep(2000);
                            user.Remove(user[nextPlayer]);
                        }
                    }
                }
                nextPlayer++;
                summaNext++;
            }
            if (nextPlayer >= user.Count)
            {
                nextPlayer = 0;
            }
            if (user.Count == 0)
            {
                Console.WriteLine("Нету игроков которые приняли участие в аукционе ");
                Thread.Sleep(2000);
                return;
            }
            if (user[nextPlayer].GetType() == typeof(Bot) || user[nextPlayer].GetType() == typeof(Player))
            {
                if (user[nextPlayer].Balance > bsnPrice)
                {
                    Console.WriteLine($"Игрок {user[nextPlayer].Symbol} покупает бизнес {((Business)bulding).Title} цена {bsnPrice}");
                    Thread.Sleep(2000);
                    user[nextPlayer].Balance -= bsnPrice;
                    ((Business)bulding).BusinessOwner = user[nextPlayer].Symbol;
                }
                else
                {
                    Console.WriteLine($"У игрока {user[nextPlayer].Symbol} не хватило деняг на покупку бизнеса {((Business)bulding).Title}");
                    Thread.Sleep(2000);
                    return;
                }
                #region Test
                //if (bulding.GetType() == typeof(Business))
                //{
                //    if (user[nextPlayer].Balance > ((Business)bulding).Price)
                //    {
                //        Console.WriteLine($"Игрок {user[nextPlayer].Symbol} покупает бизнес {((Business)bulding).Title} цена {bsnPrice}");
                //        Thread.Sleep(2000);
                //        user[nextPlayer].Balance -= ((Business)bulding).Price + bsnPrice;
                //        ((Business)bulding).BusinessOwner = user[nextPlayer].Symbol;
                //    }
                //    else
                //    {
                //        Console.WriteLine($"У игрока {user[nextPlayer].Symbol} не хватило деняг на покупку бизнеса {((Business)bulding).Title}");
                //        Thread.Sleep(2000);
                //        return;
                //    }
                //}
                //if (bulding.GetType() == typeof(CarInterior))
                //{
                //    if (user[nextPlayer].Balance > ((CarInterior)bulding).Price)
                //    {
                //        Console.WriteLine($"Игрок {user[nextPlayer].Symbol} покупает бизнес {((CarInterior)bulding).Title} цена {bsnPrice}");
                //        Thread.Sleep(2000);
                //        user[nextPlayer].Balance -= ((CarInterior)bulding).Price + bsnPrice;
                //        ((CarInterior)bulding).BusinessOwner = user[nextPlayer].Symbol;
                //    }
                //    else
                //    {
                //        Console.WriteLine($"У игрока {user[nextPlayer].Symbol} не хватило деняг на покупку бизнеса {((CarInterior)bulding).Title}");
                //        Thread.Sleep(2000);
                //        return;
                //    }
                //}
                //if (bulding.GetType() == typeof(GamingCompanies))
                //{
                //    if (user[nextPlayer].Balance > ((GamingCompanies)bulding).Price)
                //    {
                //        Console.WriteLine($"Игрок {user[nextPlayer].Symbol} покупает бизнес {((GamingCompanies)bulding).Title} цена {bsnPrice}");
                //        Thread.Sleep(2000);
                //        user[nextPlayer].Balance -= ((GamingCompanies)bulding).Price + bsnPrice;
                //        ((GamingCompanies)bulding).BusinessOwner = user[nextPlayer].Symbol;
                //    }
                //    else
                //    {
                //        Console.WriteLine($"У игрока {user[nextPlayer].Symbol} не хватило деняг на покупку бизнеса {((GamingCompanies)bulding).Title}");
                //        Thread.Sleep(2000);
                //        return;
                //    }
                //}
                #endregion   
            }
        }
        //Method

        //Other
        public void RulesTheGame()
        {

            Console.WriteLine(
                "┌─────────────────────────────────────────────────────────────────────────────────────┐\n" +
                "│ Правила игры просты: игроки поочередно бросают кубики и делают соответствующее      │\n" +
                "│ количество ходов на игральном поле (если на кубиках выпали одинаковые числа, игрок  │\n" +
                "│ получает право на ещё один ход). Встав на поле с фирмой, игрок может приобрести её, │\n" +
                "│ если фирма свободна; а если фирма принадлежит другому игроку, то игрок обязан       │\n" +
                "│ заплатить за посещение данного поля аренду по установленному правилами              │\n" +
                "│ прейскуранту (сумма аренды указывается на ярлычке поля). При посещении поля с       │\n" +
                "│ событиями игрок получает указание следовать выпавшему ему событию (Например,        │\n" +
                "│ получить деньги, заплатить штраф, или отправиться в тюрьму).                        │\n" +
                "└─────────────────────────────────────────────────────────────────────────────────────┘");


            Console.WriteLine(
                "┌───────────────────────────────────────────────────────────────────────────────────┐\n" +
                "│ В игре все фирмы относятся к различным отраслям (например, парфюмерия, авиалинии  │\n" +
                "│ или электроника), в одной отрасли может быть от двух до четырёх фирм. На игровом  │\n" +
                "│ поле фирмы одной отрасли, как правило, расположены рядом и имеют ярлычок одного   │\n" +
                "│ цвета. Игрок, владеющий всеми фирмами одной отрасли, становится монополистом, что │\n" +
                "│ даёт ему право вкладывать свои деньги в постройку филиалов, что увеличивает       │\n" +
                "│ стоимость попадания противника на поле. Таким образом, каждый игрок для победы    │\n" +
                "│ должен стараться приобрести не просто поля, а поля одной отрасли, чтобы иметь     │\n" +
                "│ возможность развиться.                                                            │\n" +
                "└───────────────────────────────────────────────────────────────────────────────────┘");

            Console.WriteLine(
                "┌───────────────────────────────────────────────────────────────────────────────────┐\n" +
                "│ Игрок может обменивать свои поля на чужие у других игроков, при этом, конечно,    │\n" +
                "│ получатель предложения может отказаться от вашей сделки. Помните, выгодная сделка │\n" +
                "│ увеличивает ваш шанс на победу.                                                   │\n" +
                "└───────────────────────────────────────────────────────────────────────────────────┘");

            Console.WriteLine(
                "┌──────────────────────────────────────────────────────────────────────────────────────┐\n" +
                "│ Игрок может оказаться в тюрьме, например, попав на поле \"Полиция\" или выбросив три   │\n" +
                "│ дубля подряд. Выйти из тюрьмы можно либо заплатив деньги, либо выбросив дубль (на    │\n" +
                "│ этот вариант даётся три попытки).                                                    │\n" +
                "└──────────────────────────────────────────────────────────────────────────────────────┘\n");
            Console.Write("Нажмите любую кнопку что бы выйти ... ");
        }//Правила Игры
        public void BotsBusinessDownturn(List<Building> building) //fix логика спада бизнесов
        {
            const int numberLaps = 0;
            for (int i = 0; i < building.Count; i++)
            {
                if (building[i].GetType() == typeof(Business))
                {
                    if (((Business)building[i]).BusinessDowntrun > numberLaps)
                    {
                        ((Business)building[i]).BusinessDowntrun -= 1;
                    }
                    else
                    {
                        ((Business)building[i]).Mortgaged = false;
                        ((Business)building[i]).BusinessDowntrun = 15;
                        ((Business)building[i]).BusinessOwner = '0';
                    }
                }
                if (building[i].GetType() == typeof(CarInterior))
                {
                    if (((CarInterior)building[i]).BusinessDowntrun > numberLaps)
                    {
                        ((CarInterior)building[i]).BusinessDowntrun -= 1;
                    }
                    else
                    {
                        ((CarInterior)building[i]).Mortgaged = false;
                        ((CarInterior)building[i]).BusinessDowntrun = 15;
                        ((CarInterior)building[i]).BusinessOwner = '0';
                    }
                }
                if (building[i].GetType() == typeof(GamingCompanies))
                {
                    if (((GamingCompanies)building[i]).BusinessDowntrun > numberLaps)
                    {
                        ((GamingCompanies)building[i]).BusinessDowntrun -= 1;
                    }
                    else
                    {
                        ((GamingCompanies)building[i]).Mortgaged = false;
                        ((GamingCompanies)building[i]).BusinessDowntrun = 15;
                        ((GamingCompanies)building[i]).BusinessOwner = '0';
                    }

                }
            }
        }
        public List<Building> AllMortagagedBusinesses(List<Building> building)
        {
            List<Building> result = new List<Building>();
            for (int i = 0; i < building.Count; i++)
            {
                if (building[i].GetType() == typeof(Business) && ((Business)building[i]).Mortgaged)
                {
                    result.Add(building[i]);
                }
                if (building[i].GetType() == typeof(CarInterior) && ((CarInterior)building[i]).Mortgaged)
                {
                    result.Add(building[i]);
                }
                if (building[i].GetType() == typeof(GamingCompanies) && ((GamingCompanies)building[i]).Mortgaged)
                {
                    result.Add(building[i]);
                }
            }
            return result;
        }//все заложенные бизнесы 
        public void AddBot(Bot Bot)
        {
            Users.Add(Bot);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Бот был добавлен в игру ");
            Console.ForegroundColor = ConsoleColor.Gray;
            Thread.Sleep(500);
        }//Добовление Бота
        public void AddPlayer(Player player)
        {
            Users.Add(player);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Игрок был добавлен в игру ");
            Console.ForegroundColor = ConsoleColor.Gray;
            Thread.Sleep(500);
        }//Добовление Игрока
        public bool DeletUser(string name)
        {
            for (int i = 0; i < Users.Count; i++)
            {
                if (Users[i].Name == name)
                {
                    Users.RemoveAt(i);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Плеер был удалён из игры");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Thread.Sleep(500);
                    return true;
                }
            }
            return false;
        }//Удаление Игроков
        public void DeletAllUser()
        {
            Users.Clear();
        }//удаление всех игроков 
        public void ClearingField(List<User> users, List<Building> buildings)
        {
            for (int i = 0; i < users.Count; i++)
            {
                users[i].CordinationPlayer = 0;
                for (int j = 0; j < buildings.Count; j++)
                {
                    if (buildings[j].GetType() == typeof(Business))
                    {
                        if (((Business)buildings[j]).BusinessOwner == users[i].Symbol)
                        {
                            ((Business)buildings[j]).BusinessOwner = ' ';
                        }
                    }
                    if (buildings[j].GetType() == typeof(CarInterior))
                    {
                        if (((CarInterior)buildings[j]).BusinessOwner == users[i].Symbol)
                        {
                            ((CarInterior)buildings[j]).BusinessOwner = ' ';
                        }
                    }
                    if (buildings[j].GetType() == typeof(GamingCompanies))
                    {
                        if (((GamingCompanies)buildings[j]).BusinessOwner == users[i].Symbol)
                        {
                            ((GamingCompanies)buildings[j]).BusinessOwner = ' ';

                        }
                    }
                    else
                    {
                        buildings[j].Symbol.Clear();
                    }
                }
            }
        }//очистка поля 
        //AddDelet

        //Show
        public void ShowBot()
        {
            //List<User> bots = Users.Where(x => x.GetType().Equals(typeof(Bot))).ToList();
            for (int i = 0; i < Users.Count; i++)
            {
                if (Users[i].GetType() == typeof(Bot))
                {
                    Users[i].Show();
                }
            }
        }//Вывод Ботов
        public void ShowPlayer()
        {
            for (int i = 0; i < Users.Count; i++)
            {
                if (Users[i].GetType() == typeof(Player))
                {
                    Users[i].Show();
                }
            }

        }//Вывод Игроков
        public void ShowField(string text)
        {
            Field.ShowAllField(Users, text);
        }//Вывод Поля
        public void ShowGameMenu()
        {
            Console.WriteLine($"{{{(int)GameMenu.ThrowCubes}}} Кинуть кубики ");
            Console.WriteLine($"{{{(int)GameMenu.SellTheBusiness}}} Заложить Бизнес ");
            Console.WriteLine($"{{{(int)GameMenu.BuyOutYourBusiness}}} Выкупить свой бизнес ");
            Console.WriteLine($"{{{(int)GameMenu.QuitTheTrade}}} Предложить трейд игроку ");
            Console.WriteLine($"{{{(int)GameMenu.PurchaseBranch}}} Покупка филиала ");
            Console.WriteLine($"{{{(int)GameMenu.SellBranch}}} Продажа филиала ");
            Console.WriteLine($"{{{(int)GameMenu.Surrender}}} Сдатся ");
        }//Вывод игрового меню
        public void ShowGameCube(int numberCube)
        {
            if (numberCube == 1)
            {
                Console.WriteLine(
                    "┌─────────┐\n" +
                    "│         │\n" +
                    "│    ●    │\n" +
                    "│         │\n" +
                    "└─────────┘");
            }
            if (numberCube == 2)
            {
                Console.WriteLine(
                     "┌─────────┐\n" +
                     "│       ● │\n" +
                     "│         │\n" +
                     "│ ●       │\n" +
                     "└─────────┘");
            }
            if (numberCube == 3)
            {
                Console.WriteLine(
                     "┌─────────┐\n" +
                     "│      ●  │\n" +
                     "│    ●    │\n" +
                     "│  ●      │\n" +
                     "└─────────┘");
            }
            if (numberCube == 4)
            {
                Console.WriteLine(
                  "┌─────────┐\n" +
                  "│ ●     ● │\n" +
                  "│         │\n" +
                  "│ ●     ● │\n" +
                  "└─────────┘");
            }
            if (numberCube == 5)
            {
                Console.WriteLine(
                    "┌─────────┐\n" +
                    "│  ●   ●  │\n" +
                    "│    ●    │\n" +
                    "│  ●   ●  │\n" +
                    "└─────────┘");
            }
            if (numberCube == 6)
            {
                Console.WriteLine(
                    "┌─────────┐\n" +
                    "│  ●   ●  │\n" +
                    "│  ●   ●  │\n" +
                    "│  ●   ●  │\n" +
                    "└─────────┘");
            }
        }//Вывыод кубиков
        public void ShowPayMenu(string text, int number)
        {
            if (number == 0)
            {
                Console.WriteLine($"{{{(int)BuyMenu.BuyBsn}}} {text}");
                Console.WriteLine($"{{{(int)BuyMenu.MortagageBsn}}} Заложить бизнес ");
                Console.WriteLine($"{{{(int)BuyMenu.BranchSale}}} Продать филиал ");
                Console.WriteLine($"{{{(int)BuyMenu.Auction}}} Отказатся от покупки   ");
                Console.WriteLine($"{{{(int)BuyMenu.Surrender}}} Сдаться");
            }
            else if (number == 1)
            {
                Console.WriteLine($"{{{(int)PayMenu.RentPayment}}} {text}");
                Console.WriteLine($"{{{(int)PayMenu.MortagageBsn}}} Заложить бизнес ");
                Console.WriteLine($"{{{(int)PayMenu.BranchSale}}} Продать филиал ");
                Console.WriteLine($"{{{(int)PayMenu.Surrender}}} Сдаться");
            }
            else if (number == 2)
            {
                Console.WriteLine($"{{{1}}} {text} ");
                Console.WriteLine($"{{{(int)BuyMenu.MortagageBsn}}} Заложить бизнес ");
                Console.WriteLine($"{{{(int)BuyMenu.BranchSale}}} Продать филиал ");
                Console.WriteLine($"{{{(int)PayMenu.Surrender}}} Сдаться");
            }
        } // вывод меню выплаты
        public void ShowWinGame()
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine(
                "       ,--.                                             ,--.         \n" +
                " ,---. |  | ,--,--.,--. ,--.,---. ,--.--.    ,--.   ,--.`--',--,--,  \n" +
                "| .-. ||  |' ,-.  | \\  '  /| .-. :|  .--'    |  |.'.|  |,--.|      \\ \n" +
                "| '-' '|  |\\ '-'  |  \\   ' \\   --.|  |       |   .'.   ||  ||  ||  | \n" +
                "|  |-' `--' `--`--'.-'  /   `----'`--'       '--'   '--'`--'`--''--' \n" +
                "`--'               `---'                                             \n");
            Console.ForegroundColor = ConsoleColor.Gray;
        }// вывод картинки выигрыша 
        //Show
        public void StartGame()
        {
            for (int i = 0; i < Users.Count; i++)
            {
                Field.Buldings[0].Symbol.Add(Users[i].Symbol);
            }
            //Users[1].Balance -= 14940;
            //Users[2].Balance -= 9000;
            //Users[3].Balance -= 7500;
            //Users[2].Balance = 4920;
            //Users[3].Balance = 3520;
            //((Business)Field.Buldings[6]).BusinessOwner = Users[1].Symbol;
            //((Business)Field.Buldings[8]).BusinessOwner = Users[1].Symbol;
            //((Business)Field.Buldings[39]).BusinessOwner = Users[1].Symbol;
            //((Business)Field.Buldings[37]).BusinessOwner = Users[1].Symbol;
            //((Business)Field.Buldings[39]).Level = 1;
            //((Business)Field.Buldings[29]).Level = 5;
            //((Business)Field.Buldings[27]).Level = 5;
            //((Business)Field.Buldings[26]).Level = 5;

            ((Business)Field.Buldings[14]).BusinessOwner = Users[0].Symbol;
            //((Business)Field.Buldings[13]).BusinessOwner = Users[3].Symbol;
            //((Business)Field.Buldings[39]).BusinessOwner = Users[1].Symbol;
            //((Business)Field.Buldings[39]).Level = 1;
            //((Business)Field.Buldings[37]).BusinessOwner = Users[1].Symbol;
            //((Business)Field.Buldings[23]).BusinessOwner = Users[1].Symbol;
            //((Business)Field.Buldings[24]).BusinessOwner = Users[1].Symbol;
            //((Business)Field.Buldings[19]).BusinessOwner = Users[0].Symbol;
            #region TestBot
            //Users[1].Balance -= 15000;
            //Users[2].Balance -= 14500;
            //Users[3].Balance -= 11000;

            //((Business)Field.Buldings[29]).BusinessOwner = Users[1].Symbol;
            //((Business)Field.Buldings[27]).BusinessOwner = Users[1].Symbol;
            //((Business)Field.Buldings[26]).BusinessOwner = Users[1].Symbol;
            //((Business)Field.Buldings[3]).BusinessOwner = Users[0].Symbol;
            //((Business)Field.Buldings[1]).Mortgaged = true;
            //((Business)Field.Buldings[3]).Mortgaged = true;
            #endregion
            Random rand = new Random();
            int maxFieldCount = 40;
            int prisonPrice = 500;
            int numberCell;
            int nextPlayer = 0;
            int firstCube = 0;
            int secondCube = 0;
            int luck = 0;
            bool surrender = false;
            bool skipping = false;
            bool opportunityEnter = false;
            bool Jackpot = false;
            bool menu = true;
            bool CheckTeleportActionTrue = false;
            int prisonSumm = 0;
            PayMenu payMenu;
            BuyMenu buyMenu;
            TaxMenu taxMenu;
            while (true)
            {
                if (IsCheckWinGame())
                {
                    Console.Write("Нажмите любую кнопку что бы вернуться на начальную страницу >>> ");
                    Console.ReadLine();
                    ClearingField(Users, Field.Buldings);
                    DeletAllUser();
                    break;
                }
                opportunityEnter = false;
                if (Users.Count - 1 == nextPlayer)
                {
                    BotsBusinessDownturn(AllMortagagedBusinesses(Field.Buldings));
                }
                #region TestCodeBag
                //if (Users[nextPlayer].GetType() == typeof(Bot))
                //{
                //    ((Bot)Users[nextPlayer]).SurrenderLogic(Field.Buldings);
                //}
                #endregion
                if (Users[nextPlayer].Surrender == true)
                {
                    if (nextPlayer >= Users.Count)
                    {
                        nextPlayer = 0;
                    }
                    else
                    {
                        if (nextPlayer + 1 >= Users.Count)
                        {
                            nextPlayer = 0;
                        }
                        else
                        {
                            nextPlayer++;
                        }
                    }
                }
                if (Users[nextPlayer].ReverseStroke == true)
                {
                    skipping = true;
                }
                if (Users[nextPlayer].GetType() == typeof(Bot))
                {
                    int luckBot = 0;
                    bool check = true;
                    while (check)
                    {
                        //((Bot)Users[nextPlayer]).SurrenderLogic(Field.Buldings);
                        if (Users[nextPlayer].StepSkip)
                        {
                            Console.WriteLine($"Игрок {Users[nextPlayer].Symbol} пропускает ход ");
                            Users[nextPlayer].StepSkip = false;
                            Thread.Sleep(2000);
                            check = false;
                            continue;
                        }
                        if (Users[nextPlayer].Prison == true)
                        {
                            if (Users[nextPlayer].Balance >= prisonPrice)
                            {
                                Users[nextPlayer].Balance -= prisonPrice;
                                Users[nextPlayer].Prison = false;
                                Console.WriteLine($"Игрок {Users[nextPlayer].Symbol} попал в тюрьму и решил заплатить за выход {prisonPrice}");
                                Thread.Sleep(2000);
                                if (luckBot == 1)
                                {
                                    break;
                                }
                            }
                            else
                            {
                                //((Bot)Users[nextPlayer]).MortagageBusiness(((Bot)Users[nextPlayer]).BotBusinesses(Field.Buldings), Users, Field.Buldings);
                                firstCube = RollTheCube(rand);
                                secondCube = RollTheCube(rand);
                                if (firstCube == secondCube)
                                {
                                    Users[nextPlayer].Prison = false;
                                }
                                //Users[nextPlayer].Surrender = true;
                            }
                        }
                        if (((Bot)Users[nextPlayer]).IsHaveMeMonoopoly(Field.Buldings))
                        {
                            ((Bot)Users[nextPlayer]).MonoopolyImprovement1(((Bot)Users[nextPlayer]).MonoopolyImprovement(Field.Buldings));
                        }
                        //Console.Clear();
                        ShowField("");
                        ((Bot)Users[nextPlayer]).BusinessBuyout(((Bot)Users[nextPlayer]).AllMortagagedBusinesses(Field.Buldings));
                        firstCube = RollTheCube(rand);
                        secondCube = RollTheCube(rand);
 
                        //if (test == 0)
                        //{
                        //    firstCube = 39;
                        //    secondCube = 0;
                        //    test = 1;
                        //}
                        //else if (test == 1)
                        //{
                        //    firstCube = 7;
                        //    secondCube = 0;
                        //}
                        #region Test
                        //if (t == 0)
                        //{
                        //    firstCube = 4;
                        //    secondCube = 4;
                        //}
                        //else if (t == 1)
                        //{
                        //    firstCube = 2;
                        //    secondCube = 2;
                        //}
                        //else if (t == 2)
                        //{
                        //    firstCube = 3;
                        //    secondCube = 3;
                        //}
                        //else
                        //{
                        //    firstCube = RollTheCube(rand);
                        //    secondCube = RollTheCube(rand);
                        //}
                        #endregion
                        ShowGameCube(firstCube);
                        ShowGameCube(secondCube);
                        if (firstCube != secondCube)
                        {
                            check = false;
                            luckBot = 0;
                        }
                        else
                        {
                            luckBot++;
                        }
                        if (luckBot == 3)
                        {
                            Console.WriteLine($"У игрока {Users[nextPlayer].Symbol} выпал дубль 3 раза и он попадет в тюрьму ");
                            ((Bot)Users[nextPlayer]).Prison = true;
                            Field.Buldings[Users[nextPlayer].CordinationPlayer].Symbol.Remove(Users[nextPlayer].Symbol);
                            Field.Buldings[Users[nextPlayer].CordinationPlayer].Symbol.Add(Users[nextPlayer].Symbol);
                            check = false;
                            Thread.Sleep(2000);
                            break;
                        }
                        Console.WriteLine($"Ход игрока {Users[nextPlayer].Symbol}");
                        Thread.Sleep(2000);
                        if (Users[nextPlayer].ReverseStroke == true)
                        {
                            Field.Buldings[Users[nextPlayer].CordinationPlayer].Symbol.Remove(Users[nextPlayer].Symbol);
                            if (Users[nextPlayer].CordinationPlayer - (firstCube + secondCube) + Field.Buldings.Count > Field.Buldings.Count)
                            {
                                Field.Buldings[Users[nextPlayer].CordinationPlayer - (firstCube + secondCube)].Symbol.Add(Users[nextPlayer].Symbol);
                                Users[nextPlayer].CordinationPlayer -= firstCube + secondCube;
                            }
                            else
                            {
                                Field.Buldings[Users[nextPlayer].CordinationPlayer - (firstCube + secondCube) + Field.Buldings.Count].Symbol.Add(Users[nextPlayer].Symbol);
                                Users[nextPlayer].CordinationPlayer -= firstCube + secondCube;
                                Users[nextPlayer].CordinationPlayer += Field.Buldings.Count;
                            }
                            ((Bot)Users[nextPlayer]).CheckCell(Field.Buldings[Users[nextPlayer].CordinationPlayer], Users, Field, firstCube, secondCube);
                            Users[nextPlayer].ReverseStroke = false;
                        }
                        else
                        {
                            if ((Users[nextPlayer].CordinationPlayer + firstCube + secondCube) >= Field.Buldings.Count)
                            {

                                Field.Buldings[Users[nextPlayer].CordinationPlayer].Symbol.Remove(Users[nextPlayer].Symbol);
                                Users[nextPlayer].CordinationPlayer += firstCube + secondCube - Field.Buldings.Count;
                                Field.Buldings[Users[nextPlayer].CordinationPlayer].Symbol.Add(Users[nextPlayer].Symbol);
                                Users[nextPlayer].NumberOfLaps += 1;
                                if (Users[nextPlayer].NumberOfLaps != 45)
                                {
                                    Users[nextPlayer].Balance += 2000;
                                    Console.WriteLine($"Бот {Users[nextPlayer].Symbol} прошел круг и получает 2000");
                                    Thread.Sleep(2000);
                                }
                                ((Bot)Users[nextPlayer]).CheckCell(Field.Buldings[Users[nextPlayer].CordinationPlayer], Users, Field, firstCube, secondCube);

                            }//если игрок вышел за приделы поля 
                            else
                            {

                                Field.Buldings[Users[nextPlayer].CordinationPlayer + firstCube + secondCube].Symbol.Add(Users[nextPlayer].Symbol);
                                Field.Buldings[Users[nextPlayer].CordinationPlayer].Symbol.Remove(Users[nextPlayer].Symbol);
                                Users[nextPlayer].CordinationPlayer += firstCube + secondCube;
                                ((Bot)Users[nextPlayer]).CheckCell(Field.Buldings[Users[nextPlayer].CordinationPlayer], Users, Field, firstCube, secondCube);

                            }//если игрок дивгается по пределам поля 
                        }
                        if (((Bot)Users[nextPlayer]).Auction)
                        {
                            Console.WriteLine($"У бота {Users[nextPlayer].Symbol} нехватило деняг и он отправляет бизнес {Field.Buldings[Users[nextPlayer].CordinationPlayer].Title} на аукцион");
                            Thread.Sleep(2000);
                            Auction(Field.Buldings[Users[nextPlayer].CordinationPlayer], Users[nextPlayer].Symbol);
                            ((Bot)Users[nextPlayer]).Auction = false;
                        }
                        ShowField($"Бот кинул кубики число первого кубика [{firstCube}] число второго кубика [{secondCube}]");
                    }
                } //fix 
                else
                {
                    GameMenu gameMenu;
                    bool check = true;
                    #region TestJacpot
                    //if (Users[nextPlayer].Jackpot == true)
                    //{
                    //    int number = 0;
                    //    Console.WriteLine("{ 1 } Сыграть в джекпот | { 2 } Отказатся от игры ");
                    //    Console.Write("{ Ввод } >> ");
                    //    int.TryParse(Console.ReadLine(), out int choise);
                    //    if (choise == 1)
                    //    {
                    //        Users[nextPlayer].Balance -= 1000;
                    //        bool checkJack = false;
                    //        do
                    //        {
                    //            Console.WriteLine(" Введите количество кубиков не больше 3 ");
                    //            int.TryParse(Console.ReadLine(), out number);
                    //        }
                    //        while (number < 1 || number > 4);
                    //        int[] arrayCubs = new int[number];
                    //        for (int i = 0; i < arrayCubs.Length; i++)
                    //        {
                    //            do
                    //            {
                    //                Console.WriteLine($"Введите число от 1 до 6 в ячейку номер {i}");
                    //                Console.Write("{ Ввод } >> ");
                    //                int.TryParse(Console.ReadLine(), out number);
                    //            }
                    //            while (number < 1 && number > 6);
                    //            for (int j = 0; j < arrayCubs.Length; j++)
                    //            {
                    //                if (arrayCubs[i] == number)
                    //                {
                    //                    checkJack = true;
                    //                    break;
                    //                }
                    //                else
                    //                {
                    //                    checkJack = false;
                    //                }
                    //            }

                    //            arrayCubs[i] = number;
                    //        }//проверка ввода чисел
                    //        firstCube = RollTheCube(rand);
                    //        for (int i = 0; i < arrayCubs.Length; i++)
                    //        {
                    //            if (arrayCubs[i] == firstCube)
                    //            {
                    //                if (arrayCubs.Length == 3)
                    //                {
                    //                    Users[nextPlayer].Balance += 2000;
                    //                    Console.WriteLine($"Игрок {Users[nextPlayer].Symbol} выиграл 2000 ");
                    //                    Thread.Sleep(2000);
                    //                }
                    //                else if (arrayCubs.Length == 2)
                    //                {
                    //                    Users[nextPlayer].Balance += 3000;
                    //                    Console.WriteLine($"Игрок {Users[nextPlayer].Symbol} выиграл 3000 ");
                    //                    Thread.Sleep(2000);
                    //                }
                    //                else if (arrayCubs.Length == 1)
                    //                {
                    //                    Users[nextPlayer].Balance += 6000;
                    //                    Console.WriteLine($"Игрок {Users[nextPlayer].Symbol} выиграл 6000 ");
                    //                    Thread.Sleep(2000);
                    //                }
                    //                break;
                    //            }
                    //        }//проверка выигрыша
                    //    }
                    //    Users[nextPlayer].Jackpot = false;

                    //}
                    #endregion
                    while (check)
                    {
                        if (Users[nextPlayer].StepSkip)
                        {
                            Console.WriteLine($"Игрок {Users[nextPlayer].Symbol} пропускает ход ");
                            Users[nextPlayer].StepSkip = false;
                            Thread.Sleep(2000);
                            check = false;
                            continue;
                        }
                        ShowField("");
                        ShowGameMenu();
                        Console.Write("{ Ввод } > ");
                        Enum.TryParse(Console.ReadLine(), out gameMenu);
                        switch (gameMenu)
                        {
                            case GameMenu.ThrowCubes://кинуть кубики 
                                {
                                    if (((Player)Users[nextPlayer]).Prison == true)
                                    {
                                        Console.WriteLine(" { 1 } Кинуть кубики | { 2 } Заплатить 500");
                                        Console.Write("{ Ввод } > ");
                                        int.TryParse(Console.ReadLine(), out int number);
                                        if (number == 1)
                                        {
                                            if (prisonSumm == 3)
                                            {
                                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                                Console.WriteLine("!!! Больше нельзя кидать кубики надо заплатить что бы выйти из тюрьмы !!!");
                                                Console.ForegroundColor = ConsoleColor.Gray;
                                                Thread.Sleep(2000);
                                                continue;
                                            }
                                            firstCube = RollTheCube(rand);
                                            secondCube = RollTheCube(rand);
                                            ShowGameCube(firstCube);
                                            ShowGameCube(secondCube);
                                            Thread.Sleep(2000);
                                            if (firstCube == secondCube)
                                            {
                                                Field.Buldings[Users[nextPlayer].CordinationPlayer].Symbol.Remove(Users[nextPlayer].Symbol);
                                                ((Player)Users[nextPlayer]).Prison = false;
                                                continue;
                                            }
                                            else
                                            {
                                                prisonSumm += 1;
                                                check = false;
                                                break;
                                            }
                                        }
                                        else if (number == 2)
                                        {
                                            if (Field.Buldings[((Player)Users[nextPlayer]).CordinationPlayer].GetType() == typeof(Prison))
                                            {
                                                //Field.Buldings[Users[nextPlayer].CordinationPlayer].Symbol.Remove(Users[nextPlayer].Symbol);
                                                ((Player)Users[nextPlayer]).Balance -= ((Prison)Field.Buldings[((Player)Users[nextPlayer]).CordinationPlayer]).ExitCost;
                                                ((Player)Users[nextPlayer]).Prison = false;
                                                prisonSumm = 0;
                                                continue;
                                            }
                                        }
                                        else
                                        {
                                            Console.ForegroundColor = ConsoleColor.DarkRed;
                                            Console.WriteLine("Неверное значение для выбора !");
                                            Console.ForegroundColor = ConsoleColor.Gray;
                                        }
                                    }
                                    else
                                    {
                                        firstCube = RollTheCube(rand);
                                        secondCube = RollTheCube(rand);
                                        //firstCube = 2;
                                        //secondCube = 0;
                                        //if (t == 0)
                                        //{
                                        //    firstCube = 39;
                                        //    secondCube = 0;
                                        //    t += 1;
                                        //}
                                        //else if(t == 1)
                                        //{
                                        //    firstCube = 6;
                                        //    secondCube = 6;
                                        //}
                                        ShowGameCube(firstCube);
                                        ShowGameCube(secondCube);
                                        Thread.Sleep(2000);
                                        #region Test
                                        //if (nextPlayer == 1)
                                        //{
                                        //    if (test == 0)
                                        //    {
                                        //        firstCube = 5;
                                        //        secondCube = 0;
                                        //    }
                                        //    else if (test == 1)
                                        //    {
                                        //        firstCube = 10;
                                        //        secondCube = 0;
                                        //    }
                                        //    else if (test == 2)
                                        //    {
                                        //        firstCube = 10;
                                        //        secondCube = 0;
                                        //    }
                                        //    else if (test == 3)
                                        //    {
                                        //        firstCube = 10;
                                        //        secondCube = 0;
                                        //    }
                                        //    test++;
                                        //}
                                        #endregion
                                        if (firstCube == secondCube)
                                        {
                                            luck++;
                                            check = true;
                                            opportunityEnter = false;
                                        }
                                        else
                                        {
                                            luck = 0;
                                            check = false;
                                        }
                                        if (luck == 3)
                                        {
                                            Console.WriteLine($"Игроку {Users[nextPlayer].Symbol} 3 раза выпал дубль и он попадает в тюрьму");
                                            ((Player)Users[nextPlayer]).Prison = true;
                                            Thread.Sleep(2000);
                                            check = false;
                                            break;
                                        }
                                    }
                                    if (((Player)Users[nextPlayer]).Surrender != true && ((Player)Users[nextPlayer]).Prison != true && ((Player)Users[nextPlayer]).StepSkip != true)
                                    {
                                    teleport:
                                        if (Users[nextPlayer].ReverseStroke == true)
                                        {
                                            Field.Buldings[Users[nextPlayer].CordinationPlayer].Symbol.Remove(Users[nextPlayer].Symbol);
                                            if (Users[nextPlayer].CordinationPlayer - (firstCube + secondCube) + Field.Buldings.Count > Field.Buldings.Count)
                                            {
                                                Field.Buldings[Users[nextPlayer].CordinationPlayer - (firstCube + secondCube)].Symbol.Add(Users[nextPlayer].Symbol);
                                                Users[nextPlayer].CordinationPlayer -= firstCube + secondCube;
                                            }
                                            else
                                            {
                                                Field.Buldings[Users[nextPlayer].CordinationPlayer - (firstCube + secondCube) + Field.Buldings.Count].Symbol.Add(Users[nextPlayer].Symbol);
                                                Users[nextPlayer].CordinationPlayer -= firstCube + secondCube;
                                                Users[nextPlayer].CordinationPlayer += Field.Buldings.Count;
                                            }
                                            ((Player)Users[nextPlayer]).CheckCell(Field.Buldings[Users[nextPlayer].CordinationPlayer], Field);
                                            Users[nextPlayer].ReverseStroke = false;
                                        }
                                        else
                                        {
                                            if ((Users[nextPlayer].CordinationPlayer + firstCube + secondCube) >= Field.Buldings.Count)
                                            {

                                                Field.Buldings[Users[nextPlayer].CordinationPlayer].Symbol.Remove(Users[nextPlayer].Symbol);
                                                Users[nextPlayer].CordinationPlayer += firstCube + secondCube - Field.Buldings.Count;
                                                Field.Buldings[Users[nextPlayer].CordinationPlayer].Symbol.Add(Users[nextPlayer].Symbol);
                                                Users[nextPlayer].NumberOfLaps += 1;
                                                if (Users[nextPlayer].NumberOfLaps != 45)
                                                {
                                                    Users[nextPlayer].Balance += 2000;
                                                    Console.WriteLine($"Игрок {Users[nextPlayer].Symbol} прошел круг и получает 2000");
                                                    Thread.Sleep(2000);
                                                }
                                                ((Player)Users[nextPlayer]).CheckCell(Field.Buldings[Users[nextPlayer].CordinationPlayer], Field);

                                            }//если игрок вышел за приделы поля 
                                            else
                                            {

                                                Field.Buldings[Users[nextPlayer].CordinationPlayer + firstCube + secondCube].Symbol.Add(Users[nextPlayer].Symbol);
                                                Field.Buldings[Users[nextPlayer].CordinationPlayer].Symbol.Remove(Users[nextPlayer].Symbol);
                                                Users[nextPlayer].CordinationPlayer += firstCube + secondCube;
                                                ((Player)Users[nextPlayer]).CheckCell(Field.Buldings[Users[nextPlayer].CordinationPlayer], Field);

                                            }//если игрок дивгается по пределам поля 
                                        }
                                        //Console.Clear();
                                        ShowField("");
                                        if (Users[nextPlayer].Jackpot == true)
                                        {
                                            bool intermediate = true;
                                            int number = 0;
                                            Console.WriteLine("{ 1 } Сыграть в джекпот | { 2 } Отказатся от игры ");
                                            Console.Write("{ Ввод } >> ");
                                            int.TryParse(Console.ReadLine(), out int choise);
                                            if (choise == 1)
                                            {
                                                Users[nextPlayer].Balance -= 1000;
                                                bool checkJack = false;
                                                do
                                                {
                                                    Console.WriteLine(" Введите количество кубиков не больше 3 ");
                                                    Console.Write("{ Ввод } >> ");
                                                    int.TryParse(Console.ReadLine(), out number);
                                                }
                                                while (number < 1 || number > 3);
                                                int[] arrayCubs = new int[number];
                                                for (int i = 0; i < arrayCubs.Length; i++)
                                                {
                                                    do
                                                    {
                                                        if (intermediate)
                                                        {
                                                            do
                                                            {
                                                                Console.WriteLine($"Введите число от 1 до 6 в ячейку номер {i}");
                                                                Console.Write("{ Ввод } >> ");
                                                                int.TryParse(Console.ReadLine(), out number);
                                                            }
                                                            while (number < 1 || number > 6);
                                                            intermediate = false;
                                                        }
                                                        for (int j = 0; j < arrayCubs.Length; j++)
                                                        {
                                                            if (arrayCubs[j] == number)
                                                            {
                                                                j = 0;
                                                                intermediate = true;
                                                                break;

                                                            }
                                                        }
                                                        if (!intermediate)
                                                        {
                                                            arrayCubs[i] = number;
                                                            Console.WriteLine($"Игрок {Users[nextPlayer].Symbol} ввел число {arrayCubs[i]} в ячейке {i}");
                                                            Thread.Sleep(2000);
                                                        }
                                                    }
                                                    while (intermediate);
                                                }//проверка ввода чисел
                                                firstCube = RollTheCube(rand);
                                                Console.WriteLine($"В джекпоте рандомно кинулся кубик и выпало число {firstCube}");
                                                Thread.Sleep(2000);
                                                for (int i = 0; i < arrayCubs.Length; i++)
                                                {
                                                    if (arrayCubs[i] == firstCube)
                                                    {
                                                        if (arrayCubs.Length == 3)
                                                        {
                                                            Users[nextPlayer].Balance += 2000;
                                                            Console.WriteLine($"Игрок {Users[nextPlayer].Symbol} выиграл 2000 ");
                                                            Thread.Sleep(2000);
                                                            Jackpot = true;
                                                        }
                                                        else if (arrayCubs.Length == 2)
                                                        {
                                                            Users[nextPlayer].Balance += 3000;
                                                            Console.WriteLine($"Игрок {Users[nextPlayer].Symbol} выиграл 3000 ");
                                                            Thread.Sleep(2000);
                                                            Jackpot = true;
                                                        }
                                                        else if (arrayCubs.Length == 1)
                                                        {
                                                            Users[nextPlayer].Balance += 6000;
                                                            Console.WriteLine($"Игрок {Users[nextPlayer].Symbol} выиграл 6000 ");
                                                            Thread.Sleep(2000);
                                                            Jackpot = true;
                                                        }
                                                        break;
                                                    }
                                                }//проверка выигрыша
                                                if (!Jackpot)
                                                {
                                                    Console.ForegroundColor = ConsoleColor.DarkRed;
                                                    Console.WriteLine($"Игрок {Users[nextPlayer].Symbol} не выиграл ");
                                                    Console.ForegroundColor = ConsoleColor.Gray;
                                                    Thread.Sleep(2000);
                                                    Users[nextPlayer].Jackpot = false;
                                                    break;
                                                }
                                            }
                                            Users[nextPlayer].Jackpot = false;

                                        }
                                        if (((Player)Users[nextPlayer]).IsCheckCellNotBsn(Field.Buldings[Users[nextPlayer].CordinationPlayer]))
                                        {
                                            if (Field.Buldings[Users[nextPlayer].CordinationPlayer].GetType() == typeof(Bank))
                                            {
                                                while (menu)
                                                {
                                                    ShowPayMenu("Выплатить налог", 1);
                                                    Console.WriteLine($"Цена снятия составляет {((Bank)Field.Buldings[Users[nextPlayer].CordinationPlayer]).Summa}");
                                                    Console.Write("{ Ввод } > ");
                                                    Enum.TryParse(Console.ReadLine(), out taxMenu);
                                                    switch (taxMenu)
                                                    {
                                                        case TaxMenu.TaxPayment:
                                                            {
                                                                if (((Player)Users[nextPlayer]).Balance > ((Bank)Field.Buldings[Users[nextPlayer].CordinationPlayer]).Summa)
                                                                {
                                                                    ((Player)Users[nextPlayer]).Balance -= ((Bank)Field.Buldings[Users[nextPlayer].CordinationPlayer]).Summa;
                                                                    Thread.Sleep(2000);
                                                                    menu = false;
                                                                }
                                                                else
                                                                {
                                                                    Console.ForegroundColor = ConsoleColor.DarkRed;
                                                                    Console.WriteLine("!!! Недостаточно денег на балансе !!!");
                                                                    Console.ForegroundColor = ConsoleColor.Gray;
                                                                }
                                                            }
                                                            break;
                                                        case TaxMenu.MortagageBsn:
                                                            {
                                                                if (ShowMyBsn(((Player)Users[nextPlayer]).Symbol))
                                                                {
                                                                    Console.ForegroundColor = ConsoleColor.DarkRed;
                                                                    Console.WriteLine("Нету бизнесов которые можно заложить");
                                                                    Console.ForegroundColor = ConsoleColor.Gray;
                                                                    Thread.Sleep(2000);
                                                                    break;
                                                                }
                                                                Console.Write("{ Ввод } > ");
                                                                int.TryParse(Console.ReadLine(), out numberCell);
                                                                if (numberCell >= maxFieldCount || numberCell < 0 || ((Player)Users[nextPlayer]).LayACell(Field.Buldings[numberCell], numberCell, Field.Buldings))
                                                                {
                                                                    Console.ForegroundColor = ConsoleColor.DarkRed;
                                                                    Console.WriteLine("Неверный номер бизнеса введите новый");
                                                                    Console.ForegroundColor = ConsoleColor.Gray;
                                                                    Thread.Sleep(2000);
                                                                    break;
                                                                }
                                                            }
                                                            break;
                                                        case TaxMenu.BranchSale:
                                                            {
                                                                if (((Player)Users[nextPlayer]).IsHaveMeMonoopoly(Field.Buldings))
                                                                {
                                                                    if (((Player)Users[nextPlayer]).GetBsnWithBranch(Field.Buldings))
                                                                    {
                                                                        if (((Player)Users[nextPlayer]).ShowImprovedBsn(((Player)Users[nextPlayer]).SerchImporvedBsn(Field.Buldings)))
                                                                        {
                                                                            Console.ForegroundColor = ConsoleColor.DarkRed;
                                                                            Console.WriteLine("Нельзя постоить филиал");
                                                                            Console.ForegroundColor = ConsoleColor.Gray;
                                                                            Thread.Sleep(2000);
                                                                            break;
                                                                        }
                                                                        Console.Write("Введите номер бизнеса : > ");
                                                                        int.TryParse(Console.ReadLine(), out numberCell);
                                                                        if (numberCell >= maxFieldCount || numberCell < 0 || ((Player)Users[nextPlayer]).BranchSale(((Player)Users[nextPlayer]).SerchImporvedBsn(Field.Buldings), numberCell))
                                                                        {
                                                                            Console.ForegroundColor = ConsoleColor.DarkRed;
                                                                            Console.WriteLine("Неверный номер бизнеса введите новый");
                                                                            Console.ForegroundColor = ConsoleColor.Gray;
                                                                            Thread.Sleep(2000);
                                                                            break;
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        Console.ForegroundColor = ConsoleColor.DarkRed;
                                                                        Console.WriteLine("Нету бизнесов с построенным филиалом");
                                                                        Console.ForegroundColor = ConsoleColor.Gray;
                                                                        Thread.Sleep(2000);
                                                                    }
                                                                }
                                                            }
                                                            break;
                                                        case TaxMenu.Surrender:
                                                            {
                                                                Users[nextPlayer].Surrender = true;
                                                                ((Player)Users[nextPlayer]).Surrendered(Field);
                                                                Field.Buldings[Users[nextPlayer].CordinationPlayer].Symbol.Remove(Users[nextPlayer].Symbol);
                                                                surrender = true;
                                                                menu = false;
                                                                check = false;
                                                            }
                                                            break;
                                                    }
                                                    //Console.Clear();
                                                    ShowField("");
                                                }
                                            }//проверка что ячейка банк
                                            else if (Field.Buldings[Users[nextPlayer].CordinationPlayer].GetType() == typeof(Tax))
                                            {
                                                while (menu)
                                                {
                                                    ShowPayMenu("Выплатить налога на роскаш", 1);
                                                    Console.WriteLine($"Цена снятия составляет {((Tax)Field.Buldings[Users[nextPlayer].CordinationPlayer]).Summa}");
                                                    Console.Write("{ Ввод } > ");
                                                    Enum.TryParse(Console.ReadLine(), out taxMenu);
                                                    switch (taxMenu)
                                                    {
                                                        case TaxMenu.TaxPayment:
                                                            {
                                                                if (((Player)Users[nextPlayer]).Balance >= ((Tax)Field.Buldings[Users[nextPlayer].CordinationPlayer]).Summa)
                                                                {
                                                                    ((Player)Users[nextPlayer]).Balance -= ((Tax)Field.Buldings[Users[nextPlayer].CordinationPlayer]).Summa;
                                                                    menu = false;
                                                                }
                                                                else
                                                                {
                                                                    Console.ForegroundColor = ConsoleColor.DarkRed;
                                                                    Console.WriteLine("!!! Недостаточно денег на балансе !!!");
                                                                    Console.ForegroundColor = ConsoleColor.Gray;
                                                                }
                                                            }
                                                            break;
                                                        case TaxMenu.MortagageBsn:
                                                            {
                                                                if (ShowMyBsn(((Player)Users[nextPlayer]).Symbol))
                                                                {
                                                                    Console.ForegroundColor = ConsoleColor.DarkRed;
                                                                    Console.WriteLine("Нету бизнесов которые можно заложить");
                                                                    Console.ForegroundColor = ConsoleColor.Gray;
                                                                    Thread.Sleep(2000);
                                                                    break;
                                                                }
                                                                Console.Write("{ Ввод } > ");
                                                                int.TryParse(Console.ReadLine(), out numberCell);
                                                                if (numberCell >= maxFieldCount || numberCell < 0 || ((Player)Users[nextPlayer]).LayACell(Field.Buldings[numberCell], numberCell, Field.Buldings))
                                                                {
                                                                    Console.ForegroundColor = ConsoleColor.DarkRed;
                                                                    Console.WriteLine("Неверный номер бизнеса введите новый");
                                                                    Console.ForegroundColor = ConsoleColor.Gray;
                                                                    Thread.Sleep(2000);
                                                                    break;
                                                                }
                                                            }
                                                            break;
                                                        case TaxMenu.BranchSale:
                                                            {
                                                                if (((Player)Users[nextPlayer]).IsHaveMeMonoopoly(Field.Buldings))
                                                                {
                                                                    if (((Player)Users[nextPlayer]).GetBsnWithBranch(Field.Buldings))
                                                                    {
                                                                        if (((Player)Users[nextPlayer]).ShowImprovedBsn(((Player)Users[nextPlayer]).SerchImporvedBsn(Field.Buldings)))
                                                                        {
                                                                            Console.ForegroundColor = ConsoleColor.DarkRed;
                                                                            Console.WriteLine("Нельзя постоить филиал");
                                                                            Console.ForegroundColor = ConsoleColor.Gray;
                                                                            Thread.Sleep(2000);
                                                                            break;
                                                                        }
                                                                        Console.Write("Введите номер бизнеса : > ");
                                                                        int.TryParse(Console.ReadLine(), out numberCell);
                                                                        if (numberCell >= maxFieldCount || numberCell < 0 || ((Player)Users[nextPlayer]).BranchSale(((Player)Users[nextPlayer]).SerchImporvedBsn(Field.Buldings), numberCell))
                                                                        {
                                                                            Console.ForegroundColor = ConsoleColor.DarkRed;
                                                                            Console.WriteLine("Неверный номер бизнеса введите новый");
                                                                            Console.ForegroundColor = ConsoleColor.Gray;
                                                                            Thread.Sleep(2000);
                                                                            break;
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        Console.ForegroundColor = ConsoleColor.DarkRed;
                                                                        Console.WriteLine("Нету бизнесов с построенным филиалом");
                                                                        Console.ForegroundColor = ConsoleColor.Gray;
                                                                        Thread.Sleep(2000);
                                                                    }
                                                                }
                                                            }
                                                            break;
                                                        case TaxMenu.Surrender:
                                                            {
                                                                Users[nextPlayer].Surrender = true;
                                                                ((Player)Users[nextPlayer]).Surrendered(Field);
                                                                Field.Buldings[Users[nextPlayer].CordinationPlayer].Symbol.Remove(Users[nextPlayer].Symbol);
                                                                surrender = true;
                                                                menu = false;
                                                                check = false;
                                                            }
                                                            break;
                                                    }
                                                    //Console.Clear();
                                                    ShowField("");
                                                }
                                            }//проверка что ячейка налог
                                            else if (((Player)Users[nextPlayer]).IsCheckCellChance(Field.Buldings[((Player)Users[nextPlayer]).CordinationPlayer]))
                                            {
                                                Random random = new Random();
                                                //14chance 
                                                Chances chance = ((Chance)Field.Buldings[Users[nextPlayer].CordinationPlayer]).Chances[random.Next(0, ((Chance)Field.Buldings[Users[nextPlayer].CordinationPlayer]).Chances.Count)];
                                                //Chances chance = ((Chance)Field.Buldings[Users[nextPlayer].CordinationPlayer]).Chances[1];
                                                if (((Player)Users[nextPlayer]).IsCheckChanceIsLesion(chance))
                                                {
                                                    while (menu)
                                                    {
                                                        ShowPayMenu("оплатить", 2);
                                                        Console.Write("{ Ввод } > ");
                                                        Enum.TryParse(Console.ReadLine(), out taxMenu);
                                                        switch (taxMenu)
                                                        {
                                                            case TaxMenu.TaxPayment:
                                                                {
                                                                    if (!((Player)Users[nextPlayer]).ChanceIsWork(chance))
                                                                    {
                                                                        Console.ForegroundColor = ConsoleColor.DarkRed;
                                                                        Console.WriteLine("Не хватает деняг !!!");
                                                                        Console.ForegroundColor = ConsoleColor.Gray;
                                                                        Thread.Sleep(2000);
                                                                        continue;
                                                                    }
                                                                    menu = false;
                                                                    Console.ForegroundColor = ConsoleColor.Green;
                                                                    Console.WriteLine("Оплата прошла успешно");
                                                                    Console.ForegroundColor = ConsoleColor.Gray;
                                                                    Thread.Sleep(2000);
                                                                }
                                                                break;
                                                            case TaxMenu.MortagageBsn:
                                                                {
                                                                    if (ShowMyBsn(((Player)Users[nextPlayer]).Symbol))
                                                                    {
                                                                        Console.ForegroundColor = ConsoleColor.DarkRed;
                                                                        Console.WriteLine("Нету бизнесов которые можно заложить");
                                                                        Console.ForegroundColor = ConsoleColor.Gray;
                                                                        Thread.Sleep(2000);
                                                                        break;
                                                                    }
                                                                    Console.Write("{ Ввод } > ");
                                                                    int.TryParse(Console.ReadLine(), out numberCell);
                                                                    if (numberCell >= maxFieldCount || numberCell < 0 || ((Player)Users[nextPlayer]).LayACell(Field.Buldings[numberCell], numberCell, Field.Buldings))
                                                                    {
                                                                        Console.ForegroundColor = ConsoleColor.DarkRed;
                                                                        Console.WriteLine("Неверный номер бизнеса введите новый");
                                                                        Console.ForegroundColor = ConsoleColor.Gray;
                                                                        Thread.Sleep(2000);
                                                                        break;
                                                                    }
                                                                }
                                                                break;
                                                            case TaxMenu.BranchSale:
                                                                {
                                                                    if (((Player)Users[nextPlayer]).IsHaveMeMonoopoly(Field.Buldings))
                                                                    {
                                                                        if (((Player)Users[nextPlayer]).GetBsnWithBranch(Field.Buldings))
                                                                        {
                                                                            if (((Player)Users[nextPlayer]).ShowImprovedBsn(((Player)Users[nextPlayer]).SerchImporvedBsn(Field.Buldings)))
                                                                            {
                                                                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                                                                Console.WriteLine("Нельзя продать филиал");
                                                                                Console.ForegroundColor = ConsoleColor.Gray;
                                                                                Thread.Sleep(2000);
                                                                                break;
                                                                            }
                                                                            Console.Write("Введите номер бизнеса : > ");
                                                                            int.TryParse(Console.ReadLine(), out numberCell);
                                                                            if (numberCell >= maxFieldCount || numberCell < 0 || ((Player)Users[nextPlayer]).BranchSale(((Player)Users[nextPlayer]).SerchImporvedBsn(Field.Buldings), numberCell))
                                                                            {
                                                                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                                                                Console.WriteLine("Неверный номер бизнеса введите новый");
                                                                                Console.ForegroundColor = ConsoleColor.Gray;
                                                                                Thread.Sleep(2000);
                                                                                break;
                                                                            }
                                                                        }
                                                                        else
                                                                        {
                                                                            Console.ForegroundColor = ConsoleColor.DarkRed;
                                                                            Console.WriteLine("Нету бизнесов с построенным филиалом");
                                                                            Console.ForegroundColor = ConsoleColor.Gray;
                                                                            Thread.Sleep(2000);
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        Console.ForegroundColor = ConsoleColor.DarkRed;
                                                                        Console.WriteLine("У вас нету моноплии ");
                                                                        Console.ForegroundColor = ConsoleColor.Gray;
                                                                        Thread.Sleep(2000);
                                                                    }
                                                                }
                                                                break;
                                                            case TaxMenu.Surrender:
                                                                {
                                                                    Users[nextPlayer].Surrender = true;
                                                                    ((Player)Users[nextPlayer]).Surrendered(Field);
                                                                    Field.Buldings[Users[nextPlayer].CordinationPlayer].Symbol.Remove(Users[nextPlayer].Symbol);
                                                                    surrender = true;
                                                                    menu = false;
                                                                    check = false;
                                                                }
                                                                break;
                                                        }
                                                        //Console.Clear();
                                                        ShowField("");
                                                    }
                                                }
                                                else if (((Player)Users[nextPlayer]).IsCheckChanceIsTepeport(chance))
                                                {
                                                    ((Player)Users[nextPlayer]).ChanceCheck(chance, Field,Users);
                                                    goto teleport;
                                                }
                                                else
                                                {
                                                    ((Player)Users[nextPlayer]).ChanceCheck(chance, Field,Users);
                                                }
                                            }//проверка что ячейка шанс снятие деняг 
                                        }//fix
                                        else if (((Player)Users[nextPlayer]).IsCheckCellBsn(Field.Buldings[Users[nextPlayer].CordinationPlayer]))
                                        {
                                            Console.WriteLine($"Игрок {Users[nextPlayer].Symbol} попал на ячейку {Field.Buldings[Users[nextPlayer].CordinationPlayer].Title}");
                                            if (((Player)Users[nextPlayer]).IsCehckByCell(Field.Buldings[Users[nextPlayer].CordinationPlayer]) == true)
                                            {
                                                while (menu)
                                                {
                                                    ShowPayMenu("Купить бизнес", 0);
                                                    Console.Write("{ Ввод } > ");
                                                    Enum.TryParse(Console.ReadLine(), out buyMenu);
                                                    switch (buyMenu)
                                                    {
                                                        case BuyMenu.BuyBsn:
                                                            {
                                                                if (!((Player)Users[nextPlayer]).IsByCell(Field.Buldings[Users[nextPlayer].CordinationPlayer], Field.Buldings))
                                                                {
                                                                    menu = false;
                                                                }
                                                                else
                                                                {
                                                                    Console.ForegroundColor = ConsoleColor.DarkRed;
                                                                    Console.WriteLine("У вас не хватает деньжат");
                                                                    Console.ForegroundColor = ConsoleColor.Gray;
                                                                    Thread.Sleep(2000);
                                                                    menu = true;
                                                                }
                                                            }
                                                            break;
                                                        case BuyMenu.MortagageBsn:
                                                            {
                                                                if (ShowMyBsn(((Player)Users[nextPlayer]).Symbol))
                                                                {
                                                                    Console.ForegroundColor = ConsoleColor.DarkRed;
                                                                    Console.WriteLine("Нету бизнесов которые можно заложить");
                                                                    Console.ForegroundColor = ConsoleColor.Gray;
                                                                    Thread.Sleep(2000);
                                                                    break;
                                                                }
                                                                Console.Write("{ Ввод } > ");
                                                                int.TryParse(Console.ReadLine(), out numberCell);
                                                                if (numberCell >= maxFieldCount || numberCell < 0 || ((Player)Users[nextPlayer]).LayACell(Field.Buldings[numberCell], numberCell, Field.Buldings))
                                                                {
                                                                    Console.ForegroundColor = ConsoleColor.DarkRed;
                                                                    Console.WriteLine("Неверный номер бизнеса введите новый");
                                                                    Console.ForegroundColor = ConsoleColor.Gray;
                                                                    Thread.Sleep(2000);
                                                                    break;
                                                                }
                                                            }
                                                            break;
                                                        case BuyMenu.BranchSale:
                                                            {
                                                                if (((Player)Users[nextPlayer]).IsHaveMeMonoopoly(Field.Buldings))
                                                                {
                                                                    if (((Player)Users[nextPlayer]).GetBsnWithBranch(Field.Buldings))
                                                                    {
                                                                        if (((Player)Users[nextPlayer]).ShowImprovedBsn(((Player)Users[nextPlayer]).SerchImporvedBsn(Field.Buldings)))
                                                                        {
                                                                            Console.ForegroundColor = ConsoleColor.DarkRed;
                                                                            Console.WriteLine("Нельзя продать филиал");
                                                                            Console.ForegroundColor = ConsoleColor.Gray;
                                                                            Thread.Sleep(2000);
                                                                            break;
                                                                        }
                                                                        Console.Write("Введите номер бизнеса : > ");
                                                                        int.TryParse(Console.ReadLine(), out numberCell);
                                                                        if (numberCell >= maxFieldCount || numberCell < 0 || ((Player)Users[nextPlayer]).BranchSale(((Player)Users[nextPlayer]).SerchImporvedBsn(Field.Buldings), numberCell))
                                                                        {
                                                                            Console.ForegroundColor = ConsoleColor.DarkRed;
                                                                            Console.WriteLine("Неверный номер бизнеса введите новый");
                                                                            Console.ForegroundColor = ConsoleColor.Gray;
                                                                            Thread.Sleep(2000);
                                                                            break;
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        Console.ForegroundColor = ConsoleColor.DarkRed;
                                                                        Console.WriteLine("Нету бизнесов с построенным филиалом");
                                                                        Console.ForegroundColor = ConsoleColor.Gray;
                                                                        Thread.Sleep(2000);
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    Console.ForegroundColor = ConsoleColor.DarkRed;
                                                                    Console.WriteLine("У вас нету моноплии ");
                                                                    Console.ForegroundColor = ConsoleColor.Gray;
                                                                    Thread.Sleep(2000);
                                                                }
                                                            }
                                                            break;
                                                        case BuyMenu.Auction:
                                                            {
                                                                Auction(Field.Buldings[Users[nextPlayer].CordinationPlayer], Users[nextPlayer].Symbol);
                                                                menu = false;
                                                            }
                                                            break;
                                                        case BuyMenu.Surrender:
                                                            {
                                                                Users[nextPlayer].Surrender = true;
                                                                ((Player)Users[nextPlayer]).Surrendered(Field);
                                                                Field.Buldings[Users[nextPlayer].CordinationPlayer].Symbol.Remove(Users[nextPlayer].Symbol);
                                                                surrender = true;
                                                                menu = false;
                                                                check = false;
                                                            }
                                                            break;
                                                    }
                                                    //Console.Clear();
                                                    ShowField("");
                                                }
                                            }//покупка ячейки
                                            else
                                            {
                                                if (((Player)Users[nextPlayer]).CheckHaveBsn(Field.Buldings[Users[nextPlayer].CordinationPlayer]))
                                                {
                                                    while (menu)
                                                    {
                                                        ShowPayMenu("Оплатить ренту", 1);
                                                        Console.Write("{ Ввод } > ");
                                                        Enum.TryParse(Console.ReadLine(), out payMenu);
                                                        switch (payMenu)
                                                        {
                                                            case PayMenu.RentPayment:
                                                                {
                                                                    if (((Player)Users[nextPlayer]).PayRent(Field.Buldings[Users[nextPlayer].CordinationPlayer], Users, firstCube, secondCube))
                                                                    {
                                                                        menu = false;
                                                                    }
                                                                    else
                                                                    {
                                                                        Console.ForegroundColor = ConsoleColor.DarkRed;
                                                                        Console.WriteLine("У вас не хватает деньжат");
                                                                        Console.ForegroundColor = ConsoleColor.Gray;
                                                                        Thread.Sleep(2000);
                                                                        menu = true;
                                                                    }
                                                                }
                                                                break;
                                                            case PayMenu.MortagageBsn:
                                                                {
                                                                    if (ShowMyBsn(((Player)Users[nextPlayer]).Symbol))
                                                                    {
                                                                        Console.ForegroundColor = ConsoleColor.DarkRed;
                                                                        Console.WriteLine("Нету бизнесов которые можно заложить");
                                                                        Console.ForegroundColor = ConsoleColor.Gray;
                                                                        Thread.Sleep(2000);
                                                                        break;
                                                                    }
                                                                    Console.Write("{ Ввод } > ");
                                                                    int.TryParse(Console.ReadLine(), out numberCell);
                                                                    if (numberCell >= maxFieldCount || numberCell < 0 || ((Player)Users[nextPlayer]).LayACell(Field.Buldings[numberCell], numberCell, Field.Buldings))
                                                                    {
                                                                        Console.ForegroundColor = ConsoleColor.DarkRed;
                                                                        Console.WriteLine("Неверный номер бизнеса введите новый");
                                                                        Console.ForegroundColor = ConsoleColor.Gray;
                                                                        Thread.Sleep(2000);
                                                                        break;
                                                                    }
                                                                }
                                                                break;
                                                            case PayMenu.BranchSale:
                                                                {
                                                                    if (((Player)Users[nextPlayer]).IsHaveMeMonoopoly(Field.Buldings))
                                                                    {
                                                                        if (((Player)Users[nextPlayer]).GetBsnWithBranch(Field.Buldings))
                                                                        {
                                                                            if (((Player)Users[nextPlayer]).ShowImprovedBsn(((Player)Users[nextPlayer]).SerchImporvedBsn(Field.Buldings)))
                                                                            {
                                                                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                                                                Console.WriteLine("Нельзя продать филиал");
                                                                                Console.ForegroundColor = ConsoleColor.Gray;
                                                                                Thread.Sleep(2000);
                                                                                break;
                                                                            }
                                                                            Console.Write("Введите номер бизнеса : > ");
                                                                            int.TryParse(Console.ReadLine(), out numberCell);
                                                                            if (numberCell >= maxFieldCount || numberCell < 0 || ((Player)Users[nextPlayer]).BranchSale(((Player)Users[nextPlayer]).SerchImporvedBsn(Field.Buldings), numberCell))
                                                                            {
                                                                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                                                                Console.WriteLine("Неверный номер бизнеса введите новый");
                                                                                Console.ForegroundColor = ConsoleColor.Gray;
                                                                                Thread.Sleep(2000);
                                                                                break;
                                                                            }
                                                                        }
                                                                        else
                                                                        {
                                                                            Console.ForegroundColor = ConsoleColor.DarkRed;
                                                                            Console.WriteLine("Нету бизнесов с построенным филиалом");
                                                                            Console.ForegroundColor = ConsoleColor.Gray;
                                                                            Thread.Sleep(2000);
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        Console.ForegroundColor = ConsoleColor.DarkRed;
                                                                        Console.WriteLine("У вас нету моноплии ");
                                                                        Console.ForegroundColor = ConsoleColor.Gray;
                                                                        Thread.Sleep(2000);
                                                                    }
                                                                }
                                                                break;
                                                            case PayMenu.Surrender:
                                                                {
                                                                    Users[nextPlayer].Surrender = true;
                                                                    ((Player)Users[nextPlayer]).Surrendered(Field);
                                                                    Field.Buldings[Users[nextPlayer].CordinationPlayer].Symbol.Remove(Users[nextPlayer].Symbol);
                                                                    surrender = true;
                                                                    menu = false;
                                                                    check = false;
                                                                }
                                                                break;
                                                        }
                                                        //Console.Clear()
                                                        ShowField("");
                                                    }
                                                }
                                                else
                                                {
                                                    Console.WriteLine($"Игрок {Users[nextPlayer].Symbol} попал на свое поле!!!");
                                                    Thread.Sleep(2000);
                                                }
                                            }//выплата ренты ячейки
                                        }
                                        ShowField("");
                                        menu = true;
                                    }
                                }
                                break;
                            case GameMenu.SellTheBusiness://заложить бизнес
                                {
                                    if (((Player)Users[nextPlayer]).ShowALlBsn(((Player)Users[nextPlayer]).GetAllBsn(Field.Buldings)))
                                    {
                                        Console.ForegroundColor = ConsoleColor.DarkRed;
                                        Console.WriteLine("Нету бизнесов которые можно заложить");
                                        Console.ForegroundColor = ConsoleColor.Gray;
                                        Thread.Sleep(2000);
                                        break;
                                    }
                                    Console.Write("{ Ввод } > ");
                                    int.TryParse(Console.ReadLine(), out numberCell);
                                    if (numberCell >= maxFieldCount || numberCell < 0 || ((Player)Users[nextPlayer]).LayACell(Field.Buldings[numberCell], numberCell, Field.Buldings))
                                    {
                                        Console.ForegroundColor = ConsoleColor.DarkRed;
                                        Console.WriteLine("Неверный номер бизнеса введите новый");
                                        Console.ForegroundColor = ConsoleColor.Gray;
                                        Thread.Sleep(2000);
                                        break;
                                    }
                                }
                                break;
                            case GameMenu.BuyOutYourBusiness://выкупить бизнеса
                                {
                                    if (ShowMortgagedBsn(((Player)Users[nextPlayer]).Symbol))
                                    {
                                        Console.ForegroundColor = ConsoleColor.DarkRed;
                                        Console.WriteLine("Нету бизнесов которые можно выкупить ");
                                        Console.ForegroundColor = ConsoleColor.Gray;
                                        Thread.Sleep(2000);
                                        break;
                                    }
                                    Console.Write("{ Ввод } > ");
                                    int.TryParse(Console.ReadLine(), out numberCell);
                                    if (numberCell >= maxFieldCount || numberCell < 0 || ((Player)Users[nextPlayer]).BsnBuyout(Field.Buldings[numberCell], numberCell, Field.Buldings))
                                    {
                                        Console.ForegroundColor = ConsoleColor.DarkRed;
                                        Console.WriteLine("Неверный номер бизнеса введите новый");
                                        Console.ForegroundColor = ConsoleColor.Gray;
                                        Thread.Sleep(2000);
                                        break;
                                    }
                                }
                                break;
                            case GameMenu.QuitTheTrade://доделать трейд 
                                {
                                    //Console.Write("{");
                                    //Console.ForegroundColor = ConsoleColor.DarkGreen;
                                    //Console.Write(" Введите символ игрока которму хотите отправить трейд ");
                                    //Console.ForegroundColor = ConsoleColor.Gray;
                                    //Console.WriteLine("}");
                                    //Trade.ShowTrade();
                                    //Console.Write("{ Ввод } > ");
                                    //char.TryParse(Console.ReadLine(), out char symbol);
                                    Console.ForegroundColor = ConsoleColor.DarkRed;
                                    Console.WriteLine("Трейд в заработке пока что не работает");
                                    Thread.Sleep(2000);
                                    Console.ForegroundColor = ConsoleColor.Gray;
                                }
                                break;
                            case GameMenu.PurchaseBranch://постойка улучшений 
                                {
                                    if (!opportunityEnter)
                                    {
                                        if (((Player)Users[nextPlayer]).IsHaveMeMonoopoly(Field.Buldings))
                                        {
                                            if (((Player)Users[nextPlayer]).CheckingBranchImproved(((Player)Users[nextPlayer]).ShowBsn(((Player)Users[nextPlayer]).CreatMonopolyBsn(Field.Buldings))))
                                            {
                                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                                Console.WriteLine("Нету бизнесов котоыре можно улучшить");
                                                Console.ForegroundColor = ConsoleColor.Gray;
                                                Thread.Sleep(2000);
                                                break;
                                            }
                                            ((Player)Users[nextPlayer]).ShowUpdateBsn(((Player)Users[nextPlayer]).ShowBsn(((Player)Users[nextPlayer]).CreatMonopolyBsn(Field.Buldings)));
                                            Console.Write("Введите номер бизнеса : > ");
                                            int.TryParse(Console.ReadLine(), out numberCell);
                                            while (((Player)Users[nextPlayer]).BusinessLiquidityCheck(((Player)Users[nextPlayer]).ShowBsn(((Player)Users[nextPlayer]).CreatMonopolyBsn(Field.Buldings)), numberCell))
                                            {
                                                Console.WriteLine("Неверныый номер бизнеса введите ещё раз");
                                                Console.Write("Введите номер бизнеса : > ");
                                                int.TryParse(Console.ReadLine(), out numberCell);
                                            }
                                            ((Player)Users[nextPlayer]).MonoopolyImprovement((Business)Field.Buldings[numberCell]);
                                        }
                                        else
                                        {
                                            Console.ForegroundColor = ConsoleColor.DarkRed;
                                            Console.WriteLine("Монополий нету нельзя попасть в этот пункт");
                                            Console.ForegroundColor = ConsoleColor.Gray;
                                            Thread.Sleep(2000);
                                        }
                                        opportunityEnter = true;
                                    }
                                    else
                                    {
                                        Console.ForegroundColor = ConsoleColor.DarkRed;
                                        Console.WriteLine("нельзя посторить улучшение больше одного раза за ход");
                                        Console.ForegroundColor = ConsoleColor.Gray;
                                        Thread.Sleep(2000);
                                    }
                                }
                                break;
                            case GameMenu.SellBranch://продажа улучшения 
                                {
                                    if (((Player)Users[nextPlayer]).IsHaveMeMonoopoly(Field.Buldings))
                                    {
                                        if (((Player)Users[nextPlayer]).GetBsnWithBranch(Field.Buldings))
                                        {
                                            if (((Player)Users[nextPlayer]).ShowImprovedBsn(((Player)Users[nextPlayer]).SerchImporvedBsn(Field.Buldings)))
                                            {
                                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                                Console.WriteLine("Нельзя постоить филиал");
                                                Console.ForegroundColor = ConsoleColor.Gray;
                                                Thread.Sleep(2000);
                                                break;
                                            }
                                            Console.Write("Введите номер бизнеса : > ");
                                            int.TryParse(Console.ReadLine(), out numberCell);
                                            if (numberCell >= maxFieldCount || numberCell < 0 || ((Player)Users[nextPlayer]).BranchSale(((Player)Users[nextPlayer]).SerchImporvedBsn(Field.Buldings), numberCell))
                                            {
                                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                                Console.WriteLine("Неверный номер бизнеса введите новый");
                                                Console.ForegroundColor = ConsoleColor.Gray;
                                                Thread.Sleep(2000);
                                                break;
                                            }
                                        }
                                        else
                                        {
                                            Console.ForegroundColor = ConsoleColor.DarkRed;
                                            Console.WriteLine("Нету бизнесов с построенным филиалом");
                                            Console.ForegroundColor = ConsoleColor.Gray;
                                            Thread.Sleep(2000);
                                        }
                                    }
                                    else
                                    {
                                        Console.ForegroundColor = ConsoleColor.DarkRed;
                                        Console.WriteLine("У вас нету моноплии ");
                                        Console.ForegroundColor = ConsoleColor.Gray;
                                        Thread.Sleep(2000);
                                    }
                                }
                                break;
                            case GameMenu.Surrender://сдатся         
                                {
                                    Users[nextPlayer].Surrender = true;
                                    ((Player)Users[nextPlayer]).Surrendered(Field);
                                    Field.Buldings[Users[nextPlayer].CordinationPlayer].Symbol.Remove(Users[nextPlayer].Symbol);
                                    surrender = true;
                                    check = false;
                                }
                                break;
                        }
                        #region TestCode
                        //if (surrender != true || prison != true || skipping != true)
                        //{
                        //    ShowGameCube(firstCube);
                        //    ShowGameCube(secondCube);
                        //    Thread.Sleep(2000);
                        //    if ((Users[nextPlayer].CordinationPlayer + firstCube + secondCube) >= Field.Buldings.Count)
                        //    {
                        //        if (Users[nextPlayer].ReverseStroke == true)
                        //        {
                        //            Field.Buldings[Users[nextPlayer].CordinationPlayer].Symbol.Remove(Users[nextPlayer].Symbol);
                        //            Users[nextPlayer].CordinationPlayer -= firstCube + secondCube + Field.Buldings.Count;
                        //            Field.Buldings[Users[nextPlayer].CordinationPlayer].Symbol.Add(Users[nextPlayer].Symbol);
                        //            ((Player)Users[nextPlayer]).CheckCell(Field.Buldings[Users[nextPlayer].CordinationPlayer], Field);
                        //            Users[nextPlayer].ReverseStroke = false;
                        //        }//если игроку выпал шанс ход в обратную сторону
                        //        else
                        //        {
                        //            Field.Buldings[Users[nextPlayer].CordinationPlayer].Symbol.Remove(Users[nextPlayer].Symbol);
                        //            Users[nextPlayer].CordinationPlayer += firstCube + secondCube - Field.Buldings.Count;
                        //            Field.Buldings[Users[nextPlayer].CordinationPlayer].Symbol.Add(Users[nextPlayer].Symbol);
                        //            ((Player)Users[nextPlayer]).CheckCell(Field.Buldings[Users[nextPlayer].CordinationPlayer], Field);
                        //            Users[nextPlayer].Balance += 2000;
                        //        }
                        //    }//если игрок вышел за приделы поля 
                        //    else
                        //    {
                        //        if (Users[nextPlayer].ReverseStroke == true)
                        //        {
                        //            Field.Buldings[Users[nextPlayer].CordinationPlayer - (firstCube + secondCube)].Symbol.Add(Users[nextPlayer].Symbol);
                        //            Field.Buldings[Users[nextPlayer].CordinationPlayer].Symbol.Remove(Users[nextPlayer].Symbol);
                        //            Users[nextPlayer].CordinationPlayer -= firstCube + secondCube;
                        //            ((Player)Users[nextPlayer]).CheckCell(Field.Buldings[Users[nextPlayer].CordinationPlayer], Field);
                        //            Users[nextPlayer].ReverseStroke = false;
                        //        }//если игроку выпал шанс ход в обратную сторону
                        //        else
                        //        {
                        //            Field.Buldings[Users[nextPlayer].CordinationPlayer + firstCube + secondCube].Symbol.Add(Users[nextPlayer].Symbol);
                        //            Field.Buldings[Users[nextPlayer].CordinationPlayer].Symbol.Remove(Users[nextPlayer].Symbol);
                        //            Users[nextPlayer].CordinationPlayer += firstCube + secondCube;
                        //            ((Player)Users[nextPlayer]).CheckCell(Field.Buldings[Users[nextPlayer].CordinationPlayer], Field);
                        //        }
                        //    }//если игрок дивгается по пределам поля 
                        //    Console.Clear();
                        //    ShowField("");
                        //    if (((Player)Users[nextPlayer]).IsCheckCellNotBis(Field.Buldings[Users[nextPlayer].CordinationPlayer]) == false)
                        //    {
                        //        if (((Player)Users[nextPlayer]).IsCehckByCell(Field.Buldings[Users[nextPlayer].CordinationPlayer]) == false)
                        //        {
                        //            Console.WriteLine("Купить бизнес {1} Да / {2} Нет");
                        //            Console.Write("{ Ввод } > ");
                        //            int.TryParse(Console.ReadLine(), out int number);
                        //            if (number == 1)
                        //            {
                        //                ((Player)Users[nextPlayer]).IsByCell(Field.Buldings[Users[nextPlayer].CordinationPlayer]);
                        //            }
                        //            else
                        //            {
                        //                Auction(Users, Field.Buldings[Users[nextPlayer].CordinationPlayer]);
                        //            }
                        //        }//покупка ячейки 
                        //        else
                        //        {
                        //            Console.WriteLine("{1} Оплатить ренту / {2} Сдатся");
                        //            Console.Write("{ Ввод } > ");
                        //            int.TryParse(Console.ReadLine(), out int number);
                        //            if (number == 1)
                        //            {
                        //                ((Player)Users[nextPlayer]).PayRent(Field.Buldings[Users[nextPlayer].CordinationPlayer], Users);
                        //            }
                        //            else
                        //            {
                        //                Users[nextPlayer].Surrender = true;
                        //            }//сдатся
                        //        }//выплата ренты ячейки
                        //    }
                        //    ShowField($"Игрок {Users[nextPlayer].Name} кинул кубики число первого кубика [{firstCube}] число второго кубика [{secondCube}]");
                        //}
                        #endregion
                    }
                }//логика хода игрока 
                nextPlayer++;
                if (nextPlayer >= Users.Count)
                {
                    nextPlayer = 0;
                }
                skipping = false;
            }
        }//начало игры 
    }
}
