namespace STV
{
    public class RedSlaver : Enemy
    {
        public RedSlaver()
        {
            Name = "Red Slaver";
            MaxHP = EnemyRNG.Next(46, 51);
            Hp = MaxHP;
            Block = 0;
            Buffs = new();
            Actions = new();
            Relics = new();
        }

        public override void EnemyAction(Hero hero, List<Enemy> encounter)
        {
            if (Intent == "Scrape")
            {
                Attack(hero, 8, encounter);
                hero.AddBuff(1, 1);
            }
            else if (Intent == "Stab")
                Attack(hero, 12, encounter);
            else hero.AddBuff(14, 2);
        }

        public override void SetEnemyIntent(int turnNumber, List<Enemy> encounter)
        {
            if (turnNumber == 0)
                Intent = "Stab";
            if (!Actions.Contains("Entangle"))
                Intent = EnemyRNG.Next(0, 20) switch
                {
                    int i when i >= 0 && i <= 4 => "Entangle",
                    _ => "Determine",
                };
            else Intent = EnemyRNG.Next(0, 20) switch
            {
                int i when i >= 0 && i <= 10 => "Scrape",
                _ => "Stab",
            };
            if (Intent == "Determine")
            {
                if (Actions != null && Actions.Count % 3 == 0)
                    Intent = "Stab";
                else Intent = "Scrape";
            }
        }
    }
}