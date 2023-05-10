namespace STV
{
    public class Sentry : Enemy
    {
        bool MiddleSentry { get; set; }
        public Sentry(bool Middle = false)
        {
            Name = "Sentry";
            MaxHP = EnemyRNG.Next(38, 43);
            Hp = MaxHP;
            Block = 0;
            Buffs = new();
            AddBuff(8, 1);
            Actions = new();
            Relics = new();
            MiddleSentry = Middle;
        }

        public override void EnemyAction(Hero hero, List<Enemy> encounter)
        {
            if (Intent == "Bolt")
            {
                for (int i = 0; i < 2; i++)
                    //hero.DiscardPile.Add(new Card(Dict.cardL[356]));
                Console.WriteLine($"{Name} has added 2 Dazed cards to your deck!");
            }
            else Attack(hero, 9, encounter);
        }

        public override void SetEnemyIntent(int turnNumber, List<Enemy> encounter)
        {
            if (turnNumber == 0)
            {
                if (MiddleSentry)
                    Intent = "Beam";
                else Intent = "Bolt";
            }
            else
            {
                if (Intent == "Bolt")
                    Intent = "Beam";
                else Intent = "Bolt";
            }
        }
    }
}
