namespace STV
{
    public class GiantHead : Enemy
    {
        int T { get; set; }
        public GiantHead()
        {
            Name = "Giant Head";
            MaxHP = 500;
            Hp = MaxHP;
            Block = 0;
            Buffs = new();
            AddBuff(114, 1);
            Actions = new();
            Relics = new();
            T = 0;
        }

        public override void EnemyAction(Hero hero, List<Enemy> encounter)
        {
            if (Intent == "Count")
                Attack(hero, 13, encounter);
            else if (Intent == "Glare")
                hero.AddBuff(2, 2);
            else Attack(hero, 25 + T, encounter);
        }

        public override void SetEnemyIntent(int turnNumber, List<Enemy> encounter)
        {
            if (turnNumber < 4)
                Intent = EnemyRNG.Next(0, 2) switch
                {
                    0 => "Count",
                    _ => "Glare",
                };
            else
            {
                Intent = "It Is Time";
                if (T < 7)
                    T++;
            }
        }
    }
}
