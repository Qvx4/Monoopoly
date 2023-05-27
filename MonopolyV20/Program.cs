using System;
using System.Text;

namespace MonopolyV20
{
    internal class Program
    {
        static public void ShowFirstMenu()
        {
            //Console.WriteLine("┌ │ ── ┐ └  ┘");
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;
            Console.WriteLine(
                "  .------------------------------------------------------.\n" +
                " /  .-.                                              .-.  \\");
            Console.Write("|  /   \\");
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.Write("  { 1 } Правила Игры  ");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("                      /   \\  |");
            Console.Write("| |\\_.  |");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write(" { 2 } Создать Бота ");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("                      |    /| |");
            Console.Write("|\\|  | /|");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write(" { 3 } Создать Игрока ");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("                    |\\  | |/|");
            Console.Write("| `---' |");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write(" { 4 } Удалить Игрока / (Бота)");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("            | `---' |");
            Console.Write("|       |");
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.Write(" { 5 } Начать игру ");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("                       |       |");
            Console.WriteLine("" +
                "|       |------------------------------------------|       |\n" +
                "\\       |                                          |       /\n" +
                " \\     /                                            \\     /\n" +
                "  `---'                                              `---'");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write("          Ввод >> ");
        }
        static void Main(string[] args)
        {
            MonopolyGame game = new MonopolyGame();
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;
            while (true)
            {
                Console.Clear();
                ShowFirstMenu();
                FirstMenu firstMenu;
                Enum.TryParse(Console.ReadLine(), out firstMenu);
                while ((int)firstMenu < 1 || (int)firstMenu > 5)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.Write("          Ввод >> ");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Enum.TryParse(Console.ReadLine(), out firstMenu);

                }
                switch (firstMenu)
                {
                    case FirstMenu.RulesGame:
                        {
                            Console.Clear();
                            game.RulesTheGame();
                            Console.ReadLine();
                        }
                        break;
                    case FirstMenu.CreateBot:
                        {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.Write("{ Введите Никнейм Боту } >> ");
                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                            string name = Console.ReadLine();
                            while (game.IsCheckName(name))
                            {
                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                Console.Write("{ Введите Никнейм Боту } >> ");
                                Console.ForegroundColor = ConsoleColor.DarkGreen;
                                name = Console.ReadLine();
                            }
                            Console.ForegroundColor = ConsoleColor.DarkBlue;
                            Console.Write("{ Введите Символ Бота } >> ");
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            char.TryParse(Console.ReadLine(),out char symbol);
                            while (game.IsCheckSymbol(symbol))
                            {
                                Console.ForegroundColor = ConsoleColor.DarkBlue;
                                Console.Write("{ Введите Символ Бота } >> ");
                                Console.ForegroundColor = ConsoleColor.DarkYellow;
                                char.TryParse(Console.ReadLine(), out  symbol);
                            }
                            Console.ForegroundColor = ConsoleColor.Gray;
                            int balance = 15000;
                            Bot bot = new Bot(name, symbol, balance, false, false);
                            game.AddBot(bot);
                        }
                        break;
                    case FirstMenu.CreatePlayer:
                        {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.Write("{ Введите Никнейм Игрока } >> ");
                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                            string name = Console.ReadLine();
                            while (game.IsCheckName(name))
                            {
                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                Console.Write("{ Введите Никнейм Игроку } >> ");
                                Console.ForegroundColor = ConsoleColor.DarkGreen;
                                name = Console.ReadLine();
                            }
                            Console.ForegroundColor = ConsoleColor.DarkBlue;
                            Console.Write("{ Введите Символ Игрока } >> ");
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            char.TryParse(Console.ReadLine(), out char symbol);
                            while (game.IsCheckSymbol(symbol))
                            {
                                Console.ForegroundColor = ConsoleColor.DarkBlue;
                                Console.Write("{ Введите Символ Игрока } >> ");
                                Console.ForegroundColor = ConsoleColor.DarkYellow;
                                char.TryParse(Console.ReadLine(), out  symbol);
                            }
                            Console.ForegroundColor = ConsoleColor.Gray;
                            int balance = 15000;
                            Player player = new Player(name, symbol, balance, false, false,false,false);
                            game.AddPlayer(player);
                        }
                        break;
                    case FirstMenu.DeletBotAndPlayer:
                        {
                            Console.ForegroundColor = ConsoleColor.DarkCyan;
                            Console.WriteLine("{ 1 } Удалить Бота ");
                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                            Console.WriteLine("{ 2 } Удалить Игрока ");
                            Console.ForegroundColor = ConsoleColor.Gray;
                            Console.Write("{ Ввод } >> ");
                            ChoosingWhoDelete choosingWhoDelete;
                            Enum.TryParse(Console.ReadLine(), out choosingWhoDelete);
                            while ((int)choosingWhoDelete < 1 || (int)choosingWhoDelete > 2)
                            {
                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                Console.Write("{ Ввод } >> ");
                                Console.ForegroundColor = ConsoleColor.Gray;
                                Enum.TryParse(Console.ReadLine(), out choosingWhoDelete);
                            }
                            switch (choosingWhoDelete)
                            {
                                case ChoosingWhoDelete.Bot:
                                    {
                                        Console.Clear();
                                        if (game.IsCheckBotNull())
                                        {
                                            Console.WriteLine("\n\n\n\n\n\n\n\n");
                                            Console.WriteLine(
                                                "\t\t\t\t\t+---------------------------+\n" +
                                                "\t\t\t\t\t| = : = : = : = : = : = : = |\n" +
                                                "\t\t\t\t\t|{>/---------------------\\<}|");
                                            Console.Write("\t\t\t\t\t|: |\t\t\t");
                                            Console.WriteLine(" | :|");
                                            Console.Write("\t\t\t\t\t| :|");
                                            Console.ForegroundColor = ConsoleColor.DarkRed;
                                            Console.Write(" Боты ещё не созданы ");
                                            Console.ForegroundColor = ConsoleColor.Gray;
                                            Console.WriteLine("|: |");
                                            Console.Write("\t\t\t\t\t|: |\t\t\t");
                                            Console.WriteLine(" | :|");
                                            Console.WriteLine(
                                                "\t\t\t\t\t|{>\\---------------------/<}|\n" +
                                                "\t\t\t\t\t| = : = : = : = : = : = : = |\n" +
                                                "\t\t\t\t\t+---------------------------+\n");
                                            Console.Write("Нажмите любую кнопку что бы выйти обртано ... ");
                                            Console.ReadLine();
                                            break;
                                        }
                                        game.ShowBot();
                                        Console.Write("{");
                                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                                        Console.Write(" Введите Никнейм Бота которого хотите удалить ");
                                        Console.ForegroundColor = ConsoleColor.Gray;
                                        Console.WriteLine("}");
                                        Console.Write("{");
                                        Console.ForegroundColor = ConsoleColor.DarkRed;
                                        Console.Write(" Ввод ");
                                        Console.ForegroundColor = ConsoleColor.Gray;
                                        Console.Write("}");
                                        Console.ForegroundColor = ConsoleColor.DarkRed;
                                        Console.Write(" >> ");
                                        Console.ForegroundColor = ConsoleColor.DarkMagenta;
                                        string name = Console.ReadLine();
                                        Console.ForegroundColor = ConsoleColor.Gray;
                                        game.DeletUser(name);
                                    }
                                    break;
                                case ChoosingWhoDelete.Player:
                                    {
                                        Console.Clear();
                                        if (game.IsCheckPlayerNull())
                                        {
                                            Console.WriteLine("\n\n\n\n\n\n\n\n");
                                            Console.WriteLine(
                                                "\t\t\t\t\t+-----------------------------+\n" +
                                                "\t\t\t\t\t| = : = : = : = : = : = : = : |\n" +
                                                "\t\t\t\t\t|{>/-----------------------\\<}|");
                                            Console.Write("\t\t\t\t\t|: |\t\t\t");
                                            Console.WriteLine("   | :|");
                                            Console.Write("\t\t\t\t\t| :|");
                                            Console.ForegroundColor = ConsoleColor.DarkRed;
                                            Console.Write(" Игроки ещё не созданы ");
                                            Console.ForegroundColor = ConsoleColor.Gray;
                                            Console.WriteLine("|: |");
                                            Console.Write("\t\t\t\t\t|: |\t\t\t");
                                            Console.WriteLine("   | :|");
                                            Console.WriteLine(
                                                "\t\t\t\t\t|{>\\-----------------------/<}|\n" +
                                                "\t\t\t\t\t| = : = : = : = : = : = : = : |\n" +
                                                "\t\t\t\t\t+-----------------------------+\n");
                                            Console.Write("Нажмите любую кнопку что бы выйти обртано ... ");
                                            Console.ReadLine();
                                            break;
                                        }
                                        game.ShowPlayer();
                                        Console.Write("{");
                                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                                        Console.Write(" Введите Никнейм Игрока которого хотите удалить ");
                                        Console.ForegroundColor = ConsoleColor.Gray;
                                        Console.WriteLine("}");
                                        Console.Write("{");
                                        Console.ForegroundColor = ConsoleColor.DarkRed;
                                        Console.Write(" Ввод ");
                                        Console.ForegroundColor = ConsoleColor.Gray;
                                        Console.Write("}");
                                        Console.ForegroundColor = ConsoleColor.DarkRed;
                                        Console.Write(" >> ");
                                        Console.ForegroundColor = ConsoleColor.DarkMagenta;
                                        string name = Console.ReadLine();
                                        Console.ForegroundColor = ConsoleColor.Gray;
                                        game.DeletUser(name);
                                    }
                                    break;
                            }
                        }
                        break;
                    case FirstMenu.StartGame:
                        {
                            Console.Clear();
                            if (game.Users.Count < 2)
                            {
                                Console.WriteLine("\n\n\n\n\n\n\n\n");
                                Console.WriteLine(
                                    "\t\t\t\t\t+--------------------------------------+\n" +
                                    "\t\t\t\t\t| = : = : = : = : = : = : = : = : = : =|\n" +
                                    "\t\t\t\t\t|{>/--------------------------------\\<}|");
                                Console.Write("\t\t\t\t\t|: |\t\t\t\t");
                                Console.WriteLine("    | :|");
                                Console.Write("\t\t\t\t\t| :|");
                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                Console.Write(" Минималный старт игры 2 игрока ");
                                Console.ForegroundColor = ConsoleColor.Gray;
                                Console.WriteLine("|: |");
                                Console.Write("\t\t\t\t\t|: |\t\t\t\t");
                                Console.WriteLine("    | :|");
                                Console.WriteLine(
                                    "\t\t\t\t\t|{>\\--------------------------------/<}|\n" +
                                    "\t\t\t\t\t| = : = : = : = : = : = : = : = : = : =|\n" +
                                    "\t\t\t\t\t+--------------------------------------+\n");
                                Console.Write("Нажмите любую кнопку что бы выйти обртано ... ");
                                Console.ReadLine();
                                break;
                            }
                            if (game.Users.Count > 4)
                            {
                                Console.WriteLine("\n\n\n\n\n\n\n\n");
                                Console.WriteLine(
                                    "\t\t\t\t\t+--------------------------------------+\n" +
                                    "\t\t\t\t\t| = : = : = : = : = : = : = : = : = : =|\n" +
                                    "\t\t\t\t\t|{>/--------------------------------\\<}|");
                                Console.Write("\t\t\t\t\t|: |\t\t\t\t");
                                Console.WriteLine("    | :|");
                                Console.Write("\t\t\t\t\t| :|");
                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                Console.Write(" Максимальное количество игроков 4 ");
                                Console.ForegroundColor = ConsoleColor.Gray;
                                Console.WriteLine("|: |");
                                Console.Write("\t\t\t\t\t|: |\t\t\t\t");
                                Console.WriteLine("    | :|");
                                Console.WriteLine(
                                    "\t\t\t\t\t|{>\\--------------------------------/<}|\n" +
                                    "\t\t\t\t\t| = : = : = : = : = : = : = : = : = : =|\n" +
                                    "\t\t\t\t\t+--------------------------------------+\n");
                                Console.Write("Нажмите любую кнопку что бы выйти обртано ... ");
                                Console.ReadLine();
                                break;
                            }
                            game.StartGame();
                        }
                        break;
                }



            }
        }
    }
}
