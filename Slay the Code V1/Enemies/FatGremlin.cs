namespace STV
{
    public class FatGremlin : Enemy
    {
        public FatGremlin()
        {
            Name = "Fat Gremlin";
            TopHP = 18;
            BottomHP = 13;
            Intents = new() { "Smash" };
            MaxHP = EnemyRNG.Next(BottomHP, TopHP);
            Hp = MaxHP;
            Block = 0;
            Buffs = new();
            Actions = new();
            Relics = new();
        }

        public override void EnemyAction(Hero hero, List<Enemy> encounter)
        {
            Attack(hero, 4, encounter);
            hero.AddBuff(2, 1);
        }

        public override void SetEnemyIntent(int turnNumber, List<Enemy> encounter)
        {
            Intent = Intents.First();
        }
    }
}
