namespace STV
{
    public class SneakyGremlin : Enemy
    {
        public SneakyGremlin()
        {
            Name = "Sneaky Gremlin";
            MaxHP = EnemyRNG.Next(10, 15);
            Hp = MaxHP;
            Block = 0;
            Buffs = new();
            Actions = new();
            Relics = new();
        }
        public override void EnemyAction(Hero hero, List<Enemy> encounter)
        { Attack(hero, 9, encounter); }
        public override void SetEnemyIntent(int turnNumber, List<Enemy> encounter)
        { Intent = "Puncure"; }
    }
}