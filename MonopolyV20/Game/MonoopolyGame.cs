
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
            Random random = new Random();
            int nextPlayer = 0;
            int bsnPrice = 0;
            BusinessType businessType = (BusinessType)0;
            if (bulding.GetType() == typeof(Business))
            {
                bsnPrice = ((Business)bulding).Price + 100;
                businessType = ((Business)bulding).BusinessType;
            }
            else if (bulding.GetType() == typeof(CarInterior))
            {
                bsnPrice = ((CarInterior)bulding).Price + 100;
                businessType = ((CarInterior)bulding).BusinessType;
            }
            else if (bulding.GetType() == typeof(GamingCompanies))
            {
                bsnPrice = ((GamingCompanies)bulding).Price + 100;
                businessType = ((GamingCompanies)bulding).BusinessType;
            }
            List<User> user = new List<User>();
            for (int i = 0; i < Users.Count; i++)
            {
                if (Users[i].Symbol != symbol && !Users[i].Surrender)
                {
                    user.Add(Users[i]);
                }
            }
            bool isWork = true;
            while (isWork)
            {
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
                    bool startOrStop = false;
                    if (((Bot)user[nextPlayer]).IsHaveMeMonoopoly(Field.Buldings))//fix
                    {
                        user.Remove(user[nextPlayer]);
                    }
                    else
                    {
                        if (((Bot)user[nextPlayer]).IsHaveBusinessThisType(businessType, Field.Buldings) == true ||
                            ((Bot)user[nextPlayer]).IsHaveEnemyBusinessType(Users, businessType, Field.Buldings))
                        {
                            startOrStop = true;
                        }
                        if (user[nextPlayer].Balance > bsnPrice * 2)
                        {
                            startOrStop = true;
                        }
                        else
                        {
                            user.Remove(user[nextPlayer]);
                            if (user.Count == 1)
                            {
                                isWork = false;
                                break;
                            }
                            break;
                        }
                        if (startOrStop)
                        {
                            bsnPrice += 100;
                        }
                        else
                        {
                            user.Remove(user[nextPlayer]);//fix
                        }
                    }
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
                            bsnPrice += 100;
                        }
                        else
                        {
                            user.Remove(user[nextPlayer]);
                        }
                    }
                }
                nextPlayer++;
            }
            if (nextPlayer >= user.Count)
            {
                nextPlayer = 0;
            }
            if (user.Count == 0)
            {
                return;
            }
            if (user[nextPlayer].GetType() == typeof(Bot) || user[nextPlayer].GetType() == typeof(Player))
            {
                if (user[nextPlayer].Balance > ((Business)bulding).Price)
                {
                    user[nextPlayer].Balance -= ((Business)bulding).Price;
                }
                else return;
                if (bulding.GetType() == typeof(Business))
                {
                    ((Business)bulding).BusinessOwner = user[nextPlayer].Symbol;
                }
                if (bulding.GetType() == typeof(CarInterior))
                {
                    ((CarInterior)bulding).BusinessOwner = user[nextPlayer].Symbol;
                }
                if (bulding.GetType() == typeof(GamingCompanies))
                {
                    ((GamingCompanies)bulding).BusinessOwner = user[nextPlayer].Symbol;
                }
            }
        }//fix
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
         //Other

        //AddDelet 
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
                }
            }
        }
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
            if (number == 0) Console.WriteLine($"{{{(int)BuyMenu.BuyBsn}}} {text}");
            else Console.WriteLine($"{{{(int)PayMenu.RentPayment}}} {text}");

            Console.WriteLine($"{{{(int)BuyMenu.MortagageBsn}}} Заложить бизнес ");
            Console.WriteLine($"{{{(int)BuyMenu.BranchSale}}} Продать филиал ");

            if (number == 0) Console.WriteLine($"{{{(int)BuyMenu.Auction}}} Отказатся от покупки   ");

            Console.WriteLine($"{{{(int)BuyMenu.Surrender}}} Сдаться");
            Console.Write("Ввод > ");


        }
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
            #region TestBot
            //Users[0].Balance -= 10000;
            Users[1].Balance -= 15000;
            //Users[2].Balance -= 14500;
            //Users[3].Balance -= 11000;

            ((Business)Field.Buldings[16]).BusinessOwner = Users[1].Symbol;
            ((Business)Field.Buldings[18]).BusinessOwner = Users[1].Symbol;
            ((Business)Field.Buldings[19]).BusinessOwner = Users[1].Symbol;

            ((Business)Field.Buldings[16]).Level = 4;
            ((Business)Field.Buldings[18]).Level = 4;
            ((Business)Field.Buldings[19]).Level = 4;

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
            bool prison = false;
            bool skipping = false;
            bool opportunityEnter = false;
            int choose = 0;
            bool menu = true;
            //int test = 0;
            PayMenu payMenu;
            BuyMenu buyMenu;
            //int lastCellNumber = 0;
            while (true)
            {
                prison = false;
                if (IsCheckWinGame())
                {
                    Console.Write("Нажмите любую кнопку что бы вернуться на начальную страницу >>> ");
                    Console.ReadLine();
                    ClearingField(Users, Field.Buldings);
                    DeletAllUser();
                    break;
                }
                opportunityEnter = false;
                if (Users[nextPlayer].GetType() == typeof(Bot))
                {
                    ((Bot)Users[nextPlayer]).SurrenderLogic(Field.Buldings);
                }
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
                if (Users[nextPlayer].Prison == true)
                {
                    prison = true;
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
                        ((Bot)Users[nextPlayer]).SurrenderLogic(Field.Buldings);
                        if (((Bot)Users[nextPlayer]).MortagagedBusinesses(Field.Buldings))
                        {
                            if (!((Bot)Users[nextPlayer]).BusinessBuyout(Field.Buldings))
                            {
                                if (nextPlayer == 0)
                                {
                                    ((Bot)Users[nextPlayer]).BotsBusinessDownturn(((Bot)Users[nextPlayer]).AllMortagagedBusinesses(Field.Buldings));
                                }
                            }
                        }
                        if (Users[nextPlayer].Prison == true)
                        {
                            if (Users[nextPlayer].Balance >= prisonPrice)
                            {
                                Users[nextPlayer].Balance -= prisonPrice;
                                Users[nextPlayer].Prison = false;
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
                        if (luckBot == 3)
                        {
                            ((Bot)Users[nextPlayer]).Prison = true;
                        }
                        //Console.Clear();
                        ShowField("");
                        firstCube = RollTheCube(rand);
                        secondCube = RollTheCube(rand);
                        ShowGameCube(firstCube);
                        ShowGameCube(secondCube);
                        Console.WriteLine($"Ход игрока {Users[nextPlayer].Symbol}");
                        Thread.Sleep(2000);
                        if ((Users[nextPlayer].CordinationPlayer + firstCube + secondCube) >= Field.Buldings.Count)
                        {
                            if (Users[nextPlayer].ReverseStroke == true)
                            {
                                Field.Buldings[Users[nextPlayer].CordinationPlayer].Symbol.Remove(Users[nextPlayer].Symbol);
                                Users[nextPlayer].CordinationPlayer -= firstCube + secondCube;
                                Field.Buldings[Users[nextPlayer].CordinationPlayer].Symbol.Add(Users[nextPlayer].Symbol);
                                ((Bot)Users[nextPlayer]).CheckCell(Field.Buldings[Users[nextPlayer].CordinationPlayer], Users, Field);
                                Users[nextPlayer].ReverseStroke = false;
                            }//если игроку выпал шанс ход в обратную сторону
                            else
                            {
                                Field.Buldings[Users[nextPlayer].CordinationPlayer].Symbol.Remove(Users[nextPlayer].Symbol);//fix
                                Users[nextPlayer].CordinationPlayer += firstCube + secondCube - Field.Buldings.Count;
                                Field.Buldings[Users[nextPlayer].CordinationPlayer].Symbol.Add(Users[nextPlayer].Symbol);
                                ((Bot)Users[nextPlayer]).CheckCell(Field.Buldings[Users[nextPlayer].CordinationPlayer], Users, Field);
                                Users[nextPlayer].Balance += 2000;
                            }
                        }
                        else
                        {
                            if (Users[nextPlayer].ReverseStroke == true)
                            {
                                if (Users[nextPlayer].CordinationPlayer - (firstCube + secondCube) < 0)//fix не попал в иф 
                                {
                                    Field.Buldings[Users[nextPlayer].CordinationPlayer + Field.Buldings.Count - (firstCube + secondCube)].Symbol.Add(Users[nextPlayer].Symbol);
                                }
                                else
                                {
                                    Field.Buldings[Users[nextPlayer].CordinationPlayer - (firstCube + secondCube)].Symbol.Add(Users[nextPlayer].Symbol);//fix краш юзер 4
                                }
                                Field.Buldings[Users[nextPlayer].CordinationPlayer].Symbol.Remove(Users[nextPlayer].Symbol);
                                if (Users[nextPlayer].CordinationPlayer - (firstCube + secondCube) < 0)
                                {
                                    Users[nextPlayer].CordinationPlayer = Field.Buldings.Count - (firstCube + secondCube);
                                }
                                else
                                {
                                    Users[nextPlayer].CordinationPlayer -= firstCube + secondCube;
                                }
                                ((Bot)Users[nextPlayer]).CheckCell(Field.Buldings[Users[nextPlayer].CordinationPlayer], Users, Field);//fix краш кординаты 
                                Users[nextPlayer].ReverseStroke = false;
                            }
                            else
                            {
                                Field.Buldings[Users[nextPlayer].CordinationPlayer + firstCube + secondCube].Symbol.Add(Users[nextPlayer].Symbol);
                                Field.Buldings[Users[nextPlayer].CordinationPlayer].Symbol.Remove(Users[nextPlayer].Symbol);
                                Users[nextPlayer].CordinationPlayer += firstCube + secondCube;
                                ((Bot)Users[nextPlayer]).CheckCell(Field.Buldings[Users[nextPlayer].CordinationPlayer], Users, Field);
                            }
                        }
                        if (((Bot)Users[nextPlayer]).Auction)
                        {
                            Auction(Field.Buldings[Users[nextPlayer].CordinationPlayer], Users[nextPlayer].Symbol);
                            ((Bot)Users[nextPlayer]).Auction = false;
                        }
                        ShowField($"Бот кинул кубики число первого кубика [{firstCube}] число второго кубика [{secondCube}]");
                        if (firstCube != secondCube)
                        {
                            check = false;
                            luckBot = 0;
                        }
                        else
                        {
                            luckBot++;
                        }
                    }
                } //fix 
                else
                {
                    GameMenu gameMenu;
                    bool check = true;
                    if (Users[nextPlayer].Jackpot == true)
                    {
                        Console.WriteLine("{ 1 } Сыграть в джекпот | { 2 } Отказатся от игры ");
                        Console.Write("{ Ввод } >> ");
                        int.TryParse(Console.ReadLine(), out int choise);
                        if (choise == 1)
                        {
                            Users[nextPlayer].Balance -= 1000;
                            bool checkJack = false;
                            Console.WriteLine(" Введите количество кубиков не больше 3 ");
                            int.TryParse(Console.ReadLine(), out int number);
                            int[] arrayCubs = new int[number];
                            for (int i = 0; i < arrayCubs.Length; i++)
                            {
                                Console.Write("{ Ввод } >> ");
                                int.TryParse(Console.ReadLine(), out number);
                                while (number < 1 || number > 6 || checkJack)
                                {
                                    Console.Write("{ Ввод } >> ");
                                    int.TryParse(Console.ReadLine(), out number);
                                    for (int j = 0; j < arrayCubs.Length; j++)
                                    {
                                        if (arrayCubs[i] == number)
                                        {
                                            checkJack = true;
                                            break;
                                        }
                                        else
                                        {
                                            checkJack = false;
                                        }
                                    }
                                }
                                arrayCubs[i] = number;
                            }//проверка ввода чисел
                            firstCube = RollTheCube(rand);
                            for (int i = 0; i < arrayCubs.Length; i++)
                            {
                                if (arrayCubs[i] == firstCube)
                                {
                                    if (arrayCubs.Length == 3)
                                    {
                                        Users[nextPlayer].Balance += 2000;
                                    }
                                    else if (arrayCubs.Length == 2)
                                    {
                                        Users[nextPlayer].Balance += 3000;
                                    }
                                    else if (arrayCubs.Length == 1)
                                    {
                                        Users[nextPlayer].Balance += 6000;
                                    }
                                    break;
                                }
                            }//проверка выигрыша
                        }
                        Users[nextPlayer].Jackpot = false;

                    }
                    while (check)
                    {
                        if (luck == 3)
                        {
                            ((Player)Users[nextPlayer]).Prison = true;
                        }
                        //Console.Clear();
                        ShowField("");
                        ShowGameMenu();
                        Console.Write("{ Ввод } > ");
                        Enum.TryParse(Console.ReadLine(), out gameMenu);
                        switch (gameMenu)
                        {
                            case GameMenu.ThrowCubes://кинуть кубики 
                                {
                                    if (prison == true)
                                    {
                                        Console.WriteLine(" { 1 } Кинуть кубики | { 2 } Заплатить 500");
                                        Console.Write("{ Ввод } > ");
                                        int.TryParse(Console.ReadLine(), out int number);
                                        if (number == 1)
                                        {
                                            firstCube = RollTheCube(rand);
                                            secondCube = RollTheCube(rand);
                                            if (firstCube == secondCube)
                                            {
                                                prison = false;
                                                ((Player)Users[nextPlayer]).Prison = false;
                                            }
                                        }
                                        else if (number == 2)
                                        {
                                            if (Field.Buldings[((Player)Users[nextPlayer]).CordinationPlayer].GetType() == typeof(Prison))
                                            {
                                                ((Player)Users[nextPlayer]).Balance -= ((Prison)Field.Buldings[((Player)Users[nextPlayer]).CordinationPlayer]).ExitCost;
                                                prison = false;
                                                ((Player)Users[nextPlayer]).Prison = false;
                                            }
                                        }
                                        else
                                        {
                                            Console.ForegroundColor = ConsoleColor.DarkRed;
                                            Console.WriteLine("Неверное здначение для выбора !");
                                            Console.ForegroundColor = ConsoleColor.Gray;
                                        }
                                    }
                                    else
                                    {
                                        firstCube = RollTheCube(rand);
                                        secondCube = RollTheCube(rand);
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
                                        }
                                        else
                                        {
                                            luck = 0;
                                            check = false;
                                        }
                                    }
                                    if (surrender != true || prison != true || skipping != true)
                                    {
                                        ShowGameCube(firstCube);
                                        ShowGameCube(secondCube);
                                        Thread.Sleep(2000);
                                        if ((Users[nextPlayer].CordinationPlayer + firstCube + secondCube) >= Field.Buldings.Count)
                                        {
                                            if (Users[nextPlayer].ReverseStroke == true)
                                            {
                                                Field.Buldings[Users[nextPlayer].CordinationPlayer].Symbol.Remove(Users[nextPlayer].Symbol);
                                                Users[nextPlayer].CordinationPlayer -= firstCube + secondCube + Field.Buldings.Count;
                                                Field.Buldings[Users[nextPlayer].CordinationPlayer].Symbol.Add(Users[nextPlayer].Symbol);
                                                ((Player)Users[nextPlayer]).CheckCell(Field.Buldings[Users[nextPlayer].CordinationPlayer], Field); // F
                                                Users[nextPlayer].ReverseStroke = false;
                                            }//если игроку выпал шанс ход в обратную сторону
                                            else
                                            {
                                                Field.Buldings[Users[nextPlayer].CordinationPlayer].Symbol.Remove(Users[nextPlayer].Symbol);
                                                Users[nextPlayer].CordinationPlayer += firstCube + secondCube - Field.Buldings.Count;
                                                Field.Buldings[Users[nextPlayer].CordinationPlayer].Symbol.Add(Users[nextPlayer].Symbol);
                                                ((Player)Users[nextPlayer]).CheckCell(Field.Buldings[Users[nextPlayer].CordinationPlayer], Field); // F
                                                Users[nextPlayer].Balance += 2000;
                                            }
                                        }//если игрок вышел за приделы поля 
                                        else
                                        {
                                            if (Users[nextPlayer].ReverseStroke == true)
                                            {
                                                Field.Buldings[Users[nextPlayer].CordinationPlayer - (firstCube + secondCube) + Field.Buldings.Count].Symbol.Add(Users[nextPlayer].Symbol);//f
                                                Field.Buldings[Users[nextPlayer].CordinationPlayer].Symbol.Remove(Users[nextPlayer].Symbol);
                                                Users[nextPlayer].CordinationPlayer -= firstCube + secondCube + Field.Buldings.Count;
                                                ((Player)Users[nextPlayer]).CheckCell(Field.Buldings[Users[nextPlayer].CordinationPlayer], Field); // F
                                                Users[nextPlayer].ReverseStroke = false;
                                            }//если игроку выпал шанс ход в обратную сторону
                                            else
                                            {
                                                Field.Buldings[Users[nextPlayer].CordinationPlayer + firstCube + secondCube].Symbol.Add(Users[nextPlayer].Symbol);
                                                Field.Buldings[Users[nextPlayer].CordinationPlayer].Symbol.Remove(Users[nextPlayer].Symbol);
                                                Users[nextPlayer].CordinationPlayer += firstCube + secondCube;
                                                ((Player)Users[nextPlayer]).CheckCell(Field.Buldings[Users[nextPlayer].CordinationPlayer], Field); // F
                                            }
                                        }//если игрок дивгается по пределам поля 
                                        //Console.Clear();
                                        ShowField("");
                                        if (((Player)Users[nextPlayer]).IsCheckCellNotBsn(Field.Buldings[Users[nextPlayer].CordinationPlayer]))
                                        {
                                            if (Field.Buldings[Users[nextPlayer].CordinationPlayer].GetType() == typeof(Bank))
                                            {
                                                while (menu)
                                                {
                                                    ShowPayMenu("Выплатить налог", choose = 0);
                                                    Enum.TryParse(Console.ReadLine(), out buyMenu);
                                                    switch (buyMenu)
                                                    {
                                                        case BuyMenu.BuyBsn:
                                                            {
                                                                ((Player)Users[nextPlayer]).Balance -= ((Bank)Field.Buldings[Users[nextPlayer].CordinationPlayer]).Summa;
                                                                Console.WriteLine($"Игрок {((Player)Users[nextPlayer]).Symbol} попал на банк и у него снимают списывают {((Bank)Field.Buldings[Users[nextPlayer].CordinationPlayer]).Summa}");
                                                                Thread.Sleep(2000);
                                                                menu = false;
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
                                                                if (numberCell >= maxFieldCount || numberCell < 0 || ((Player)Users[nextPlayer]).LayACell(Field.Buldings[numberCell], numberCell))
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
                                            }
                                            else if (Field.Buldings[Users[nextPlayer].CordinationPlayer].GetType() == typeof(Tax))
                                            {
                                                while (menu)
                                                {
                                                    ShowPayMenu("Выплатить налога на роскаш", choose = 1);
                                                    Enum.TryParse(Console.ReadLine(), out buyMenu);
                                                    switch (buyMenu)
                                                    {
                                                        case BuyMenu.BuyBsn:
                                                            {
                                                                ((Player)Users[nextPlayer]).Balance -= ((Tax)Field.Buldings[Users[nextPlayer].CordinationPlayer]).Summa;
                                                                Console.WriteLine($"Игрок {((Player)Users[nextPlayer]).Symbol} вы попали на ячейку налог {((Tax)Field.Buldings[Users[nextPlayer].CordinationPlayer]).Summa}");
                                                                Thread.Sleep(2000);
                                                                menu = false;
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
                                                                if (numberCell >= maxFieldCount || numberCell < 0 || ((Player)Users[nextPlayer]).LayACell(Field.Buldings[numberCell], numberCell))
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
                                                        case BuyMenu.Auction:
                                                            { Auction(Field.Buldings[Users[nextPlayer].CordinationPlayer], Users[nextPlayer].Symbol); menu = false; }
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
                                            }
                                        }
                                        else if (((Player)Users[nextPlayer]).IsCheckCellBsn(Field.Buldings[Users[nextPlayer].CordinationPlayer]))
                                        {
                                            if (((Player)Users[nextPlayer]).IsCehckByCell(Field.Buldings[Users[nextPlayer].CordinationPlayer]) == true)
                                            {
                                                while (menu)
                                                {
                                                    ShowPayMenu("Купить бизнес", choose = 0);
                                                    Enum.TryParse(Console.ReadLine(), out buyMenu);
                                                    switch (buyMenu)
                                                    {
                                                        case BuyMenu.BuyBsn:
                                                            { ((Player)Users[nextPlayer]).IsByCell(Field.Buldings[Users[nextPlayer].CordinationPlayer],Field.Buldings); menu = false; }
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
                                                                if (numberCell >= maxFieldCount || numberCell < 0 || ((Player)Users[nextPlayer]).LayACell(Field.Buldings[numberCell], numberCell))
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
                                                while (menu)
                                                {
                                                    ShowPayMenu("Оплатить ренту", choose = 1);
                                                    Console.Write("{ Ввод } > ");
                                                    Enum.TryParse(Console.ReadLine(), out payMenu);
                                                    switch (payMenu)
                                                    {
                                                        case PayMenu.RentPayment:
                                                            { ((Player)Users[nextPlayer]).PayRent(Field.Buldings[Users[nextPlayer].CordinationPlayer], Users); menu = false; }
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
                                                                if (numberCell >= maxFieldCount || numberCell < 0 || ((Player)Users[nextPlayer]).LayACell(Field.Buldings[numberCell], numberCell))
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
                                            }//выплата ренты ячейки
                                        }
                                        ShowField("");
                                        menu = true;
                                    }
                                }
                                break;
                            case GameMenu.SellTheBusiness://заложить бизнес
                                {
                                    //if (ShowMyBsn(((Player)Users[nextPlayer]).Symbol))
                                    //{
                                    //    Console.ForegroundColor = ConsoleColor.DarkRed;
                                    //    Console.WriteLine("Нету бизнесов которые можно заложить");
                                    //    Console.ForegroundColor = ConsoleColor.Gray;
                                    //    Thread.Sleep(2000);
                                    //    break;
                                    //}
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
                                    if (numberCell >= maxFieldCount || numberCell < 0 || ((Player)Users[nextPlayer]).LayACell(Field.Buldings[numberCell], numberCell))
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
                                    if (numberCell >= maxFieldCount || numberCell < 0 || ((Player)Users[nextPlayer]).BsnBuyout(Field.Buldings[numberCell], numberCell))
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
