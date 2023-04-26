namespace STV
{
    public class GremlinNob : Enemy
    {
        public GremlinNob()
        {
            this.Name = "Gremlin Nob";
            this.TopHP = 87;
            this.BottomHP = 82;
            this.Intents = new() { "Bellow", "Rush", "Skull Bash" };
        }

        public GremlinNob(Enemy e)
        {
            this.Name = e.Name;
            this.MaxHP = EnemyRNG.Next(e.BottomHP, e.TopHP);
            this.Hp = this.MaxHP;
            this.Block = 0;
            this.Intents = e.Intents;
            this.Buffs = new();
            this.Actions = new();
            this.Relics = new();
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
