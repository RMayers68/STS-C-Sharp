namespace STV
{
    public class Bear : Enemy
    {
        public Bear()
        {
            Name = "Bear";
            MaxHP = EnemyRNG.Next(38, 43);
            Hp = MaxHP;
            Block = 0;
            Buffs = new();
            Actions = new();
            Relics = new();
        }

        public override void EnemyAction(Hero hero, List<Enemy> encounter)
        {
            if (Intent == "Lunge")
            {
                Attack(hero, 9, encounter);
                GainBlock(9);
            }
            else if (Intent == "Maul")
                Attack(hero, 18, encounter);
            else hero.AddBuff(9, -2);
        }

        public override void SetEnemyIntent(int turnNumber, List<Enemy> encounter)
        {
            if (turnNumber == 0)
                Intent = "Bear Hug";
            else if (turnNumber % 2 == 1)
                Intent = "Lunge";
            else Intent = "Maul";
        }
    }
}