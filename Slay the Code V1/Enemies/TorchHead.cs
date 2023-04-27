namespace STV
{
    public class TorchHead : Enemy
    {
        public TorchHead()
        {
            Name = "Torch Head";
            MaxHP = EnemyRNG.Next(38,41);
            Hp = MaxHP;
            Block = 0;
            Buffs = new();
            Actions = new();
            Relics = new();
        }

        public override void EnemyAction(Hero hero, List<Enemy> encounter)
        {
            Attack(hero, 7, encounter);
        }

        public override void SetEnemyIntent(int turnNumber, List<Enemy> encounter)
        {
            Intent = "Tackle";
        }
    }
}