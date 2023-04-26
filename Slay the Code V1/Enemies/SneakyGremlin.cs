namespace STV
{
    public class SneakyGremlin : Enemy
    {
        public SneakyGremlin()
        {
            this.Name = "Sneaky Gremlin";
            this.TopHP = 15;
            this.BottomHP = 10;
            this.Intents = new() { "Puncture" };
        }

        public SneakyGremlin(Enemy e)
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
            Attack(hero, 9, encounter);
        }

        public override void SetEnemyIntent(int turnNumber, List<Enemy> encounter)
        {
            Intent = Intents.First();
        }
    }
}