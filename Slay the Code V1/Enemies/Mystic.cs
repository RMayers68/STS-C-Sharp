namespace STV
{
    public class Mystic : Enemy
    {
        public Mystic()
        {
            Name = "Mystic";
            TopHP = 57;
            BottomHP = 48;
            Intents = new() { "Heal", "Buff", "Attack/Debuff" };
            MaxHP = EnemyRNG.Next(BottomHP, TopHP);
            Hp = MaxHP;
            Block = 0;
            Buffs = new();
            Actions = new();
            Relics = new();
        }

        public override void EnemyAction(Hero hero, List<Enemy> encounter)
        {
            if (Intent == "Attack/Debuff")
            {
                Attack(hero, 6, encounter);
                hero.AddBuff(6, 2);
            }
            else if (Intent == "Buff")
                foreach (Enemy e in encounter)
                    e.AddBuff(4,2);
            else foreach (Enemy e in encounter)
                    e.HealHP(16);                
        }

        public override void SetEnemyIntent(int turnNumber, List<Enemy> encounter)
        {
            if (encounter.Any(x => x.Hp + 16 <= x.MaxHP))
                Intent = "Heal";
            else
            {
                Intent = EnemyRNG.Next(0, 20) switch
                {
                    int i when i >= 0 && i <= 11 => "Attack/Debuff",
                    _ => "Buff",
                };
            }
        }
    }
}
