namespace STV
{
    public class Collector : Enemy
    {
        public Collector()
        {
            Name = "The Collector";
            MaxHP = 282;
            Hp = MaxHP;
            Block = 0;
            Buffs = new();
            Actions = new();
            Relics = new();
        }

        public override void EnemyAction(Hero hero, List<Enemy> encounter)
        {
            if (Intent == "Mega Debuff")
            {
                hero.AddBuff(2, 5);
                hero.AddBuff(1, 5);
                hero.AddBuff(6, 5);
            }
            else if (Intent == "Buff")
            {
                foreach (Enemy e in encounter)
                    e.AddBuff(4, 3);
                GainBlock(15);
            }
            else if (Intent == "Spawn")
                for (int i = 0 + encounter.Count; i < 3; i++)
                    encounter.Add(new TorchHead());
            else Attack(hero, 18, encounter);
        }

        public override void SetEnemyIntent(int turnNumber, List<Enemy> encounter)
        {
            if (turnNumber == 0)
                Intent = "Spawn";
            else if (turnNumber == 3)
                Intent = "Mega Debuff";
            else if (encounter.Count > 2)
            {
                Intent = EnemyRNG.Next(20) switch
                {
                    int i when i >= 0 && i <= 4 => "Spawn",
                    int i when i >= 5 && i <= 10 => "Buff",
                    _ => "Fireball",
                };
            }
            else Intent = EnemyRNG.Next(20) switch
            {
                int i when i >= 0 && i <= 13 => "Fireball",
                _ => "Buff",
            };
        }
    }
}
