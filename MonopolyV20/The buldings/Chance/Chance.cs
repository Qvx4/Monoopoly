using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonopolyV20
{
    public class Chance : Building
    {
        public List<Chances> Chances { get; set; }
        public Chance(string title, int number) : base(title, number)
        {
            Chances = new List<Chances>();
            AddChance();
        }
        private void AddChance()
        {
            Chances.Add(new Lesion(750, "{ Поход в магазин }", "Вы решили пойти в магазин и потратили 750"));
            Chances.Add(new RandomActions("{ День рождения }", "У вас сегодня день рождение и вы получаете с каждого игрока по 150", Actions.Birthday));
            Chances.Add(new RandomActions("{ Тюрьма }","Вы не заплатили налоги и по этому вы отправляетесь в тюрьму", Actions.GoToJail));
            Chances.Add(new Lesion(1000, "{ Обучение }", "Конец учебного года закончился и вам надо оплатить дальнешее обучение в универе"));
            Chances.Add(new Profit(500, "{ Компенсация }", "Вы ехали на велосипеди по дороге и вас подбила машина в результате вы получаете компенсацию в размере 500"));
            Chances.Add(new RandomActions("{ Пропуск хода }", "Вы упали и отправились в больницу из-за этого вы пропускаете ход", Actions.Skipping));
            Chances.Add(new Lesion(350, "{ ДТП }", "Вы попали в маленькое дтп и должны запалатить 350"));
            Chances.Add(new Profit(250, "{ Бонус }", "Вы шли домой и нашли 70 "));
            Chances.Add(new RandomActions("{ Ход назад }", "Cлудующий ход вы ходите в обратном направлении ", Actions.WalkBackWards));
            Chances.Add(new Lesion(450, "{ Страховка }", "Вы должны оплатить страховку в размере 450"));
            Chances.Add(new Profit(1500, "{ Ставка }", "Вы поставили ставку на игру и выиграли 1500"));
            Chances.Add(new RandomActions("{ Телепорт }", "Вас телепорт на рандомную ячейку вперёд", Actions.Teleport));
            Chances.Add(new Lesion(550, "{ Оплата комунальных услуг }", "Вам надо оплатить комунальные услуги в размере 550"));
            Chances.Add(new Profit(700, "{ Приятный бонус }", "Вы просто так получаете 700"));
            Chances.Add(new RandomActions("{ Пустой шанс }", "В этом шансе ничего не происходит вам повезло", Actions.Empty));

        }
    }
}
