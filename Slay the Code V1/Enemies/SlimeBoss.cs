namespace STV
{
    public class SlimeBoss : Enemy
    {
        public SlimeBoss()
        {
            Name = "Slime Boss";
            MaxHP = 140;
            Hp = MaxHP;
            Block = 0;
            Buffs = new();
            AddBuff(101, 1);
            Actions = new();
            Relics = new();
        }

        public override void EnemyAction(Hero hero, List<Enemy> encounter)
        {
            if (Intent == "Goop Spray")
            {
                for (int i = 0; i < 3; i++)
                    //hero.DiscardPile.Add(new Card(Dict.cardL[358]));
                Console.WriteLine($"{Name} has added 3 Slimed cards into your Deck! Ewww!");
            }
            else if (Intent == "Slam")
                Attack(hero, 35, encounter);
            else if (Intent == "Preparing")
                Console.WriteLine("This enemy is preparing a huge attack!");
            else
            {
                encounter.Remove(this);
                encounter.Add(new LargeSpikeSlime(Hp));
                encounter.Add(new LargeAcidSlime(Hp));
            }
        }

        public override void SetEnemyIntent(int turnNumber, List<Enemy> encounter)
        {
            if (Hp <= MaxHP / 2)
                Intent = "Split";
            else Intent = (Actions.Count % 3) switch
            {
                int i when i == 0 => "Goop Spray",
                int i when i == 1 => "Charging",
                _ => "Slam",
            };
        }
    }
}
