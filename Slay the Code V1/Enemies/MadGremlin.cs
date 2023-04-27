namespace STV
{
    public class MadGremlin : Enemy
    {
        public MadGremlin(bool minion = false)
        {
            Name = "Mad Gremlin";
            MaxHP = EnemyRNG.Next(20, 25);
            Hp = MaxHP;
            Block = 0;
            Buffs = new();
            AddBuff(103, 1);
            if (minion)
                AddBuff(118, 1);
            Actions = new();
            Relics = new();
        }

        public override void EnemyAction(Hero hero, List<Enemy> encounter)
        {
            Attack(hero, 4, encounter);
        }

        public override void SetEnemyIntent(int turnNumber, List<Enemy> encounter)
        {
            Intent = "Scratch";
        }
    }
}