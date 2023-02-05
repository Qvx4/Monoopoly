using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonopolyV20
{
    public abstract class User
    {
        public string Name { get; set; }
        public char Symbol { get; set; }
        public int Balance { get; set; }
        public bool StepSkip { get; set; }
        public bool Prison { get; set; }
        public bool ReverseStroke { get; set; }
        public ConsoleColor Color { get; set; }
        public int CordinationPlayer { get; set; }
        public bool Surrender { get; set; }
        public bool Jackpot { get; set; }
        public User() { }
        public User(string name,char symbol,int balance,bool stepSkip,bool prison)
        {
            Name = name;
            Symbol = symbol;
            Balance = balance;
            StepSkip = stepSkip;
            Prison = prison;
            CordinationPlayer = 0;
        }
        public User(User user)
        {
            Name=user.Name;
            Symbol = user.Symbol;
            Balance = user.Balance;
            StepSkip = user.StepSkip;
            Prison = user.Prison;
            CordinationPlayer = user.CordinationPlayer;
            Surrender = user.Surrender;
            Jackpot = user.Jackpot;
        }
        public abstract void Show();
    }
}
