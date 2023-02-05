using System;

namespace MonopolyV20
{
    public class Trade
    {
        public Applicationcs PlayrFirst { get; set; }
        public Applicationcs PlayrSecond { get; set; }
        public Trade()
        {
            PlayrFirst = new Applicationcs();
            PlayrSecond = new Applicationcs();
        }
        public void ShowTrade()
        {
            PlayrFirst.Money = 16000;
            PlayrSecond.Money = 16000;
            string[] circle =  {
                    "+++++++++"
                ,"+++         +++"
                ,"++             ++"
                ,"++                 ++"
                ,"++                 ++"
                ,"+                   +"
                ,"++                 ++"
                ,"++                 ++"
                ,"++             ++"
                ,"+++         +++"
                ,  "+++++++++"};
            int numberCirlce = 0;
            for (int i = 0; i < 1; i++)
            {
                Console.Write("┌");
                for (int j = 0; j < 100; j++)
                {
                    Console.Write("─");
                }
                Console.Write("┐ ");
                Console.WriteLine();
            }//верхняя часть 
            for (int i = 0; i < 37; i++)
            {
                Console.Write("│");
                for (int j = 0; j < 2; j++)
                {
                    //вывод круга чей трейд и кому 
                    if (i == 1 || i == 11)
                    {
                        for (int k = 0; k < (50 - circle[numberCirlce].Length) / 2 + 23; k++)
                        {
                            if (j == 0)
                            {
                                if (k == 42)
                                {
                                    Console.Write("│ ");
                                }
                            }
                            if (k == 18)
                            {
                                Console.Write(circle[numberCirlce]);
                            }
                            else if (k != 15 & k != 40)
                            {
                                Console.Write(" ");
                            }
                        }
                    }
                    if (i == 2 || i == 10)
                    {
                        for (int k = 0; k < (50 - circle[numberCirlce].Length) / 2 + 19; k++)
                        {
                            if (j == 0)
                            {
                                if (k == 35)
                                {
                                    Console.Write("│ ");
                                }
                            }
                            if (k == 14)
                            {
                                Console.Write(circle[numberCirlce]);
                            }
                            else if (k != 15 & k != 40)
                            {
                                Console.Write(" ");
                            }
                        }
                    }
                    if (i == 3 || i == 9)
                    {
                        for (int k = 0; k < (50 - circle[numberCirlce].Length) / 2 + 18; k++)
                        {
                            if (j == 0)
                            {
                                if (k == 33)
                                {
                                    Console.Write("│ ");
                                }
                            }
                            if (k == 13)
                            {
                                Console.Write(circle[numberCirlce]);
                            }
                            else if (k != 15 & k != 40)
                            {
                                Console.Write(" ");
                            }
                        }
                    }
                    if (i == 4 || i == 5 || i == 7 || i == 8)
                    {
                        for (int k = 0; k < (50 - circle[numberCirlce].Length) / 2 + 16; k++)
                        {
                            if (j == 0)
                            {
                                if (k == 29)
                                {
                                    Console.Write("│ ");
                                }
                            }
                            if (k == 11)
                            {
                                Console.Write(circle[numberCirlce]);
                            }
                            else if (k != 15 & k != 40)
                            {
                                Console.Write(" ");
                            }
                        }
                    }
                    if (i == 6)
                    {
                        for (int k = 0; k < (50 - circle[numberCirlce].Length) / 2 + 16; k++)
                        {
                            if (j == 0)
                            {
                                if (k == 29)
                                {
                                    Console.Write("│ ");
                                }
                            }
                            if (k == 11)
                            {
                                Console.Write(circle[numberCirlce]);
                            }
                            else if (k != 15 & k != 40)
                            {
                                Console.Write(" ");
                            }
                        }
                    }
                    //вывод круга чей трейд и кому 

                    //вывод ячейки денег
                    if (i == 12)
                    {
                        for (int k = 0; k < 50 - 1; k++)
                        {
                            if (j == 0)
                            {
                                if (k == 48)
                                {
                                    Console.Write("│ ");
                                }
                            }
                            if (k == 10)
                            {
                                Console.Write("┌");
                            }
                            else if (k == 32)
                            {
                                Console.Write("┐");
                            }
                            else if (k > 10 && k < 32)
                            {
                                Console.Write("─");
                            }
                            else
                            {
                                Console.Write(" ");
                            }

                        }
                    }
                    if (i == 13)
                    {
                        string cash = "Money";
                        for (int k = 0; k < 11; k++)
                        {
                            if (k == 10)
                            {
                                Console.Write("│");
                                if (j == 0)
                                {
                                    Console.Write($"{cash}:{PlayrFirst.Money}");
                                    for (int o = 0; o < 21 - PlayrFirst.Money.ToString().Length - cash.Length - 1; o++)
                                    {
                                        Console.Write(" ");
                                    }
                                }
                                else
                                {
                                    Console.Write($"{cash}:{PlayrSecond.Money}");
                                    for (int o = 0; o < 21 - PlayrSecond.Money.ToString().Length - cash.Length - 1; o++)
                                    {
                                        Console.Write(" ");
                                    }
                                }
                                Console.Write("│");
                            }
                            else
                            {
                                Console.Write(" ");
                            }
                        }
                        for (int k = 0; k < 16; k++)
                        {
                            if (j == 0)
                            {
                                if (k == 15)
                                {
                                    Console.Write("│ ");
                                }
                            }
                            Console.Write(" ");
                        }
                    }
                    if (i == 14)
                    {
                        for (int k = 0; k < 50 - 1; k++)
                        {
                            if (j == 0)
                            {
                                if (k == 48)
                                {
                                    Console.Write("│ ");
                                }
                            }
                            if (k == 10)
                            {
                                Console.Write("└");
                            }
                            else if (k == 32)
                            {
                                Console.Write("┘");
                            }
                            else if (k > 10 && k < 32)
                            {
                                Console.Write("─");
                            }
                            else
                            {
                                Console.Write(" ");
                            }

                        }
                    }
                    //вывод ячейки денег
                    //вывод ячейки бизнеса
                    if (i == 15)
                    {
                        for (int k = 0; k < 10; k++)
                        {
                            Console.Write(" ");
                        }
                        Console.Write("│");
                        if (j == 0)
                        {
                            for (int k = 0; k < PlayrFirst.Buldings.Count; k++)
                            {

                            }
                        }
                        else
                        {

                        }
                        for (int k = 0; k < 21; k++)
                        {
                            Console.Write(" ");
                        }
                        Console.Write("│");
                        for (int k = 0; k < 16; k++)
                        {
                            if (j == 0)
                            {
                                if (k == 15)
                                {
                                    Console.Write("│ ");
                                }
                            }
                            Console.Write(" ");
                        }
                    }
                    //вывод ячейки бизнеса 
                    if (j == 0)
                    {
                        if (i > 15 + PlayrFirst.Buldings.Count || i < 1)
                        {
                            for (int k = 0; k < 50; k++)
                            {
                                if (j == 0)
                                {
                                    if (k == 48)
                                    {
                                        Console.Write("│ ");
                                    }
                                }
                                if (k != 48)
                                {
                                    Console.Write(" ");
                                }
                            }//вывод пробелов с полоской посредине
                        }
                    }
                    else
                    {
                        if (i > 15  + PlayrSecond.Buldings.Count || i < 1)
                        {
                            for (int k = 0; k < 50; k++)
                            {
                                if (j == 0)
                                {
                                    if (k == 48)
                                    {
                                        Console.Write("│ ");
                                    }
                                }
                                if (k != 48)
                                {
                                    Console.Write(" ");
                                }
                            }//вывод пробелов с полоской посредине
                        }
                    }
                }
                if (i != 0)
                {
                    numberCirlce++;
                }
                Console.Write("│");
                Console.WriteLine();
            }
            for (int i = 0; i < 1; i++)
            {
                Console.Write("└");
                for (int j = 0; j < 100; j++)
                {
                    Console.Write("─");
                }
                Console.Write("┘ ");
            }//нижняя часть 
        }

    }
}
