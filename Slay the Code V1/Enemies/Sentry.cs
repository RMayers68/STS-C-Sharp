namespace STV
{
    public class Sentry : Enemy
    {
        public Sentry()
        {
            this.Name = "Sentry";
            this.TopHP = 43;
            this.BottomHP = 38;
            this.Intents = new() { "Beam", "Bolt" };
        }

        public Sentry(Enemy e)
        {
            this.Name = e.Name;
            this.MaxHP = EnemyRNG.Next(e.BottomHP, e.TopHP);
            this.Hp = this.MaxHP;
            this.Block = 0;
            this.Intents = e.Intents;
            this.Buffs = new();
            AddBuff(8, 1);
            this.Actions = new();
            this.Relics = new();
        }

        public override void EnemyAction(Hero hero, List<Enemy> encounter)
        {
            if (Intent == "Bolt")
            {
                for (int i = 0; i < 2; i++)
                    hero.DiscardPile.Add(new Card(Dict.cardL[356]));
                Console.WriteLine($"{Name} has added 2 Dazed cards to your deck!");
            }
            else Attack(hero, 9, encounter);
        }

        public override void SetEnemyIntent(int turnNumber, List<Enemy> encounter)
        {
            if (turnNumber == 0 && encounter.Count == 3)
            {
                encounter[0].Intent = "Bolt";
                encounter[1].Intent = "Beam";
                encounter[2].Intent = "Bolt";
            }
            else if (turnNumber == 1)
                Intent = "Bolt";
            if (Intent == "Bolt")
                Intent = "Beam";
            else Intent = "Bolt";
        }
    }
}
