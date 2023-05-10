namespace STV
{
    public class BronzeAutomaton : Enemy
    {
        public BronzeAutomaton()
        {
            Name = "Bronze Automaton";
            MaxHP = 300;
            Hp = MaxHP;
            Block = 0;
            Buffs = new();
            AddBuff(8, 3);
            Actions = new();
            Relics = new();
        }

        public override void EnemyAction(Hero hero, List<Enemy> encounter)
        {
            if (Intent == "Spawn Orbs")
                for (int i = 0; i < 2; i++)
                    encounter.Add(new BronzeOrb());
            else if (Intent == "Boost")
            {
                AddBuff(4, 3);
                GainBlock(9);
            }
            else if (Intent == "Flail")
                for (int i = 0; i < 2; i++)
                    Attack(hero, 7, encounter);
            else Attack(hero, 45, encounter);
        }

        public override void SetEnemyIntent(int turnNumber, List<Enemy> encounter)
        {
            if (turnNumber == 0)
                Intent = "Spawn Orbs";
            else
                Intent = (turnNumber % 6) switch
                {
                    1 or 3 => "Flail",
                    5 => "HYPER BEAM",
                    _ => "Boost",
                };
        }
    }
}
