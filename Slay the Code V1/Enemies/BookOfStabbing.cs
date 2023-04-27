namespace STV
{
    public class BookOfStabbing : Enemy
    {
        int MultiStabs { get; set; }

        public BookOfStabbing()
        {
            Name = "Book of Stabbing";
            MaxHP = EnemyRNG.Next(160, 163);
            Hp = MaxHP;
            Block = 0;
            Buffs = new();
            AddBuff(107, 1);
            MultiStabs = 2;
            Actions = new();
            Relics = new();
        }

        public override void EnemyAction(Hero hero, List<Enemy> encounter)
        {
            if (Intent == "Stab Stab Stab")
            {
                for (int i = 0; i < MultiStabs; i++)
                    Attack(hero, 6, encounter);
                MultiStabs++;
            }
            else Attack(hero, 21, encounter);
        }

        public override void SetEnemyIntent(int turnNumber, List<Enemy> encounter)
        {
            Intent = EnemyRNG.Next(0, 20) switch
            {
                int i when i >= 0 && i <= 16 => "Stab Stab Stab",
                _ => "Stab",
            };
        }
    }
}

