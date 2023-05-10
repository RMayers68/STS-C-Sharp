namespace STV
{
    public class Taskmaster : Enemy
    {
        public Taskmaster()
        {
            Name = "Taskmaster";
            MaxHP = EnemyRNG.Next(54, 61);
            Hp = MaxHP;
            Block = 0;
            Buffs = new();
            Actions = new();
            Relics = new();
        }
        public override void EnemyAction(Hero hero, List<Enemy> encounter)
        {
            Attack(hero, 7, encounter);
            //hero.DiscardPile.Add(new(Dict.cardL[357]));
        }

        public override void SetEnemyIntent(int turnNumber, List<Enemy> encounter)
        {
            Intent = "Scouring Whip";
        }
    }
}