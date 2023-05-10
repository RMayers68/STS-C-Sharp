namespace STV
{
    public class Donu : Enemy
    {
        public Donu()
        {
            Name = "Donu";
            MaxHP = 250;
            Hp = MaxHP;
            Block = 0;
            Buffs = new();
            AddBuff(8, 2);
            Actions = new();
            Relics = new();
        }

        public override void EnemyAction(Hero hero, List<Enemy> encounter)
        {
            if (Intent == "Circle of Power")
                foreach (Enemy e in encounter)
                    e.AddBuff(4, 3);
            else for (int i = 0; i < 2; i++)
                    Attack(hero, 10, encounter);
        }

        public override void SetEnemyIntent(int turnNumber, List<Enemy> encounter)
        {
            if (turnNumber % 2 == 0)
                Intent = "Circle of Power";
            else Intent = "Beam";
        }
    }
}
