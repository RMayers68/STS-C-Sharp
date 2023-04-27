namespace STV
{
    public class SneakyGremlin : Enemy
    {
        public SneakyGremlin()
        {
            Name = "Sneaky Gremlin";
            TopHP = 15;
            BottomHP = 10;
            Intents = new() { "Puncture" };
            MaxHP = EnemyRNG.Next(BottomHP, TopHP);
            Hp = MaxHP;
            Block = 0;
            Buffs = new();
            Actions = new();
            Relics = new();
        }
        public override void EnemyAction(Hero hero, List<Enemy> encounter)
        { Attack(hero, 9, encounter); }
        public override void SetEnemyIntent(int turnNumber, List<Enemy> encounter)
        { Intent = Intents.First(); }
    }
}