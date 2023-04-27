namespace STV
{
    public class Pointy : Enemy
    {
        public Pointy()
        {
            Name = "Pointy";
            MaxHP = 30;
            Hp = MaxHP;
            Block = 0;
            Buffs = new();
            Actions = new();
            Relics = new();
        }

        public override void EnemyAction(Hero hero, List<Enemy> encounter)
        {
            for (int i = 0; i < 2; i++)
                Attack(hero, 5, encounter);
        }

        public override void SetEnemyIntent(int turnNumber, List<Enemy> encounter)
        {
            Intent = "Attack";
        }
    }
}
