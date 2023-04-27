namespace STV
{
    public class Transient : Enemy
    {
        int X {  get; set; }
        public Transient()
        {
            Name = "Transient";
            MaxHP = 999;
            Hp = MaxHP;
            Block = 0;
            Buffs = new();
            AddBuff(111, 6);
            AddBuff(112, 1);
            Actions = new();
            Relics = new();
        }

        public override void EnemyAction(Hero hero, List<Enemy> encounter)
        {
            Attack(hero, 30+(X*10), encounter);
        }

        public override void SetEnemyIntent(int turnNumber, List<Enemy> encounter)
        {
            if (turnNumber == 0)
            {

            }
        }
    }
}