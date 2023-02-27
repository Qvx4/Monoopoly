
using System;
using System.Collections.Generic;
using System.Linq;
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
            if (name == "" || name == " ")
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
        public void ShowMyBsn(char symbol)
        {
            for (int i = 0; i < Field.Buldings.Count; i++)
            {
                if (Field.Buldings[i].GetType() == typeof(Business))
                {
                    if (((Business)Field.Buldings[i]).BusinessOwner == symbol)
                    {
                        if (((Business)Field.Buldings[i]).Mortgaged == false)
                        {
                            Console.WriteLine($"{i} > [ {Field.Buldings[i].Title} ] ");
                        }
                    }
                }
                else if (Field.Buldings[i].GetType() == typeof(CarInterior))
                {
                    if (((CarInterior)Field.Buldings[i]).BusinessOwner == symbol)
                    {
                        if (((CarInterior)Field.Buldings[i]).Mortgaged == false)
                        {
                            Console.WriteLine($"{i} > [ {Field.Buldings[i].Title} ] ");
                        }
                    }
                }
                else if (Field.Buldings[i].GetType() == typeof(GamingCompanies))
                {
                    if (((GamingCompanies)Field.Buldings[i]).BusinessOwner == symbol)
                    {
                        if (((GamingCompanies)Field.Buldings[i]).Mortgaged == false)
                        {
                            Console.WriteLine($"{i} > [ {Field.Buldings[i].Title} ] ");
                        }
                    }
                }
            }
        }//Вывод бизнесов игрока которые не заложенные
        public void ShowMortgagedBsn(char symbol)
        {
            for (int i = 0; i < Field.Buldings.Count; i++)
            {
                if (Field.Buldings[i].GetType() == typeof(Business))
                {
                    if (((Business)Field.Buldings[i]).BusinessOwner == symbol)
                    {
                        if (((Business)Field.Buldings[i]).Mortgaged == true)
                        {
                            Console.WriteLine($"{i} > [ {Field.Buldings[i].Title} ]");
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
                        }
                    }
                }
            }
        }//вывод заложенных бизнесов
        public void ShowAllBsn(char symbol)
        {
            for (int i = 0; i < Field.Buldings.Count; i++)
            {
                if (Field.Buldings[i].GetType() == typeof(Business))
                {
                    if (((Business)Field.Buldings[i]).BusinessOwner == symbol)
                    {
                        Console.WriteLine($"{i} > [ {Field.Buldings[i].Title} ] ");
                    }
                }
                else if (Field.Buldings[i].GetType() == typeof(CarInterior))
                {
                    if (((CarInterior)Field.Buldings[i]).BusinessOwner == symbol)
                    {
                        Console.WriteLine($"{i} > [ {Field.Buldings[i].Title} ] ");
                    }
                }
                else if (Field.Buldings[i].GetType() == typeof(GamingCompanies))
                {
                    if (((GamingCompanies)Field.Buldings[i]).BusinessOwner == symbol)
                    {
                        Console.WriteLine($"{i} > [ {Field.Buldings[i].Title} ] ");
                    }
                }
            }
        }//вывод всех бизнесов
        public bool IsCheckWinGame()
        {
            int playerIsWin = 0;
            for (int i = 0; i < Users.Count; i++)
            {
                if (Users[i].Surrender != true)
                {
                    playerIsWin++;
                }
            }
            if (playerIsWin == 1)
            {
                return true;
            }
            return false;
        }//проверка на победу 
        public void Auction(List<User> users, Building bulding)
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
                user.Add(Users[i]);
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
            if (user[nextPlayer].GetType() == typeof(Bot))
            {
                user[nextPlayer].Balance -= ((Business)bulding).Price;
                if (businessType.GetType() == typeof(Business))
                {
                    ((Business)bulding).BusinessOwner = user[nextPlayer].Symbol;
                }
                if (businessType.GetType() == typeof(CarInterior))
                {
                    ((CarInterior)bulding).BusinessOwner = user[nextPlayer].Symbol;
                }
                if (businessType.GetType() == typeof(GamingCompanies))
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
        }//Добовление Бота
        public void AddPlayer(Player player)
        {
            Users.Add(player);
        }//Добовление Игрока
        public bool DeletUser(string name)
        {
            for (int i = 0; i < Users.Count; i++)
            {
                if (Users[i].Name == name)
                {
                    Users.RemoveAt(i);
                    return true;
                }
            }
            return false;
        }//Удаление Игроков
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
            Console.WriteLine("{1} Кинуть кубики ");
            Console.WriteLine("{2} Заложить Бизнес ");
            Console.WriteLine("{3} Выкупить свой бизнес ");
            Console.WriteLine("{4} Предложить трейд игроку ");
            Console.WriteLine("{5} Покупка филиала ");
            Console.WriteLine("{6} Сдатся ");
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
         //Show
        public void StartGame()
        {
            for (int i = 0; i < Users.Count; i++)
            {
                Field.Buldings[0].Symbol.Add(Users[i].Symbol);
            }
            Users[2].Balance -= 11000;
            Random rand = new Random();
            int numberCell;
            int nextPlayer = 0;
            int firstCube = 0;
            int secondCube = 0;
            int luck = 0;
            bool surrender = false;
            bool prison = false;
            bool skipping = false;
            //int lastCellNumber = 0;
            while (true)
            {
                if (Users[nextPlayer].Surrender == true)
                {
                    nextPlayer++;
                    if (nextPlayer >= Users.Count)
                    {
                        nextPlayer = 0;
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
                        int prisonPrice = 500;
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
                        }//доделать выход с тюрьмы
                        if (((Bot)Users[nextPlayer]).IsHaveMeMonoopoly(Field.Buldings))
                        {
                            ((Bot)Users[nextPlayer]).MonoopolyImprovement1(((Bot)Users[nextPlayer]).MonoopolyImprovement(Field.Buldings));
                        }
                        if (luckBot == 3)
                        {
                            ((Bot)Users[nextPlayer]).Prison = true;
                        }
                        Console.Clear();
                        ShowField("");
                        firstCube = RollTheCube(rand);
                        secondCube = RollTheCube(rand);
                        ShowGameCube(firstCube);
                        ShowGameCube(secondCube);
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
                            Auction(Users, Field.Buldings[Users[nextPlayer].CordinationPlayer]);
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
                        Console.Clear();
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
                                    }
                                    else
                                    {
                                        firstCube = RollTheCube(rand);
                                        secondCube = RollTheCube(rand);
                                        //firstCube = 4;
                                        //secondCube = 6;
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
                                }
                                break;
                            case GameMenu.SellTheBusiness://заложить бизнес
                                {
                                    ShowMyBsn(((Player)Users[nextPlayer]).Symbol);
                                    Console.Write("{ Ввод } > ");
                                    int.TryParse(Console.ReadLine(), out numberCell);
                                    ((Player)Users[nextPlayer]).LayACell(Field.Buldings[numberCell]);
                                }
                                break;
                            case GameMenu.BuyOutYourBusiness://выкупить бизнеса
                                {
                                    ShowMortgagedBsn(((Player)Users[nextPlayer]).Symbol);
                                    Console.Write("{ Ввод } > ");
                                    int.TryParse(Console.ReadLine(), out numberCell);
                                    ((Player)Users[nextPlayer]).BsnBuyout(Field.Buldings[numberCell]);
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
                                    Console.ForegroundColor = ConsoleColor.Gray;
                                }
                                break;
                            case GameMenu.PurchaseBranch://доделать постойку улучшений 
                                {
                                    if (((Player)Users[nextPlayer]).IsHaveMeMonoopoly(Field.Buldings))
                                    {
                                        ((Player)Users[nextPlayer]).ShowBsn(Field.Buldings);
                                        int.TryParse(Console.ReadLine(), out numberCell);
                                        ((Player)Users[nextPlayer]).MonoopolyImprovement((Business)Field.Buldings[numberCell]);
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
                                    ((Player)Users[nextPlayer]).CheckCell(Field.Buldings[Users[nextPlayer].CordinationPlayer], Field);
                                    Users[nextPlayer].ReverseStroke = false;
                                }//если игроку выпал шанс ход в обратную сторону
                                else
                                {
                                    Field.Buldings[Users[nextPlayer].CordinationPlayer].Symbol.Remove(Users[nextPlayer].Symbol);
                                    Users[nextPlayer].CordinationPlayer += firstCube + secondCube - Field.Buldings.Count;
                                    Field.Buldings[Users[nextPlayer].CordinationPlayer].Symbol.Add(Users[nextPlayer].Symbol);
                                    ((Player)Users[nextPlayer]).CheckCell(Field.Buldings[Users[nextPlayer].CordinationPlayer], Field);
                                    Users[nextPlayer].Balance += 2000;
                                }
                            }//если игрок вышел за приделы поля 
                            else
                            {
                                if (Users[nextPlayer].ReverseStroke == true)
                                {
                                    Field.Buldings[Users[nextPlayer].CordinationPlayer - (firstCube + secondCube)].Symbol.Add(Users[nextPlayer].Symbol);
                                    Field.Buldings[Users[nextPlayer].CordinationPlayer].Symbol.Remove(Users[nextPlayer].Symbol);
                                    Users[nextPlayer].CordinationPlayer -= firstCube + secondCube;
                                    ((Player)Users[nextPlayer]).CheckCell(Field.Buldings[Users[nextPlayer].CordinationPlayer], Field);
                                    Users[nextPlayer].ReverseStroke = false;
                                }//если игроку выпал шанс ход в обратную сторону
                                else
                                {
                                    Field.Buldings[Users[nextPlayer].CordinationPlayer + firstCube + secondCube].Symbol.Add(Users[nextPlayer].Symbol);
                                    Field.Buldings[Users[nextPlayer].CordinationPlayer].Symbol.Remove(Users[nextPlayer].Symbol);
                                    Users[nextPlayer].CordinationPlayer += firstCube + secondCube;
                                    ((Player)Users[nextPlayer]).CheckCell(Field.Buldings[Users[nextPlayer].CordinationPlayer], Field);
                                }
                            }//если игрок дивгается по пределам поля 
                            Console.Clear();
                            ShowField("");
                            if (((Player)Users[nextPlayer]).IsCheckCellNotBis(Field.Buldings[Users[nextPlayer].CordinationPlayer]) == false)
                            {
                                if (((Player)Users[nextPlayer]).IsCehckByCell(Field.Buldings[Users[nextPlayer].CordinationPlayer]) == false)
                                {
                                    Console.WriteLine("Купить бизнес {1} Да / {2} Нет");
                                    Console.Write("{ Ввод } > ");
                                    int.TryParse(Console.ReadLine(), out int number);
                                    if (number == 1)
                                    {
                                        ((Player)Users[nextPlayer]).IsByCell(Field.Buldings[Users[nextPlayer].CordinationPlayer]);
                                    }
                                    else
                                    {
                                        Auction(Users, Field.Buldings[Users[nextPlayer].CordinationPlayer]);
                                    }
                                }//покупка ячейки 
                                else
                                {
                                    Console.WriteLine("{1} Оплатить ренту / {2} Сдатся");
                                    Console.Write("{ Ввод } > ");
                                    int.TryParse(Console.ReadLine(), out int number);
                                    if (number == 1)
                                    {
                                        ((Player)Users[nextPlayer]).PayRent(Field.Buldings[Users[nextPlayer].CordinationPlayer], Users);
                                    }
                                    else
                                    {
                                        Users[nextPlayer].Surrender = true;
                                    }//сдатся
                                }//выплата ренты ячейки
                            }
                            ShowField($"Игрок {Users[nextPlayer].Name} кинул кубики число первого кубика [{firstCube}] число второго кубика [{secondCube}]");
                        }
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
