namespace STV
{
    public class GremlinNob : Enemy
    {
        public GremlinNob()
        {
            Name = "Gremlin Nob";
            MaxHP = EnemyRNG.Next(82, 87);
            Hp = MaxHP;
            Block = 0;
            Buffs = new();
            Actions = new();
            Relics = new();
        }

        public override void EnemyAction(Hero hero, List<Enemy> encounter)
        {
            if (Intent == "Skull Bash")
            {
                Attack(hero, 6, encounter);
                hero.AddBuff(1, 2);
            }
            else if (Intent == "Rush")
                Attack(hero, 14, encounter);
            else AddBuff(19, 2);
        }

        public override void SetEnemyIntent(int turnNumber, List<Enemy> encounter)
        {
            if (turnNumber == 0)
                Intent = "Bellow";
            Intent = EnemyRNG.Next(0, 21) switch
            {
                int i when i >= 0 && i <= 6 => "Skull Bash",
                _ => "Rush",
            };
        }
    }
}
