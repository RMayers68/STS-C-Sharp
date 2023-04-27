namespace STV
{
    public class SmallSpikeSlime : Enemy
    {
        public SmallSpikeSlime()
        {
            Name = "Spike Slime (S)";
            TopHP = 15;
            BottomHP = 10;
            Intents = new() { "Tackle" };
            MaxHP = EnemyRNG.Next(BottomHP, TopHP);
            Hp = MaxHP;
            Block = 0;
            Buffs = new();
            Actions = new();
            Relics = new();
        }

        public override void EnemyAction(Hero hero, List<Enemy> encounter)
        {
            Attack(hero, 5, encounter);
        }

        public override void SetEnemyIntent(int turnNumber, List<Enemy> encounter)
        {
            Intent = "Tackle";
        }
    }
}