using System;
using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

namespace MonopolyV20
{
    public class Field
    {
        public List<Building> Buldings { get; set; }
        public Field()
        {
            Buldings = new List<Building>();
            AddingField();
        }

        public ConsoleColor FieldPainting(Building Bulding, List<User> users)
        {
            if (Bulding.GetType() == typeof(Business))
            {
                if (((Business)Bulding).BusinessOwner != '\0')
                {
                    return SerchColorUser(((Business)Bulding).BusinessOwner, users);
                }
            }//иф который делает провку на куленный бизнес
            else if (Bulding.GetType() == typeof(CarInterior))
            {
                if (((CarInterior)Bulding).BusinessOwner != '\0')
                {
                    return SerchColorUser(((CarInterior)Bulding).BusinessOwner, users);
                }
            }//иф который делает провку на куленный центрАвто
            else if (Bulding.GetType() == typeof(GamingCompanies))
            {
                if (((GamingCompanies)Bulding).BusinessOwner != '\0')
                {
                    return SerchColorUser(((GamingCompanies)Bulding).BusinessOwner, users);
                }
            }//иф который делает провку на куленный ИгроваяКомпания
            return ConsoleColor.Black;
        }
        public ConsoleColor SerchColorUser(char symbol, List<User> users)
        {
            for (int i = 0; i < users.Count; i++)
            {
                if (users[i].Symbol == symbol)
                {
                    return users[i].Color;
                }
            }
            return ConsoleColor.Black;
        }
        public void AddingField()
        {
            Buldings.Add(new Start("Старт", 0, 1000));
            Buldings.Add(new Business("CHANEL", 1, 600, 360, 300, 0, 500, new List<int>() { 20, 100, 300, 900, 1600, 2500 }, BusinessType.Perfumery));
            Buldings.Add(new Chance("Шанс", 2));
            ((Chance)Buldings[Buldings.Count - 1]).AddChance();
            Buldings.Add(new Business("HUGO BOSS", 3, 600, 360, 300, 0, 500, new List<int>() { 40, 200, 600, 1800, 3200, 4500 }, BusinessType.Perfumery));
            Buldings.Add(new Bank("Банк", 4, 2000));
            Buldings.Add(new CarInterior("Mercedes", 5, 2000, 1200, 1000, 0, new List<int>() { 250, 500, 1000, 2000 }, BusinessType.Car));
            Buldings.Add(new Business("Adidas", 6, 1000, 600, 500, 0, 500, new List<int>() { 60, 300, 900, 2700, 4000, 5500 }, BusinessType.ClothingStores));
            Buldings.Add(new Chance("Шанс", 7));
            ((Chance)Buldings[Buldings.Count - 1]).AddChance();
            Buldings.Add(new Business("Puma", 8, 1000, 600, 500, 0, 500, new List<int>() { 60, 300, 900, 2700, 4000, 5500 }, BusinessType.ClothingStores));
            Buldings.Add(new Business("LACOSTE", 9, 1200, 720, 600, 0, 500, new List<int>() { 80, 400, 1000, 3000, 4500, 6000 }, BusinessType.ClothingStores));
            Buldings.Add(new Prison("Тюрьма", 10, 500, 3));
            Buldings.Add(new Business("Vk", 11, 1400, 840, 700, 0, 750, new List<int>() { 100, 500, 1500, 4500, 6250, 7500 }, BusinessType.WebServices));
            Buldings.Add(new GamingCompanies("Rockstar Games", 12, 1500, 750, 900, 0, new List<int>() { 100, 250 }, BusinessType.GameCorparation));
            Buldings.Add(new Business("Facebook", 13, 1400, 840, 700, 0, 750, new List<int>() { 100, 513, 1537, 4613, 6406, 7687 }, BusinessType.WebServices));
            Buldings.Add(new Business("Twitter", 14, 1600, 960, 800, 0, 750, new List<int>() { 120, 600, 1800, 5000, 7000, 9000 }, BusinessType.WebServices));
            Buldings.Add(new CarInterior("Audi", 15, 2000, 1200, 1000, 0, new List<int>() { 250, 500, 1000, 2000 }, BusinessType.Car));
            Buldings.Add(new Business("Coca-Cola", 16, 1800, 1080, 900, 0, 1000, new List<int>() { 140, 700, 2000, 5500, 7500, 9500 }, BusinessType.BeverageCompanies));
            Buldings.Add(new Chance("Шанс", 17));
            ((Chance)Buldings[Buldings.Count - 1]).AddChance();
            Buldings.Add(new Business("Pepsi", 18, 1800, 1080, 900, 0, 1000, new List<int>() { 140, 700, 2000, 5500, 7500, 9500 }, BusinessType.BeverageCompanies));
            Buldings.Add(new Business("Fanta", 19, 2000, 1200, 1000, 0, 1000, new List<int>() { 160, 800, 2200, 6000, 8000, 10000 }, BusinessType.BeverageCompanies));
            Buldings.Add(new Jackpot("JACKPOT", 20));
            Buldings.Add(new Business("AmerAirlin", 21, 2200, 1400, 1320, 0, 1250, new List<int>() { 180, 900, 2500, 7000, 8750, 10500 }, BusinessType.Airlines));
            Buldings.Add(new Chance("Шанс", 22));
            ((Chance)Buldings[Buldings.Count - 1]).AddChance();
            Buldings.Add(new Business("Lufthansa", 23, 2200, 1320, 1100, 0, 1250, new List<int>() { 180, 900, 2500, 7000, 8750, 10500 }, BusinessType.Airlines));
            Buldings.Add(new Business("Brit.Air", 24, 2400, 1440, 1200, 0, 1250, new List<int>() { 200, 1000, 3000, 7500, 9250, 11000 }, BusinessType.Airlines));
            Buldings.Add(new CarInterior("Ford", 25, 2000, 1200, 1000, 0, new List<int>() { 250, 500, 1000, 2000 }, BusinessType.Car));
            Buldings.Add(new Business("McDonalds", 26, 2600, 1560, 1300, 0, 1500, new List<int>() { 220, 1100, 3300, 8000, 9750, 11500 }, BusinessType.Restaurants));
            Buldings.Add(new Business("BurgerKing", 27, 2600, 1560, 1300, 0, 1500, new List<int>() { 220, 1100, 3300, 8000, 9750, 11500 }, BusinessType.Restaurants));
            Buldings.Add(new GamingCompanies("Rovio", 28, 1500, 900, 750, 0, new List<int>() { 100, 250 }, BusinessType.GameCorparation));
            Buldings.Add(new Business("KFC", 29, 2800, 1680, 1400, 0, 1500, new List<int>() { 240, 1200, 3600, 8500, 10250, 12000 }, BusinessType.Restaurants));
            Buldings.Add(new PoliceStation("Полицейский участок", 30));
            Buldings.Add(new Business("Holiday Inn", 31, 3000, 1800, 1400, 0, 1750, new List<int>() { 260, 1300, 3900, 9000, 11000, 12750 }, BusinessType.Hotels));
            Buldings.Add(new Business("Radissom Blu", 32, 3000, 1800, 1500, 0, 1750, new List<int>() { 260, 1300, 3900, 9000, 11000, 12750 }, BusinessType.Hotels));
            Buldings.Add(new Chance("Шанс", 33));
            ((Chance)Buldings[Buldings.Count - 1]).AddChance();
            Buldings.Add(new Business("Novotel", 34, 3200, 1920, 1600, 0, 1750, new List<int>() { 280, 1500, 4500, 10000, 12000, 14000 }, BusinessType.Hotels));
            Buldings.Add(new CarInterior("Land Rover", 35, 2000, 1000, 1200, 0, new List<int>() { 250, 500, 1000, 2000 }, BusinessType.Car));
            Buldings.Add(new Tax("Налог", 36, 100));
            Buldings.Add(new Business("Apple", 37, 3500, 2100, 1750, 0, 2000, new List<int>() { 350, 1750, 5000, 11000, 13000, 15000 }, BusinessType.Electronics));
            Buldings.Add(new Chance("Шанс", 38));
            ((Chance)Buldings[Buldings.Count - 1]).AddChance();
            Buldings.Add(new Business("Nokia", 39, 4000, 2400, 2000, 0, 2000, new List<int>() { 500, 2000, 6000, 14000, 17000, 20000 }, BusinessType.Electronics));

        }//Добовление категорий в ячейки 
        public void TopLineOutput(int[] cellNumber, List<User> users)
        {
            int count = 10;
            int countCells = 11;
            int next = 0;
            bool checkByCells = false;
            Random random = new Random();
            ConsoleColor consoleColorType = ConsoleColor.Black;
            for (int i = 0; i < countCells; i++)
            {
                if (i == 10)
                {
                    count = 10;
                }
                Console.Write("┌");
                for (int j = 0; j < count; j++)
                {
                    Console.Write("──");
                }
                Console.Write("┐ ");
                count = 5;
            }
            for (int i = 0; i < 8; i++)
            {
                Console.WriteLine();
                count = 20;
                for (int j = 0; j < countCells; j++)
                {
                    if (j == 10)
                    {
                        count = 20;
                    }
                    Console.Write("│");//сделать для автосалона и игровых команий
                    if (Buldings[cellNumber[j]].GetType() == typeof(Business))//иф который делает провку на куленный бизнес
                    {
                        if (((Business)Buldings[cellNumber[j]]).BusinessOwner != ' ')
                        {
                            checkByCells = true;
                            consoleColorType = SerchColorUser(((Business)Buldings[cellNumber[j]]).BusinessOwner, users);
                        }
                    }
                    else if (Buldings[cellNumber[j]].GetType() == typeof(CarInterior))
                    {
                        if (((CarInterior)Buldings[cellNumber[j]]).BusinessOwner != ' ')
                        {
                            checkByCells = true;
                            consoleColorType = SerchColorUser(((CarInterior)Buldings[cellNumber[j]]).BusinessOwner, users);
                        }
                    }
                    else if (Buldings[cellNumber[j]].GetType() == typeof(GamingCompanies))
                    {
                        if (((GamingCompanies)Buldings[cellNumber[j]]).BusinessOwner != ' ')
                        {
                            checkByCells = true;
                            consoleColorType = SerchColorUser(((GamingCompanies)Buldings[cellNumber[j]]).BusinessOwner, users);
                        }
                    }
                    if (i == 0)
                    {
                        if (Buldings[cellNumber[j]].GetType() == typeof(Business))
                        {
                            bool isColor = true;
                            if (checkByCells == true)
                            {
                                Console.BackgroundColor = consoleColorType;
                            }
                            if (((Business)Buldings[cellNumber[j]]).Mortgaged == true)
                            {
                                for (int k = 0; k < Buldings[cellNumber[j]].Title.Length; k++)
                                {
                                    if (isColor) Console.BackgroundColor = consoleColorType;
                                    else Console.BackgroundColor = ConsoleColor.Black;
                                    isColor = !isColor;
                                    Console.Write(Buldings[cellNumber[j]].Title[k]);
                                }
                            }
                            else Console.Write($"{Buldings[cellNumber[j]].Title}");

                            for (int k = 0; k < count - Buldings[cellNumber[j]].Title.Length; k++)
                            {
                                if (((Business)Buldings[cellNumber[j]]).Mortgaged == true)
                                {
                                    if (isColor) Console.BackgroundColor = consoleColorType;
                                    else Console.BackgroundColor = ConsoleColor.Black;
                                    isColor = !isColor;
                                }
                                Console.Write(" ");
                            }
                        }
                        else if (Buldings[cellNumber[j]].GetType() == typeof(CarInterior))
                        {
                            bool isColor = true;
                            if (checkByCells == true)
                            {
                                Console.BackgroundColor = consoleColorType;
                            }
                            if (((CarInterior)Buldings[cellNumber[j]]).Mortgaged == true)
                            {
                                for (int k = 0; k < Buldings[cellNumber[j]].Title.Length; k++)
                                {
                                    if (isColor) Console.BackgroundColor = consoleColorType;
                                    else Console.BackgroundColor = ConsoleColor.Black;
                                    isColor = !isColor;
                                    Console.Write(Buldings[cellNumber[j]].Title[k]);
                                }
                            }
                            else Console.Write($"{Buldings[cellNumber[j]].Title}");

                            for (int k = 0; k < count - Buldings[cellNumber[j]].Title.Length; k++)
                            {
                                if (((CarInterior)Buldings[cellNumber[j]]).Mortgaged == true)
                                {
                                    if (isColor) Console.BackgroundColor = consoleColorType;
                                    else Console.BackgroundColor = ConsoleColor.Black;
                                    isColor = !isColor;
                                }
                                Console.Write(" ");
                            }
                        }
                        else if (Buldings[cellNumber[j]].GetType() == typeof(GamingCompanies))
                        {
                            bool isColor = true;
                            if (checkByCells == true)
                            {
                                Console.BackgroundColor = consoleColorType;
                            }
                            if (((GamingCompanies)Buldings[cellNumber[j]]).Mortgaged == true)
                            {
                                for (int k = 0; k < Buldings[cellNumber[j]].Title.Length; k++)
                                {
                                    if (isColor) Console.BackgroundColor = consoleColorType;
                                    else Console.BackgroundColor = ConsoleColor.Black;
                                    isColor = !isColor;
                                    Console.Write(Buldings[cellNumber[j]].Title[k]);
                                }
                            }
                            else Console.Write($"{Buldings[cellNumber[j]].Title}");
                            for (int k = 0; k < count - Buldings[cellNumber[j]].Title.Length; k++)
                            {
                                if (((GamingCompanies)Buldings[cellNumber[j]]).Mortgaged == true)
                                {
                                    if (isColor) Console.BackgroundColor = consoleColorType;
                                    else Console.BackgroundColor = ConsoleColor.Black;
                                    isColor = !isColor;
                                }
                                Console.Write(" ");
                            }
                        }
                        else
                        {
                            Console.BackgroundColor = ConsoleColor.DarkYellow;
                            for (int k = 0; k < count; k++)
                            {
                                Console.BackgroundColor = ConsoleColor.DarkYellow;
                                Console.Write(" ");

                            }
                        }
                    }//Название
                    if (i == 1)
                    {
                        if (Buldings[cellNumber[j]].GetType() == typeof(Business))
                        {
                            bool isColor = false;
                            if (checkByCells == true)
                            {
                                Console.BackgroundColor = consoleColorType;
                            }
                            if (((Business)Buldings[cellNumber[j]]).Mortgaged == true)
                            {
                                for (int k = 0; k < ((Business)Buldings[cellNumber[j]]).Price.ToString().Length; k++)
                                {
                                    if (isColor) Console.BackgroundColor = consoleColorType;
                                    else Console.BackgroundColor = ConsoleColor.Black;
                                    isColor = !isColor;
                                    Console.Write(((Business)Buldings[cellNumber[j]]).Price.ToString()[k]);
                                }
                            }
                            else Console.Write($"{((Business)Buldings[cellNumber[j]]).Price}");
                            for (int k = 0; k < count - ((Business)Buldings[cellNumber[j]]).Price.ToString().Length; k++)
                            {
                                if (((Business)Buldings[cellNumber[j]]).Mortgaged == true)
                                {
                                    if (isColor) Console.BackgroundColor = consoleColorType;
                                    else Console.BackgroundColor = ConsoleColor.Black;
                                    isColor = !isColor;
                                }
                                Console.Write(" ");
                            }
                        }
                        else if (Buldings[cellNumber[j]].GetType() == typeof(CarInterior))
                        {
                            bool isColor = false;
                            if (checkByCells == true)
                            {
                                Console.BackgroundColor = consoleColorType;
                            }
                            if (((CarInterior)Buldings[cellNumber[j]]).Mortgaged == true)
                            {
                                for (int k = 0; k < ((CarInterior)Buldings[cellNumber[j]]).Price.ToString().Length; k++)
                                {
                                    if (isColor) Console.BackgroundColor = consoleColorType;
                                    else Console.BackgroundColor = ConsoleColor.Black;
                                    isColor = !isColor;
                                    Console.Write(((CarInterior)Buldings[cellNumber[j]]).Price.ToString()[k]);
                                }
                            }
                            else Console.Write($"{((CarInterior)Buldings[cellNumber[j]]).Price}");
                            for (int k = 0; k < count - ((CarInterior)Buldings[cellNumber[j]]).Price.ToString().Length; k++)
                            {
                                if (((CarInterior)Buldings[cellNumber[j]]).Mortgaged == true)
                                {
                                    if (isColor) Console.BackgroundColor = consoleColorType;
                                    else Console.BackgroundColor = ConsoleColor.Black;
                                    isColor = !isColor;
                                }
                                Console.Write(" ");
                            }
                        }
                        else if (Buldings[cellNumber[j]].GetType() == typeof(GamingCompanies))
                        {
                            bool isColor = false;
                            if (checkByCells == true)
                            {
                                Console.BackgroundColor = consoleColorType;
                            }
                            if (((GamingCompanies)Buldings[cellNumber[j]]).Mortgaged == true)
                            {
                                for (int k = 0; k < ((GamingCompanies)Buldings[cellNumber[j]]).Price.ToString().Length; k++)
                                {
                                    if (isColor) Console.BackgroundColor = consoleColorType;
                                    else Console.BackgroundColor = ConsoleColor.Black;
                                    isColor = !isColor;
                                    Console.Write(((GamingCompanies)Buldings[cellNumber[j]]).Price.ToString()[k]);
                                }
                            }
                            else Console.Write($"{((GamingCompanies)Buldings[cellNumber[j]]).Price}");
                            for (int k = 0; k < count - ((GamingCompanies)Buldings[cellNumber[j]]).Price.ToString().Length; k++)
                            {
                                if (((GamingCompanies)Buldings[cellNumber[j]]).Mortgaged == true)
                                {
                                    if (isColor) Console.BackgroundColor = consoleColorType;
                                    else Console.BackgroundColor = ConsoleColor.Black;
                                    isColor = !isColor;
                                }
                                Console.Write(" ");
                            }
                        }
                        else
                        {
                            Console.BackgroundColor = ConsoleColor.DarkYellow;
                            for (int k = 0; k < count; k++)
                            {
                                Console.Write(" ");
                            }
                        }

                    }//Цена покупки 
                    if (i == 2)
                    {
                        if (Buldings[cellNumber[j]].GetType() == typeof(Business))
                        {
                            bool isColor = true;
                            if (checkByCells == true)
                            {
                                Console.BackgroundColor = consoleColorType;
                            }
                            if (((Business)Buldings[cellNumber[j]]).Mortgaged == true)
                            {
                                for (int k = 0; k < ((Business)Buldings[cellNumber[j]]).RansomValue.ToString().Length; k++)
                                {
                                    if (isColor) Console.BackgroundColor = consoleColorType;
                                    else Console.BackgroundColor = ConsoleColor.Black;
                                    isColor = !isColor;
                                    Console.Write(((Business)Buldings[cellNumber[j]]).RansomValue.ToString()[k]);
                                }
                            }
                            else Console.Write($"{((Business)Buldings[cellNumber[j]]).RansomValue}");
                            for (int k = 0; k < count - ((Business)Buldings[cellNumber[j]]).RansomValue.ToString().Length; k++)
                            {
                                if (((Business)Buldings[cellNumber[j]]).Mortgaged == true)
                                {
                                    if (isColor) Console.BackgroundColor = consoleColorType;
                                    else Console.BackgroundColor = ConsoleColor.Black;
                                    isColor = !isColor;
                                }
                                Console.Write(" ");
                            }
                        }
                        else if (Buldings[cellNumber[j]].GetType() == typeof(CarInterior))
                        {
                            bool isColor = true;
                            if (checkByCells == true)
                            {
                                Console.BackgroundColor = consoleColorType;
                            }
                            if (((CarInterior)Buldings[cellNumber[j]]).Mortgaged == true)
                            {
                                for (int k = 0; k < ((CarInterior)Buldings[cellNumber[j]]).RansomValue.ToString().Length; k++)
                                {
                                    if (isColor) Console.BackgroundColor = consoleColorType;
                                    else Console.BackgroundColor = ConsoleColor.Black;
                                    isColor = !isColor;
                                    Console.Write(((CarInterior)Buldings[cellNumber[j]]).RansomValue.ToString()[k]);
                                }
                            }
                            else Console.Write($"{((CarInterior)Buldings[cellNumber[j]]).RansomValue}");
                            for (int k = 0; k < count - ((CarInterior)Buldings[cellNumber[j]]).RansomValue.ToString().Length; k++)
                            {
                                if (((CarInterior)Buldings[cellNumber[j]]).Mortgaged == true)
                                {
                                    if (isColor) Console.BackgroundColor = consoleColorType;
                                    else Console.BackgroundColor = ConsoleColor.Black;
                                    isColor = !isColor;
                                }
                                Console.Write(" ");
                            }
                        }
                        else if (Buldings[cellNumber[j]].GetType() == typeof(GamingCompanies))
                        {
                            bool isColor = true;
                            if (checkByCells == true)
                            {
                                Console.BackgroundColor = consoleColorType;
                            }
                            if (((GamingCompanies)Buldings[cellNumber[j]]).Mortgaged == true)
                            {
                                for (int k = 0; k < ((GamingCompanies)Buldings[cellNumber[j]]).RansomValue.ToString().Length; k++)
                                {
                                    if (isColor) Console.BackgroundColor = consoleColorType;
                                    else Console.BackgroundColor = ConsoleColor.Black;
                                    isColor = !isColor;
                                    Console.Write(((GamingCompanies)Buldings[cellNumber[j]]).RansomValue.ToString()[k]);
                                }
                            }
                            else Console.Write($"{((GamingCompanies)Buldings[cellNumber[j]]).RansomValue}");
                            for (int k = 0; k < count - ((GamingCompanies)Buldings[cellNumber[j]]).RansomValue.ToString().Length; k++)
                            {
                                if (((GamingCompanies)Buldings[cellNumber[j]]).Mortgaged == true)
                                {
                                    if (isColor) Console.BackgroundColor = consoleColorType;
                                    else Console.BackgroundColor = ConsoleColor.Black;
                                    isColor = !isColor;
                                }
                                Console.Write(" ");
                            }
                        }
                        else
                        {
                            Console.BackgroundColor = ConsoleColor.DarkYellow;
                            for (int k = 0; k < count; k++)
                            {
                                Console.Write(" ");
                            }
                        }
                    }//Цена залога
                    if (i == 3)
                    {
                        if (Buldings[cellNumber[j]].GetType() == typeof(Business))
                        {
                            bool isColor = false;
                            if (checkByCells == true)
                            {
                                Console.BackgroundColor = consoleColorType;
                            }
                            if (((Business)Buldings[cellNumber[j]]).Mortgaged == true)
                            {
                                for (int k = 0; k < ((Business)Buldings[cellNumber[j]]).ValueOfCollaterel.ToString().Length; k++)
                                {
                                    if (isColor) Console.BackgroundColor = consoleColorType;
                                    else Console.BackgroundColor = ConsoleColor.Black;
                                    isColor = !isColor;
                                    Console.Write(((Business)Buldings[cellNumber[j]]).ValueOfCollaterel.ToString()[k]);
                                }
                            }
                            else Console.Write($"{((Business)Buldings[cellNumber[j]]).ValueOfCollaterel}");
                            for (int k = 0; k < count - ((Business)Buldings[cellNumber[j]]).ValueOfCollaterel.ToString().Length; k++)
                            {
                                if (((Business)Buldings[cellNumber[j]]).Mortgaged == true)
                                {
                                    if (isColor) Console.BackgroundColor = consoleColorType;
                                    else Console.BackgroundColor = ConsoleColor.Black;
                                    isColor = !isColor;
                                }
                                Console.Write(" ");
                            }
                        }
                        else if (Buldings[cellNumber[j]].GetType() == typeof(CarInterior))
                        {
                            bool isColor = false;
                            if (checkByCells == true)
                            {
                                Console.BackgroundColor = consoleColorType;
                            }
                            if (((CarInterior)Buldings[cellNumber[j]]).Mortgaged == true)
                            {
                                for (int k = 0; k < ((CarInterior)Buldings[cellNumber[j]]).ValueOfCollaterel.ToString().Length; k++)
                                {
                                    if (isColor) Console.BackgroundColor = consoleColorType;
                                    else Console.BackgroundColor = ConsoleColor.Black;
                                    isColor = !isColor;
                                    Console.Write(((CarInterior)Buldings[cellNumber[j]]).ValueOfCollaterel.ToString()[k]);
                                }
                            }
                            else Console.Write($"{((CarInterior)Buldings[cellNumber[j]]).ValueOfCollaterel}");
                            for (int k = 0; k < count - ((CarInterior)Buldings[cellNumber[j]]).ValueOfCollaterel.ToString().Length; k++)
                            {
                                if (((CarInterior)Buldings[cellNumber[j]]).Mortgaged == true)
                                {
                                    if (isColor) Console.BackgroundColor = consoleColorType;
                                    else Console.BackgroundColor = ConsoleColor.Black;
                                    isColor = !isColor;
                                }
                                Console.Write(" ");
                            }
                        }
                        else if (Buldings[cellNumber[j]].GetType() == typeof(GamingCompanies))
                        {
                            bool isColor = false;
                            if (checkByCells == true)
                            {
                                Console.BackgroundColor = consoleColorType;
                            }
                            if (((GamingCompanies)Buldings[cellNumber[j]]).Mortgaged == true)
                            {
                                for (int k = 0; k < ((GamingCompanies)Buldings[cellNumber[j]]).ValueOfCollaterel.ToString().Length; k++)
                                {
                                    if (isColor) Console.BackgroundColor = consoleColorType;
                                    else Console.BackgroundColor = ConsoleColor.Black;
                                    isColor = !isColor;
                                    Console.Write(((GamingCompanies)Buldings[cellNumber[j]]).ValueOfCollaterel.ToString()[k]);
                                }
                            }
                            else Console.Write($"{((GamingCompanies)Buldings[cellNumber[j]]).ValueOfCollaterel}");
                            for (int k = 0; k < count - ((GamingCompanies)Buldings[cellNumber[j]]).ValueOfCollaterel.ToString().Length; k++)
                            {
                                if (((GamingCompanies)Buldings[cellNumber[j]]).Mortgaged == true)
                                {
                                    if (isColor) Console.BackgroundColor = consoleColorType;
                                    else Console.BackgroundColor = ConsoleColor.Black;
                                    isColor = !isColor;
                                }
                                Console.Write(" ");
                            }
                        }
                        else
                        {
                            if (j == 10)
                            {
                                if (Buldings[cellNumber[j]].GetType() == typeof(Prison))
                                {
                                    List<User> users1 = new List<User>();
                                    Console.BackgroundColor = ConsoleColor.DarkYellow;
                                    Console.ForegroundColor = ConsoleColor.Black;
                                    Console.Write($"{Buldings[cellNumber[j]].Title}");
                                    Console.ForegroundColor = ConsoleColor.Gray;
                                    Console.BackgroundColor = ConsoleColor.DarkYellow;
                                    for (int k = 0; k < users.Count; k++)
                                    {
                                        if (users[k].Prison == true)
                                        {
                                            users1.Add(users[k]);
                                        }
                                    }
                                    Console.Write(" ");

                                    if (users1.Count != 0)
                                    {
                                        for (int k = 0; k < users1.Count; k++)
                                        {
                                            Console.ForegroundColor = ConsoleColor.Black;
                                            //Console.Write($"{Buldings[cellNumber[j]].Symbol[k]}");
                                            Console.Write($"{users1[k].Symbol}");
                                            if (k != users1.Count - 1)
                                            {
                                                Console.Write(",");
                                            }
                                            else
                                            {
                                                Console.Write(" ");
                                            }
                                            Console.ForegroundColor = ConsoleColor.Gray;
                                        }
                                        for (int k = 0; k < count - users1.Count - Buldings[cellNumber[j]].Title.Length - 1 - users1.Count; k++)
                                        {
                                            Console.Write(" ");
                                        }
                                    }
                                    else
                                    {
                                        for (int k = 0; k < count - Buldings[cellNumber[j]].Title.Length - 1; k++)
                                        {
                                            Console.Write(" ");
                                        }
                                    }
                                }
                                else
                                {
                                    List<User> users1 = new List<User>();
                                    Console.BackgroundColor = ConsoleColor.DarkYellow;
                                    Console.ForegroundColor = ConsoleColor.Black;
                                    Console.Write($"{Buldings[cellNumber[j]].Title}");
                                    Console.ForegroundColor = ConsoleColor.Gray;
                                    Console.BackgroundColor = ConsoleColor.DarkYellow;
                                    for (int k = 0; k < users.Count; k++)
                                    {
                                        if (users[k].Jackpot == true)
                                        {
                                            users1.Add(users[k]);
                                        }
                                    }
                                    Console.Write(" ");

                                    for (int k = 0; k < users1.Count; k++)
                                    {
                                        Console.ForegroundColor = ConsoleColor.Black;
                                        //Console.Write($"{Buldings[cellNumber[j]].Symbol[k]}");
                                        Console.Write($"{users1[k].Symbol}");
                                        if (k != users1.Count - 1)
                                        {
                                            Console.Write(",");
                                        }
                                        else
                                        {
                                            Console.Write(" ");
                                        }
                                        Console.ForegroundColor = ConsoleColor.Gray;
                                    }

                                    for (int k = 0; k < count - users1.Count - Buldings[cellNumber[j]].Title.Length - 1 - users1.Count; k++)
                                    {
                                        Console.Write(" ");
                                    }
                                }
                            }
                            else
                            {
                                Console.BackgroundColor = ConsoleColor.DarkYellow;
                                Console.ForegroundColor = ConsoleColor.Black;
                                Console.Write($"{Buldings[cellNumber[j]].Title}");
                                Console.ForegroundColor = ConsoleColor.Gray;
                                for (int k = 0; k < count - Buldings[cellNumber[j]].Title.Length; k++)
                                {
                                    Console.Write(" ");
                                }
                            }
                        }
                    }//Цена выкупа
                    if (i == 4)
                    {
                        if (Buldings[cellNumber[j]].GetType() == typeof(Business))
                        {
                            bool isColor = true;
                            if (checkByCells == true)
                            {
                                Console.BackgroundColor = consoleColorType;
                            }
                            if (((Business)Buldings[cellNumber[j]]).Mortgaged == true)
                            {
                                for (int k = 0; k < ((Business)Buldings[cellNumber[j]]).Level.ToString().Length; k++)
                                {
                                    if (isColor) Console.BackgroundColor = consoleColorType;
                                    else Console.BackgroundColor = ConsoleColor.Black;
                                    isColor = !isColor;
                                    Console.Write($"{((Business)Buldings[cellNumber[j]]).Level.ToString()[k]}|{((Business)Buldings[cellNumber[j]]).BusinessDowntrun}");
                                }
                                for (int k = 0; k < count - ((Business)Buldings[cellNumber[j]]).Level.ToString().Length - ((Business)Buldings[cellNumber[j]]).BusinessDowntrun.ToString().Length - 1; k++)
                                {
                                    if (((Business)Buldings[cellNumber[j]]).Mortgaged == true)
                                    {
                                        if (isColor) Console.BackgroundColor = consoleColorType;
                                        else Console.BackgroundColor = ConsoleColor.Black;
                                        isColor = !isColor;
                                    }
                                    Console.Write(" ");
                                }
                            }
                            else
                            {
                                Console.Write($"{((Business)Buldings[cellNumber[j]]).Level}");
                                for (int k = 0; k < count - ((Business)Buldings[cellNumber[j]]).Level.ToString().Length; k++)
                                {
                                    if (((Business)Buldings[cellNumber[j]]).Mortgaged == true)
                                    {
                                        if (isColor) Console.BackgroundColor = consoleColorType;
                                        else Console.BackgroundColor = ConsoleColor.Black;
                                        isColor = !isColor;
                                    }
                                    Console.Write(" ");
                                }
                            }
                        }
                        else if (Buldings[cellNumber[j]].GetType() == typeof(CarInterior))
                        {
                            bool isColor = true;
                            if (checkByCells == true)
                            {
                                Console.BackgroundColor = consoleColorType;
                            }
                            if (((CarInterior)Buldings[cellNumber[j]]).Mortgaged == true)
                            {
                                for (int k = 0; k < ((CarInterior)Buldings[cellNumber[j]]).Level.ToString().Length; k++)
                                {
                                    if (isColor) Console.BackgroundColor = consoleColorType;
                                    else Console.BackgroundColor = ConsoleColor.Black;
                                    isColor = !isColor;
                                    Console.Write($"{((CarInterior)Buldings[cellNumber[j]]).Level.ToString()[k]}|{((CarInterior)Buldings[cellNumber[j]]).BusinessDowntrun}");
                                }
                                for (int k = 0; k < count - ((CarInterior)Buldings[cellNumber[j]]).Level.ToString().Length - ((CarInterior)Buldings[cellNumber[j]]).BusinessDowntrun.ToString().Length - 1; k++)
                                {
                                    if (((CarInterior)Buldings[cellNumber[j]]).Mortgaged == true)
                                    {
                                        if (isColor) Console.BackgroundColor = consoleColorType;
                                        else Console.BackgroundColor = ConsoleColor.Black;
                                        isColor = !isColor;
                                    }
                                    Console.Write(" ");
                                }
                            }
                            else
                            {

                                Console.Write($"{((CarInterior)Buldings[cellNumber[j]]).Level}");
                                for (int k = 0; k < count - ((CarInterior)Buldings[cellNumber[j]]).Level.ToString().Length; k++)
                                {
                                    if (((CarInterior)Buldings[cellNumber[j]]).Mortgaged == true)
                                    {
                                        if (isColor) Console.BackgroundColor = consoleColorType;
                                        else Console.BackgroundColor = ConsoleColor.Black;
                                        isColor = !isColor;
                                    }
                                    Console.Write(" ");
                                }
                            }
                        }
                        else if (Buldings[cellNumber[j]].GetType() == typeof(GamingCompanies))
                        {
                            bool isColor = true;
                            if (checkByCells == true)
                            {
                                Console.BackgroundColor = consoleColorType;
                            }
                            if (((GamingCompanies)Buldings[cellNumber[j]]).Mortgaged == true)
                            {
                                for (int k = 0; k < ((GamingCompanies)Buldings[cellNumber[j]]).Level.ToString().Length; k++)
                                {
                                    if (isColor) Console.BackgroundColor = consoleColorType;
                                    else Console.BackgroundColor = ConsoleColor.Black;
                                    isColor = !isColor;
                                    Console.Write($"{((GamingCompanies)Buldings[cellNumber[j]]).Level.ToString()[k]}|{((GamingCompanies)Buldings[cellNumber[j]]).BusinessDowntrun}");
                                }
                                for (int k = 0; k < count - ((GamingCompanies)Buldings[cellNumber[j]]).Level.ToString().Length - ((GamingCompanies)Buldings[cellNumber[j]]).BusinessDowntrun.ToString().Length - 1; k++)
                                {
                                    if (((GamingCompanies)Buldings[cellNumber[j]]).Mortgaged == true)
                                    {
                                        if (isColor) Console.BackgroundColor = consoleColorType;
                                        else Console.BackgroundColor = ConsoleColor.Black;
                                        isColor = !isColor;
                                    }
                                    Console.Write(" ");
                                }
                            }
                            else
                            {
                                Console.Write($"{((GamingCompanies)Buldings[cellNumber[j]]).Level}");
                                for (int k = 0; k < count - ((GamingCompanies)Buldings[cellNumber[j]]).Level.ToString().Length; k++)
                                {
                                    if (((GamingCompanies)Buldings[cellNumber[j]]).Mortgaged == true)
                                    {
                                        if (isColor) Console.BackgroundColor = consoleColorType;
                                        else Console.BackgroundColor = ConsoleColor.Black;
                                        isColor = !isColor;
                                    }
                                    Console.Write(" ");
                                }
                            }
                        }
                        else
                        {
                            Console.BackgroundColor = ConsoleColor.DarkYellow;
                            for (int k = 0; k < count; k++)
                            {
                                Console.Write(" ");
                            }
                        }

                    }//Уровень ячейки
                    if (i == 5)
                    {
                        if (Buldings[cellNumber[j]].GetType() == typeof(Business))
                        {
                            bool isColor = false;
                            if (checkByCells == true)
                            {
                                Console.BackgroundColor = consoleColorType;
                            }
                            if (((Business)Buldings[cellNumber[j]]).Mortgaged == true)
                            {
                                for (int k = 0; k < ((Business)Buldings[cellNumber[j]]).Rent[((Business)Buldings[cellNumber[j]]).Level].ToString().Length; k++)
                                {
                                    if (isColor) Console.BackgroundColor = consoleColorType;
                                    else Console.BackgroundColor = ConsoleColor.Black;
                                    isColor = !isColor;
                                    Console.Write(((Business)Buldings[cellNumber[j]]).Rent[((Business)Buldings[cellNumber[j]]).Level].ToString()[k]);
                                }
                            }
                            else Console.Write($"{((Business)Buldings[cellNumber[j]]).Rent[((Business)Buldings[cellNumber[j]]).Level]}");//fix

                            for (int k = 0; k < count - ((Business)Buldings[cellNumber[j]]).Rent[((Business)Buldings[cellNumber[j]]).Level].ToString().Length; k++)
                            {
                                if (((Business)Buldings[cellNumber[j]]).Mortgaged == true)
                                {
                                    if (isColor) Console.BackgroundColor = consoleColorType;
                                    else Console.BackgroundColor = ConsoleColor.Black;
                                    isColor = !isColor;
                                }
                                Console.Write(" ");
                            }
                        }
                        else if (Buldings[cellNumber[j]].GetType() == typeof(CarInterior))
                        {
                            bool isColor = false;
                            if (checkByCells == true)
                            {
                                Console.BackgroundColor = consoleColorType;
                            }
                            if (((CarInterior)Buldings[cellNumber[j]]).Mortgaged == true)
                            {
                                for (int k = 0; k < ((CarInterior)Buldings[cellNumber[j]]).Rent[((CarInterior)Buldings[cellNumber[j]]).Level].ToString().Length; k++)
                                {
                                    if (isColor) Console.BackgroundColor = consoleColorType;
                                    else Console.BackgroundColor = ConsoleColor.Black;
                                    isColor = !isColor;
                                    Console.Write(((CarInterior)Buldings[cellNumber[j]]).Rent[((CarInterior)Buldings[cellNumber[j]]).Level].ToString()[k]);
                                }
                            }
                            else Console.Write($"{((CarInterior)Buldings[cellNumber[j]]).Rent[((CarInterior)Buldings[cellNumber[j]]).Level]}");
                            for (int k = 0; k < count - ((CarInterior)Buldings[cellNumber[j]]).Rent[((CarInterior)Buldings[cellNumber[j]]).Level].ToString().Length; k++)
                            {
                                if (((CarInterior)Buldings[cellNumber[j]]).Mortgaged == true)
                                {
                                    if (isColor) Console.BackgroundColor = consoleColorType;
                                    else Console.BackgroundColor = ConsoleColor.Black;
                                    isColor = !isColor;
                                }
                                Console.Write(" ");
                            }
                        }
                        else if (Buldings[cellNumber[j]].GetType() == typeof(GamingCompanies))
                        {
                            bool isColor = false;
                            if (checkByCells == true)
                            {
                                Console.BackgroundColor = consoleColorType;
                            }
                            if (((GamingCompanies)Buldings[cellNumber[j]]).Mortgaged == true)
                            {
                                for (int k = 0; k < ((GamingCompanies)Buldings[cellNumber[j]]).Rent[((GamingCompanies)Buldings[cellNumber[j]]).Level].ToString().Length; k++)
                                {
                                    if (isColor) Console.BackgroundColor = consoleColorType;
                                    else Console.BackgroundColor = ConsoleColor.Black;
                                    isColor = !isColor;
                                    Console.Write(((GamingCompanies)Buldings[cellNumber[j]]).Rent[((GamingCompanies)Buldings[cellNumber[j]]).Level].ToString()[k]);
                                }
                            }
                            else Console.Write($"{((GamingCompanies)Buldings[cellNumber[j]]).Rent[((GamingCompanies)Buldings[cellNumber[j]]).Level]}");
                            for (int k = 0; k < count - ((GamingCompanies)Buldings[cellNumber[j]]).Rent[((GamingCompanies)Buldings[cellNumber[j]]).Level].ToString().Length; k++)
                            {
                                if (((GamingCompanies)Buldings[cellNumber[j]]).Mortgaged == true)
                                {
                                    if (isColor) Console.BackgroundColor = consoleColorType;
                                    else Console.BackgroundColor = ConsoleColor.Black;
                                    isColor = !isColor;
                                }
                                Console.Write(" ");
                            }
                        }
                        else
                        {
                            Console.BackgroundColor = ConsoleColor.DarkYellow;
                            for (int k = 0; k < count; k++)
                            {
                                Console.Write(" ");
                            }

                        }
                    }//Рента за поле
                    if (i == 6)
                    {
                        if (Buldings[cellNumber[j]].GetType() == typeof(Business))
                        {
                            bool isColor = true;
                            if (checkByCells == true)
                            {
                                Console.BackgroundColor = consoleColorType;
                            }
                            if (((Business)Buldings[cellNumber[j]]).Mortgaged == true)
                            {
                                for (int k = 0; k < ((Business)Buldings[cellNumber[j]]).UpgradePrice.ToString().Length; k++)
                                {
                                    if (isColor) Console.BackgroundColor = consoleColorType;
                                    else Console.BackgroundColor = ConsoleColor.Black;
                                    isColor = !isColor;
                                    Console.Write(((Business)Buldings[cellNumber[j]]).UpgradePrice.ToString()[k]);
                                }
                            }
                            else Console.Write($"{((Business)Buldings[cellNumber[j]]).UpgradePrice}");

                            for (int k = 0; k < count - ((Business)Buldings[cellNumber[j]]).UpgradePrice.ToString().Length; k++)
                            {
                                if (((Business)Buldings[cellNumber[j]]).Mortgaged == true)
                                {
                                    if (isColor) Console.BackgroundColor = consoleColorType;
                                    else Console.BackgroundColor = ConsoleColor.Black;
                                    isColor = !isColor;
                                }
                                Console.Write(" ");
                            }
                        }
                        else if (Buldings[cellNumber[j]].GetType() != typeof(GamingCompanies) && Buldings[cellNumber[j]].GetType() != typeof(CarInterior))
                        {
                            bool isColor = false;
                            Console.BackgroundColor = ConsoleColor.DarkYellow;
                            for (int k = 0; k < count; k++)
                            {
                                Console.Write(" ");
                            }
                        }
                        else
                        {
                            bool isColor = true;
                            for (int k = 0; k < count; k++)
                            {
                                if (checkByCells == true)
                                {
                                    Console.BackgroundColor = consoleColorType;
                                }
                                if (Buldings[cellNumber[j]].GetType() == typeof(CarInterior))
                                {
                                    if (((CarInterior)Buldings[cellNumber[j]]).Mortgaged == true)
                                    {
                                        if (isColor) Console.BackgroundColor = consoleColorType;
                                        else Console.BackgroundColor = ConsoleColor.Black;
                                        isColor = !isColor;
                                    }
                                }
                                if (Buldings[cellNumber[j]].GetType() == typeof(GamingCompanies))
                                {
                                    if (((GamingCompanies)Buldings[cellNumber[j]]).Mortgaged == true)
                                    {
                                        if (isColor) Console.BackgroundColor = consoleColorType;
                                        else Console.BackgroundColor = ConsoleColor.Black;
                                        isColor = !isColor;
                                    }
                                }
                                Console.Write(" ");
                            }
                        }
                    }//Цена обновления
                    if (i == 7)
                    {
                        if (Buldings[cellNumber[j]].GetType() != typeof(Business) &&
                            Buldings[cellNumber[j]].GetType() != typeof(CarInterior) &&
                            Buldings[cellNumber[j]].GetType() != typeof(GamingCompanies))
                        {
                            int number = 0;
                            Console.BackgroundColor = ConsoleColor.DarkYellow;
                            for (int k = 0; k < Buldings[cellNumber[j]].Symbol.Count; k++)
                            {
                                if (!users[k].Prison)
                                {
                                    Console.ForegroundColor = ConsoleColor.Black;
                                    Console.Write($"{Buldings[cellNumber[j]].Symbol[k]}");
                                    if (k == Buldings[cellNumber[j]].Symbol.Count - 1)
                                    {
                                        Console.Write(" ");
                                    }
                                    else
                                    {
                                        Console.Write(",");
                                    }
                                    Console.ForegroundColor = ConsoleColor.Gray;
                                }
                                else
                                {
                                    number += 2;
                                }
                            }
                            for (int k = 0; k < count - Buldings[cellNumber[j]].Symbol.Count - Buldings[cellNumber[j]].Symbol.Count + number; k++)
                            {
                                Console.Write(" ");
                            }
                        }
                        else
                        {
                            //Console.Write($"{Buldings[cellNumber[j]].Symbol[k]}");
                            bool isColor = false;
                            int number = 0;
                            if (checkByCells == true)
                            {
                                Console.BackgroundColor = consoleColorType;
                            }
                            if (Buldings[cellNumber[j]].GetType() == typeof(Business))
                            {
                                if (((Business)Buldings[cellNumber[j]]).Mortgaged == true)
                                {
                                    for (int k = 0; k < Buldings[cellNumber[j]].Symbol.Count; k++)
                                    {
                                        if (isColor) Console.BackgroundColor = consoleColorType;
                                        else Console.BackgroundColor = ConsoleColor.Black;
                                        isColor = !isColor;
                                        if (!users[k].Prison)
                                        {
                                            Console.ForegroundColor = ConsoleColor.White;
                                            Console.Write($"{Buldings[cellNumber[j]].Symbol[k]}");
                                            if (k == Buldings[cellNumber[j]].Symbol.Count - 1)
                                            {
                                                Console.Write(" ");
                                            }
                                            else
                                            {
                                                Console.Write(",");
                                            }
                                            Console.ForegroundColor = ConsoleColor.Gray;
                                        }
                                        else
                                        {
                                            number += 2;
                                        }
                                    }
                                }
                                else
                                {
                                    for (int k = 0; k < Buldings[cellNumber[j]].Symbol.Count; k++)
                                    {
                                        if (!users[k].Prison)
                                        {
                                            Console.ForegroundColor = ConsoleColor.White;
                                            Console.Write($"{Buldings[cellNumber[j]].Symbol[k]}");
                                            if (k == Buldings[cellNumber[j]].Symbol.Count - 1)
                                            {
                                                Console.Write(" ");
                                            }
                                            else
                                            {
                                                Console.Write(",");
                                            }
                                            Console.ForegroundColor = ConsoleColor.Gray;
                                        }
                                        else
                                        {
                                            number += 2;
                                        }
                                    }
                                }
                            }
                            if (Buldings[cellNumber[j]].GetType() == typeof(CarInterior))
                            {
                                if (((CarInterior)Buldings[cellNumber[j]]).Mortgaged == true)
                                {
                                    for (int k = 0; k < Buldings[cellNumber[j]].Symbol.Count; k++)
                                    {
                                        if (isColor) Console.BackgroundColor = consoleColorType;
                                        else Console.BackgroundColor = ConsoleColor.Black;
                                        isColor = !isColor;
                                        if (!users[k].Prison)
                                        {
                                            Console.ForegroundColor = ConsoleColor.White;
                                            Console.Write($"{Buldings[cellNumber[j]].Symbol[k]}");
                                            if (k == Buldings[cellNumber[j]].Symbol.Count - 1)
                                            {
                                                Console.Write(" ");
                                            }
                                            else
                                            {
                                                Console.Write(",");
                                            }
                                            Console.ForegroundColor = ConsoleColor.Gray;
                                        }
                                        else
                                        {
                                            number += 2;
                                        }
                                    }
                                }
                                else
                                {
                                    for (int k = 0; k < Buldings[cellNumber[j]].Symbol.Count; k++)
                                    {
                                        if (!users[k].Prison)
                                        {
                                            Console.ForegroundColor = ConsoleColor.White;
                                            Console.Write($"{Buldings[cellNumber[j]].Symbol[k]}");
                                            if (k == Buldings[cellNumber[j]].Symbol.Count - 1)
                                            {
                                                Console.Write(" ");
                                            }
                                            else
                                            {
                                                Console.Write(",");
                                            }
                                            Console.ForegroundColor = ConsoleColor.Gray;
                                        }
                                        else
                                        {
                                            number += 2;
                                        }
                                    }
                                }
                            }
                            if (Buldings[cellNumber[j]].GetType() == typeof(GamingCompanies))
                            {
                                if (((GamingCompanies)Buldings[cellNumber[j]]).Mortgaged == true)
                                {
                                    for (int k = 0; k < Buldings[cellNumber[j]].Symbol.Count; k++)
                                    {
                                        if (isColor) Console.BackgroundColor = consoleColorType;
                                        else Console.BackgroundColor = ConsoleColor.Black;
                                        isColor = !isColor;
                                        if (!users[k].Prison)
                                        {
                                            Console.ForegroundColor = ConsoleColor.White;
                                            Console.Write($"{Buldings[cellNumber[j]].Symbol[k]}");
                                            if (k == Buldings[cellNumber[j]].Symbol.Count - 1)
                                            {
                                                Console.Write(",");
                                            }
                                            else
                                            {
                                                Console.Write(" ");
                                            }
                                            Console.ForegroundColor = ConsoleColor.Gray;
                                        }
                                        else
                                        {
                                            number += 2;
                                        }
                                    }
                                }
                                else
                                {
                                    for (int k = 0; k < Buldings[cellNumber[j]].Symbol.Count; k++)
                                    {
                                        if (!users[k].Prison)
                                        {
                                            Console.ForegroundColor = ConsoleColor.White;
                                            Console.Write($"{Buldings[cellNumber[j]].Symbol[k]}");
                                            if (k == Buldings[cellNumber[j]].Symbol.Count - 1)
                                            {
                                                Console.Write(" ");
                                            }
                                            else
                                            {
                                                Console.Write(",");
                                            }
                                            Console.ForegroundColor = ConsoleColor.Gray;
                                        }
                                        else
                                        {
                                            number += 2;
                                        }
                                    }
                                }
                            }
                            for (int k = 0; k < count - Buldings[cellNumber[j]].Symbol.Count - Buldings[cellNumber[j]].Symbol.Count + number; k++)
                            {
                                if (Buldings[cellNumber[j]].GetType() == typeof(Business))
                                {
                                    if (((Business)Buldings[cellNumber[j]]).Mortgaged == true)
                                    {
                                        if (isColor) Console.BackgroundColor = consoleColorType;
                                        else Console.BackgroundColor = ConsoleColor.Black;
                                        isColor = !isColor;
                                    }
                                }
                                if (Buldings[cellNumber[j]].GetType() == typeof(CarInterior))
                                {
                                    if (((CarInterior)Buldings[cellNumber[j]]).Mortgaged == true)
                                    {
                                        if (isColor) Console.BackgroundColor = consoleColorType;
                                        else Console.BackgroundColor = ConsoleColor.Black;
                                        isColor = !isColor;
                                    }
                                }
                                if (Buldings[cellNumber[j]].GetType() == typeof(GamingCompanies))
                                {
                                    if (((GamingCompanies)Buldings[cellNumber[j]]).Mortgaged == true)
                                    {
                                        if (isColor) Console.BackgroundColor = consoleColorType;
                                        else Console.BackgroundColor = ConsoleColor.Black;
                                        isColor = !isColor;
                                    }
                                }
                                Console.Write(" ");
                            }
                        }
                    }//кто находится на поле
                    if (i > 7)
                    {
                        for (int k = 0; k < count; k++)
                        {
                            if (checkByCells == true)
                            {
                                Console.BackgroundColor = consoleColorType;
                            }
                            Console.Write(" ");
                        }
                    }
                    checkByCells = false;
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.Write("│ ");
                    count = 10;
                    next++;
                }
            }
            count = 10;
            Console.WriteLine();
            for (int i = 0; i < 11; i++)
            {
                if (i == 10)
                {
                    count = 10;
                }
                Console.Write("└");
                for (int j = 0; j < count; j++)
                {
                    Console.Write("──");
                }
                Console.Write("┘ ");
                count = 5;
            }
        }//Вывод верхних и нижних полей .. доделать 
        public void CentralFieldOutput(int[] cellNumber, string text, List<User> users)//доделать логику для текста в центре игрового поля 
        {
            int count = 10;
            int countSpaceCentr = 118;
            int countSpaceInBox = 20;
            Console.WriteLine();
            int nextCellTitile = 0;
            int nextCellPrice = 0;
            int nextCellRenta = 0;
            int lastCell = 0;
            ConsoleColor consoleColorType = ConsoleColor.Black;
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    Console.Write("┌");
                    for (int k = 0; k < 10; k++)
                    {
                        Console.Write("──");
                    }
                    Console.Write("┐");
                    if (j == 0)
                    {
                        if (i == 0)
                        {
                            Console.Write("┌");
                            for (int k = 0; k < 58; k++)
                            {
                                Console.Write("──");
                            }
                            Console.Write("┐");
                        }
                        else
                        {
                            Console.Write("│");
                            for (int k = 0; k < countSpaceCentr - 2; k++)
                            {
                                Console.Write(" ");
                            }
                            Console.Write("│");
                        }
                    }
                }//Верхнии линии рисовки ячейки
                Console.WriteLine();
                count = 4;
                for (int j = 0; j < count; j++)
                {
                    for (int k = 0; k < 2; k++)
                    {
                        if (nextCellTitile >= cellNumber.Length) nextCellTitile -= 1;
                        Console.Write("│");
                        if (j == 0)
                        {
                            consoleColorType = FieldPainting(Buldings[cellNumber[nextCellTitile]], users);
                            if (Buldings[cellNumber[nextCellTitile]].GetType() == typeof(Business))
                            {
                                bool isColor = true;
                                Console.BackgroundColor = consoleColorType;
                                if (((Business)Buldings[cellNumber[nextCellTitile]]).Mortgaged == true)
                                {
                                    for (int o = 0; o < Buldings[cellNumber[nextCellTitile]].Title.Length; o++)
                                    {
                                        if (isColor) Console.BackgroundColor = consoleColorType;
                                        else Console.BackgroundColor = ConsoleColor.Black;
                                        isColor = !isColor;
                                        Console.Write(Buldings[cellNumber[nextCellTitile]].Title[o]);
                                    }
                                }
                                else Console.Write($"{Buldings[cellNumber[nextCellTitile]].Title}");

                                for (int o = 0; o < countSpaceInBox - Buldings[cellNumber[nextCellTitile]].Title.Length; o++)
                                {
                                    if (((Business)Buldings[cellNumber[nextCellTitile]]).Mortgaged == true)
                                    {
                                        if (isColor) Console.BackgroundColor = consoleColorType;
                                        else Console.BackgroundColor = ConsoleColor.Black;
                                        isColor = !isColor;
                                    }
                                    Console.Write(" ");
                                }
                            }
                            else if (Buldings[cellNumber[nextCellTitile]].GetType() == typeof(CarInterior))
                            {
                                bool isColor = true;
                                Console.BackgroundColor = consoleColorType;
                                if (((CarInterior)Buldings[cellNumber[nextCellTitile]]).Mortgaged == true)
                                {
                                    for (int o = 0; o < Buldings[cellNumber[nextCellTitile]].Title.Length; o++)
                                    {
                                        if (isColor) Console.BackgroundColor = consoleColorType;
                                        else Console.BackgroundColor = ConsoleColor.Black;
                                        isColor = !isColor;
                                        Console.Write(Buldings[cellNumber[nextCellTitile]].Title[o]);
                                    }
                                }
                                else Console.Write($"{Buldings[cellNumber[nextCellTitile]].Title}");
                                for (int o = 0; o < countSpaceInBox - Buldings[cellNumber[nextCellTitile]].Title.Length; o++)
                                {
                                    if (((CarInterior)Buldings[cellNumber[nextCellTitile]]).Mortgaged == true)
                                    {
                                        if (isColor) Console.BackgroundColor = consoleColorType;
                                        else Console.BackgroundColor = ConsoleColor.Black;
                                        isColor = !isColor;
                                    }
                                    Console.Write(" ");
                                }
                            }
                            else if (Buldings[cellNumber[nextCellTitile]].GetType() == typeof(GamingCompanies))
                            {
                                bool isColor = true;
                                Console.BackgroundColor = consoleColorType;
                                if (((GamingCompanies)Buldings[cellNumber[nextCellTitile]]).Mortgaged == true)
                                {
                                    for (int o = 0; o < Buldings[cellNumber[nextCellTitile]].Title.Length; o++)
                                    {
                                        if (isColor) Console.BackgroundColor = consoleColorType;
                                        else Console.BackgroundColor = ConsoleColor.Black;
                                        isColor = !isColor;
                                        Console.Write(Buldings[cellNumber[nextCellTitile]].Title[o]);
                                    }
                                }
                                else Console.Write($"{Buldings[cellNumber[nextCellTitile]].Title}");

                                for (int o = 0; o < countSpaceInBox - Buldings[cellNumber[nextCellTitile]].Title.Length; o++)
                                {
                                    if (((GamingCompanies)Buldings[cellNumber[nextCellTitile]]).Mortgaged == true)
                                    {
                                        if (isColor) Console.BackgroundColor = consoleColorType;
                                        else Console.BackgroundColor = ConsoleColor.Black;
                                        isColor = !isColor;
                                    }
                                    Console.Write(" ");
                                }
                            }
                            else
                            {
                                Console.BackgroundColor = ConsoleColor.DarkYellow;
                                Console.ForegroundColor = ConsoleColor.Black;
                                Console.Write($"{Buldings[cellNumber[nextCellTitile]].Title}");
                                Console.ForegroundColor = ConsoleColor.Gray;
                                for (int o = 0; o < countSpaceInBox - Buldings[cellNumber[nextCellTitile]].Title.Length; o++)
                                {
                                    Console.Write(" ");
                                }
                            }
                            nextCellTitile++;
                        }//Название ячейки //возможно переделать иф
                        if (j == 1)//Цена покупки поля 
                        {
                            consoleColorType = FieldPainting(Buldings[cellNumber[nextCellPrice]], users);
                            if (Buldings[cellNumber[nextCellPrice]].GetType() == typeof(Business))
                            {
                                bool isColor = false;
                                Console.BackgroundColor = consoleColorType;
                                var tempBusiness = ((Business)Buldings[cellNumber[nextCellPrice]]);
                                string fullText = tempBusiness.Price + "/" + tempBusiness.RansomValue + "/" + tempBusiness.ValueOfCollaterel + "/" + tempBusiness.Level;
                                if (((Business)Buldings[cellNumber[nextCellPrice]]).Mortgaged == true)
                                {

                                    for (int o = 0; o < fullText.Length; o++)
                                    {
                                        if (isColor) Console.BackgroundColor = consoleColorType;
                                        else Console.BackgroundColor = ConsoleColor.Black;
                                        isColor = !isColor;
                                        Console.Write(fullText[o]);
                                    }

                                }
                                else Console.Write($"{((Business)Buldings[cellNumber[nextCellPrice]]).Price}/" +
                                    $"{((Business)Buldings[cellNumber[nextCellPrice]]).RansomValue}/" +
                                    $"{((Business)Buldings[cellNumber[nextCellPrice]]).ValueOfCollaterel}/" +
                                    $"{((Business)Buldings[cellNumber[nextCellPrice]]).Level}");

                                for (int o = 0; o < countSpaceInBox - ((Business)Buldings[cellNumber[nextCellPrice]]).Price.ToString().Length -
                                    ((Business)Buldings[cellNumber[nextCellPrice]]).RansomValue.ToString().Length -
                                    ((Business)Buldings[cellNumber[nextCellPrice]]).ValueOfCollaterel.ToString().Length -
                                    ((Business)Buldings[cellNumber[nextCellPrice]]).Level.ToString().Length - 3; o++)
                                {
                                    if (((Business)Buldings[cellNumber[nextCellPrice]]).Mortgaged == true)
                                    {
                                        if (isColor) Console.BackgroundColor = consoleColorType;
                                        else Console.BackgroundColor = ConsoleColor.Black;
                                        isColor = !isColor;
                                    }
                                    Console.Write(" ");
                                }
                            }
                            else if (Buldings[cellNumber[nextCellPrice]].GetType() == typeof(CarInterior))
                            {
                                bool isColor = false;
                                Console.BackgroundColor = consoleColorType;
                                var tempBusiness = ((CarInterior)Buldings[cellNumber[nextCellPrice]]);
                                string fullText = tempBusiness.Price + "/" + tempBusiness.RansomValue + "/" + tempBusiness.ValueOfCollaterel + "/" + tempBusiness.Level;
                                if (((CarInterior)Buldings[cellNumber[nextCellPrice]]).Mortgaged == true)
                                {
                                    for (int o = 0; o < fullText.Length; o++)
                                    {
                                        if (isColor) Console.BackgroundColor = consoleColorType;
                                        else Console.BackgroundColor = ConsoleColor.Black;
                                        isColor = !isColor;
                                        Console.Write(fullText[o]);
                                    }
                                }
                                else Console.Write($"{((CarInterior)Buldings[cellNumber[nextCellPrice]]).Price}/" +
                                    $"{((CarInterior)Buldings[cellNumber[nextCellPrice]]).RansomValue}/" +
                                    $"{((CarInterior)Buldings[cellNumber[nextCellPrice]]).ValueOfCollaterel}/" +
                                    $"{((CarInterior)Buldings[cellNumber[nextCellPrice]]).Level}");

                                for (int o = 0; o < countSpaceInBox - ((CarInterior)Buldings[cellNumber[nextCellPrice]]).Price.ToString().Length -
                                    ((CarInterior)Buldings[cellNumber[nextCellPrice]]).RansomValue.ToString().Length -
                                    ((CarInterior)Buldings[cellNumber[nextCellPrice]]).ValueOfCollaterel.ToString().Length -
                                    ((CarInterior)Buldings[cellNumber[nextCellPrice]]).Level.ToString().Length - 3; o++)
                                {
                                    if (((CarInterior)Buldings[cellNumber[nextCellPrice]]).Mortgaged == true)
                                    {
                                        if (isColor) Console.BackgroundColor = consoleColorType;
                                        else Console.BackgroundColor = ConsoleColor.Black;
                                        isColor = !isColor;
                                    }
                                    Console.Write(" ");
                                }
                            }
                            else if (Buldings[cellNumber[nextCellPrice]].GetType() == typeof(GamingCompanies))
                            {
                                bool isColor = false;
                                Console.BackgroundColor = consoleColorType;
                                var tempBusiness = ((GamingCompanies)Buldings[cellNumber[nextCellPrice]]);
                                string fullText = tempBusiness.Price + "/" + tempBusiness.RansomValue + "/" + tempBusiness.ValueOfCollaterel + "/" + tempBusiness.Level;
                                if (((GamingCompanies)Buldings[cellNumber[nextCellPrice]]).Mortgaged == true)
                                {
                                    for (int o = 0; o < fullText.Length; o++)
                                    {
                                        if (isColor) Console.BackgroundColor = consoleColorType;
                                        else Console.BackgroundColor = ConsoleColor.Black;
                                        isColor = !isColor;
                                        Console.Write(fullText[o]);
                                    }
                                }
                                else Console.Write($"{((GamingCompanies)Buldings[cellNumber[nextCellPrice]]).Price}/" +
                                    $"{((GamingCompanies)Buldings[cellNumber[nextCellPrice]]).RansomValue}/" +
                                    $"{((GamingCompanies)Buldings[cellNumber[nextCellPrice]]).ValueOfCollaterel}/" +
                                    $"{((GamingCompanies)Buldings[cellNumber[nextCellPrice]]).Level}");


                                for (int o = 0; o < countSpaceInBox - ((GamingCompanies)Buldings[cellNumber[nextCellPrice]]).Price.ToString().Length -
                                    ((GamingCompanies)Buldings[cellNumber[nextCellPrice]]).RansomValue.ToString().Length -
                                    ((GamingCompanies)Buldings[cellNumber[nextCellPrice]]).ValueOfCollaterel.ToString().Length -
                                    ((GamingCompanies)Buldings[cellNumber[nextCellPrice]]).Level.ToString().Length - 3; o++)
                                {
                                    if (((GamingCompanies)Buldings[cellNumber[nextCellPrice]]).Mortgaged == true)
                                    {
                                        if (isColor) Console.BackgroundColor = consoleColorType;
                                        else Console.BackgroundColor = ConsoleColor.Black;
                                        isColor = !isColor;
                                    }
                                    Console.Write(" ");
                                }
                            }
                            else
                            {
                                Console.BackgroundColor = ConsoleColor.DarkYellow;
                                for (int o = 0; o < countSpaceInBox; o++)
                                {
                                    Console.Write(" ");
                                }
                            }
                            nextCellPrice++;
                        }//Цена поля//стоимость залога//стоимость выкупа//уровень ячейки
                        if (j == 2)
                        {
                            consoleColorType = FieldPainting(Buldings[cellNumber[nextCellRenta]], users);
                            if (Buldings[cellNumber[nextCellRenta]].GetType() == typeof(Business))
                            {
                                bool isColor = true;
                                Console.BackgroundColor = consoleColorType;
                                var tempBusiness = ((Business)Buldings[cellNumber[nextCellRenta]]);
                                string fullText = tempBusiness.Rent[tempBusiness.Level] + "/" + tempBusiness.UpgradePrice;//fix краш лвл 6
                                if (((Business)Buldings[cellNumber[nextCellRenta]]).Mortgaged == true)
                                {
                                    for (int o = 0; o < fullText.Length; o++)
                                    {
                                        if (isColor) Console.BackgroundColor = consoleColorType;
                                        else Console.BackgroundColor = ConsoleColor.Black;
                                        isColor = !isColor;
                                        Console.Write(fullText[o]);
                                    }
                                }
                                else Console.Write($"{((Business)Buldings[cellNumber[nextCellRenta]]).Rent[((Business)Buldings[cellNumber[nextCellRenta]]).Level]}/" +
                                $"{((Business)Buldings[cellNumber[nextCellRenta]]).UpgradePrice}");

                                for (int o = 0; o < countSpaceInBox - ((Business)Buldings[cellNumber[nextCellRenta]]).Rent[((Business)Buldings[cellNumber[nextCellRenta]]).Level].ToString().Length -
                                    ((Business)Buldings[cellNumber[nextCellRenta]]).UpgradePrice.ToString().Length - 1; o++)
                                {
                                    if (((Business)Buldings[cellNumber[nextCellRenta]]).Mortgaged == true)
                                    {
                                        if (isColor) Console.BackgroundColor = consoleColorType;
                                        else Console.BackgroundColor = ConsoleColor.Black;
                                        isColor = !isColor;
                                    }
                                    Console.Write(" ");
                                }
                            }
                            else if (Buldings[cellNumber[nextCellRenta]].GetType() == typeof(CarInterior))
                            {
                                bool isColor = true;
                                Console.BackgroundColor = consoleColorType;
                                if (((CarInterior)Buldings[cellNumber[nextCellRenta]]).Mortgaged == true)
                                {
                                    for (int o = 0; o < ((CarInterior)Buldings[cellNumber[nextCellRenta]]).Rent[((CarInterior)Buldings[cellNumber[nextCellRenta]]).Level].ToString().Length; o++)
                                    {
                                        if (isColor) Console.BackgroundColor = consoleColorType;
                                        else Console.BackgroundColor = ConsoleColor.Black;
                                        isColor = !isColor;
                                        Console.Write(((CarInterior)Buldings[cellNumber[nextCellRenta]]).Rent[((CarInterior)Buldings[cellNumber[nextCellRenta]]).Level].ToString()[o]);
                                    }
                                }
                                else Console.Write($"{((CarInterior)Buldings[cellNumber[nextCellRenta]]).Rent[((CarInterior)Buldings[cellNumber[nextCellRenta]]).Level]}");
                                for (int o = 0; o < countSpaceInBox - ((CarInterior)Buldings[cellNumber[nextCellRenta]]).Rent[((CarInterior)Buldings[cellNumber[nextCellRenta]]).Level].ToString().Length; o++)
                                {
                                    if (((CarInterior)Buldings[cellNumber[nextCellRenta]]).Mortgaged == true)
                                    {
                                        if (isColor) Console.BackgroundColor = consoleColorType;
                                        else Console.BackgroundColor = ConsoleColor.Black;
                                        isColor = !isColor;
                                    }
                                    Console.Write(" ");
                                }
                            }
                            else if (Buldings[cellNumber[nextCellRenta]].GetType() == typeof(GamingCompanies))
                            {
                                bool isColor = true;
                                Console.BackgroundColor = consoleColorType;
                                if (((GamingCompanies)Buldings[cellNumber[nextCellRenta]]).Mortgaged == true)
                                {
                                    for (int o = 0; o < ((GamingCompanies)Buldings[cellNumber[nextCellRenta]]).Rent[((GamingCompanies)Buldings[cellNumber[nextCellRenta]]).Level].ToString().Length; o++)
                                    {
                                        if (isColor) Console.BackgroundColor = consoleColorType;
                                        else Console.BackgroundColor = ConsoleColor.Black;
                                        isColor = !isColor;
                                        Console.Write(((GamingCompanies)Buldings[cellNumber[nextCellRenta]]).Rent[((GamingCompanies)Buldings[cellNumber[nextCellRenta]]).Level].ToString()[o]);
                                    }
                                }
                                else Console.Write($"{((GamingCompanies)Buldings[cellNumber[nextCellRenta]]).Rent[((GamingCompanies)Buldings[cellNumber[nextCellRenta]]).Level]}");

                                for (int o = 0; o < countSpaceInBox - ((GamingCompanies)Buldings[cellNumber[nextCellRenta]]).Rent[((GamingCompanies)Buldings[cellNumber[nextCellRenta]]).Level].ToString().Length; o++)
                                {
                                    if (((GamingCompanies)Buldings[cellNumber[nextCellRenta]]).Mortgaged == true)
                                    {
                                        if (isColor) Console.BackgroundColor = consoleColorType;
                                        else Console.BackgroundColor = ConsoleColor.Black;
                                        isColor = !isColor;
                                    }
                                    Console.Write(" ");
                                }
                            }
                            else
                            {
                                Console.BackgroundColor = ConsoleColor.DarkYellow;
                                for (int o = 0; o < countSpaceInBox; o++)
                                {
                                    Console.Write(" ");
                                }
                            }
                            nextCellRenta++;
                        }//рента поля
                        if (j != 0 && j != 1 && j != 2)
                        {
                            if (Buldings[cellNumber[lastCell]].GetType() != typeof(GamingCompanies) &&
                                Buldings[cellNumber[lastCell]].GetType() != typeof(Business) &&
                                Buldings[cellNumber[lastCell]].GetType() != typeof(CarInterior))
                            {
                                Console.BackgroundColor = ConsoleColor.DarkYellow;
                                Console.ForegroundColor = ConsoleColor.Black;
                                for (int o = 0; o < Buldings[cellNumber[lastCell]].Symbol.Count; o++)
                                {
                                    Console.Write($"{Buldings[cellNumber[lastCell]].Symbol[o]}");
                                }
                                Console.ForegroundColor = ConsoleColor.Gray;
                                for (int o = 0; o < countSpaceInBox - Buldings[cellNumber[lastCell]].Symbol.Count; o++)
                                {
                                    Console.Write(" ");
                                }
                            }
                            else
                            {
                                bool isColor = false;
                                consoleColorType = FieldPainting(Buldings[cellNumber[lastCell]], users);
                                Console.BackgroundColor = consoleColorType;
                                if (Buldings[cellNumber[lastCell]].GetType() == typeof(Business))
                                {
                                    if (((Business)Buldings[cellNumber[lastCell]]).Mortgaged == true)
                                    {
                                        for (int o = 0; o < Buldings[cellNumber[lastCell]].Symbol.Count; o++)
                                        {
                                            if (isColor) Console.BackgroundColor = consoleColorType;
                                            else Console.BackgroundColor = ConsoleColor.Black;
                                            isColor = !isColor;
                                            Console.Write($"{Buldings[cellNumber[lastCell]].Symbol[o]}");
                                        }
                                    }
                                    else
                                    {
                                        for (int o = 0; o < Buldings[cellNumber[lastCell]].Symbol.Count; o++)
                                        {
                                            Console.Write($"{Buldings[cellNumber[lastCell]].Symbol[o]}");
                                        }
                                    }
                                }
                                if (Buldings[cellNumber[lastCell]].GetType() == typeof(CarInterior))
                                {
                                    if (((CarInterior)Buldings[cellNumber[lastCell]]).Mortgaged == true)
                                    {
                                        for (int o = 0; o < Buldings[cellNumber[lastCell]].Symbol.Count; o++)
                                        {
                                            if (isColor) Console.BackgroundColor = consoleColorType;
                                            else Console.BackgroundColor = ConsoleColor.Black;
                                            isColor = !isColor;
                                            Console.Write($"{Buldings[cellNumber[lastCell]].Symbol[o]}");
                                        }
                                    }
                                    else
                                    {
                                        for (int o = 0; o < Buldings[cellNumber[lastCell]].Symbol.Count; o++)
                                        {
                                            Console.Write($"{Buldings[cellNumber[lastCell]].Symbol[o]}");
                                        }
                                    }
                                }
                                if (Buldings[cellNumber[lastCell]].GetType() == typeof(GamingCompanies))
                                {
                                    if (((GamingCompanies)Buldings[cellNumber[lastCell]]).Mortgaged == true)
                                    {
                                        for (int o = 0; o < Buldings[cellNumber[lastCell]].Symbol.Count; o++)
                                        {
                                            if (isColor) Console.BackgroundColor = consoleColorType;
                                            else Console.BackgroundColor = ConsoleColor.Black;
                                            isColor = !isColor;
                                            Console.Write($"{Buldings[cellNumber[lastCell]].Symbol[o]}");
                                        }
                                    }
                                    else
                                    {
                                        for (int o = 0; o < Buldings[cellNumber[j]].Symbol.Count; o++)
                                        {
                                            Console.Write($"{Buldings[cellNumber[lastCell]].Symbol[o]}");
                                        }
                                    }
                                }
                                for (int o = 0; o < countSpaceInBox - Buldings[cellNumber[lastCell]].Symbol.Count; o++)
                                {
                                    if (Buldings[cellNumber[lastCell]].GetType() == typeof(Business))
                                    {
                                        if (((Business)Buldings[cellNumber[lastCell]]).Mortgaged == true)
                                        {
                                            if (isColor) Console.BackgroundColor = consoleColorType;
                                            else Console.BackgroundColor = ConsoleColor.Black;
                                            isColor = !isColor;
                                        }
                                    }
                                    if (Buldings[cellNumber[lastCell]].GetType() == typeof(CarInterior))
                                    {
                                        if (((CarInterior)Buldings[cellNumber[lastCell]]).Mortgaged == true)
                                        {
                                            if (isColor) Console.BackgroundColor = consoleColorType;
                                            else Console.BackgroundColor = ConsoleColor.Black;
                                            isColor = !isColor;
                                        }
                                    }
                                    if (Buldings[cellNumber[lastCell]].GetType() == typeof(GamingCompanies))
                                    {
                                        if (((GamingCompanies)Buldings[cellNumber[lastCell]]).Mortgaged == true)
                                        {
                                            if (isColor) Console.BackgroundColor = consoleColorType;
                                            else Console.BackgroundColor = ConsoleColor.Black;
                                            isColor = !isColor;
                                        }
                                    }
                                    Console.Write(" ");
                                }
                            }
                            lastCell++;
                        }
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.Write("│");
                        if (k == 0)
                        {
                            Console.Write("│");
                            for (int o = 0; o < countSpaceCentr - 2/*- text.Length*/; o++)
                            {
                                Console.Write(" ");
                            }
                            Console.Write("│");
                        }
                    }
                    if (i == 0)
                    {
                        if (j == 0)
                        {
                            Console.Write("{1} Название ");

                        }
                        if (j == 1)
                        {
                            Console.Write("{2} Цена покупки поля ");
                        }
                        if (j == 2)
                        {
                            Console.Write("{3} Цена выкупа поля");
                        }
                        if (j == 3)
                        {
                            Console.Write("{4} Цена залога поля ");
                        }
                    }
                    if (i == 1)
                    {
                        if (j == 0)
                        {
                            Console.Write("{5} Уровень Бизнеса ");
                        }
                        if (j == 1)
                        {
                            Console.Write("{6} Цена ренты поля");
                        }
                        if (j == 2)
                        {
                            Console.Write("{7} Стоимость покупки филиала");
                        }
                        if (j == 3)
                        {
                            Console.Write("{8} Игрок который находится на поле");
                        }
                    }
                    Console.WriteLine();
                }//Центральные линии рисовки ячейки
                for (int j = 0; j < 2; j++)
                {
                    Console.Write("└");
                    for (int k = 0; k < 10; k++)
                    {
                        Console.Write("──");
                    }
                    Console.Write("┘");
                    if (j == 0)
                    {
                        if (i == 8)
                        {
                            Console.Write("└");
                            for (int k = 0; k < 58; k++)
                            {
                                Console.Write("──");
                            }
                            Console.Write("┘");
                        }
                        else
                        {
                            Console.Write("│");
                            for (int k = 0; k < countSpaceCentr - 2; k++)
                            {
                                Console.Write(" ");
                            }
                            Console.Write("│");
                        }
                    }
                }//Нижнии линии рисовкия
                Console.WriteLine();
            }
        }//Вывод боковых полей
        public void ShowPlayersIsBloock(List<User> users)
        {
            string skelet;
            int count = 12;
            int countSpace = 24;
            for (int i = 0; i < users.Count; i++)
            {
                Console.Write("┌");
                for (int j = 0; j < count; j++)
                {
                    Console.Write("──");
                }
                Console.Write("┐                   ");
            }
            count = 9;
            for (int i = 0; i < count; i++)
            {
                Console.WriteLine();
                for (int j = 0; j < users.Count; j++)
                {
                    if (users[j].Surrender == true)
                    {
                        Console.Write("│");
                        Console.BackgroundColor = users[j].Color;
                        if (i == 0)
                        {
                            skelet = ",-'\"\"`-.";
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Write($"        {skelet}");
                            Console.ForegroundColor = ConsoleColor.Gray;
                            for (int k = 0; k < countSpace - skelet.Length - 8; k++)
                            {
                                Console.Write(" ");
                            }

                        }
                        if (i == 1)
                        {
                            skelet = ";        :";
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Write($"       {skelet}");
                            Console.ForegroundColor = ConsoleColor.Gray;
                            for (int k = 0; k < countSpace - skelet.Length - 7; k++)
                            {
                                Console.Write(" ");
                            }
                        }
                        if (i == 2)
                        {
                            skelet = ":  L o s   :";
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Write($"      {skelet}");
                            Console.ForegroundColor = ConsoleColor.Gray;
                            for (int k = 0; k < countSpace - skelet.Length - 6; k++)
                            {
                                Console.Write(" ");
                            }
                        }
                        if (i == 3)
                        {
                            skelet = ":  _    _  ;";
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Write($"      {skelet}");
                            Console.ForegroundColor = ConsoleColor.Gray;
                            for (int k = 0; k < countSpace - skelet.Length - 6; k++)
                            {
                                Console.Write(" ");
                            }
                        }
                        if (i == 4)
                        {
                            skelet = ": ( )  ( ) :";
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Write($"      {skelet}");
                            Console.ForegroundColor = ConsoleColor.Gray;
                            for (int k = 0; k < countSpace - skelet.Length - 6; k++)
                            {
                                Console.Write(" ");
                            }
                        }
                        if (i == 5)
                        {
                            skelet = "::   '`   :;";
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Write($"      {skelet}");
                            Console.ForegroundColor = ConsoleColor.Gray;
                            for (int k = 0; k < countSpace - skelet.Length - 6; k++)
                            {
                                Console.Write(" ");
                            }
                        }
                        if (i == 6)
                        {
                            skelet = "!:      :!";
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Write($"       {skelet}");
                            Console.ForegroundColor = ConsoleColor.Gray;
                            for (int k = 0; k < countSpace - skelet.Length - 7; k++)
                            {
                                Console.Write(" ");
                            }
                        }
                        if (i == 7)
                        {
                            skelet = "`:`++++';'";
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Write($"       {skelet}");
                            Console.ForegroundColor = ConsoleColor.Gray;
                            for (int k = 0; k < countSpace - skelet.Length - 7; k++)
                            {
                                Console.Write(" ");
                            }
                        }
                        if (i == 8)
                        {
                            skelet = "`....'";
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Write($"         {skelet}");
                            Console.ForegroundColor = ConsoleColor.Gray;
                            for (int k = 0; k < countSpace - skelet.Length - 9; k++)
                            {
                                Console.Write(" ");
                            }
                        }
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.Write("│                   ");
                    }
                    else
                    {
                        //Console.WriteLine("           ");
                        Console.Write("│");
                        Console.BackgroundColor = users[j].Color;
                        if (i == 3)
                        {
                            int number = (countSpace - users[j].Name.Length) / 2;
                            for (int k = 0; k < number; k++)
                            {
                                Console.Write(" ");
                            }

                            Console.Write($"{users[j].Name}");
                            number = countSpace - (number + users[j].Name.Length);
                            for (int k = 0; k < number/*countSpace - users[j].Name.Length - 11*/; k++)
                            {
                                Console.Write(" ");
                            }
                        }
                        if (i == 5)
                        {
                            Console.Write($"         ${users[j].Balance}");
                            for (int k = 0; k < countSpace - users[j].Balance.ToString().Length - 10; k++)
                            {
                                Console.Write(" ");
                            }
                        }
                        if (i == 7)
                        {
                            Console.Write($"          S{users[j].Symbol}");
                            for (int k = 0; k < countSpace - users[j].Symbol.ToString().Length - 11; k++)
                            {
                                Console.Write(" ");
                            }
                        }
                        if (i != 3 && i != 5 && i != 7)
                        {
                            for (int k = 0; k < countSpace; k++)
                            {
                                Console.Write(" ");
                            }
                        }
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.Write("│                   ");
                    }
                }
            }
            countSpace = 12;
            Console.WriteLine();
            for (int j = 0; j < users.Count; j++)
            {
                Console.Write("└");
                for (int k = 0; k < countSpace; k++)
                {
                    Console.Write("──");
                }
                Console.Write("┘                   ");
            }

        }//Вывод полей игроков
        public void GiveColorPlayer(List<User> users)
        {
            int[] numberColor = { 4, 1, 2, 13, };
            int next = 0;
            for (int i = 0; i < users.Count; i++)
            {
                users[i].Color = (ConsoleColor)numberColor[next];
                next++;
            }
        }//Назначения цвета игрокам
        public void ShowAllField(List<User> users, string text)//Вывод цельного поля
        {
            GiveColorPlayer(users);
            int[] cellNumberUp = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            TopLineOutput(cellNumberUp, users);
            int[] cellNumberTop = { 39, 11, 38, 12, 37, 13, 36, 14, 35, 15, 34, 16, 33, 17, 32, 18, 31, 19 };
            CentralFieldOutput(cellNumberTop, text, users);
            int[] cellNumberDown = { 30, 29, 28, 27, 26, 25, 24, 23, 22, 21, 20 };
            TopLineOutput(cellNumberDown, users);
            Console.WriteLine();
            ShowPlayersIsBloock(users);
            Console.WriteLine();
        }
    }
}
