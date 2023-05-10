namespace STV
{
    public class Romeo : Enemy
    {
        public Romeo()
        {
            Name = "Romeo";
            MaxHP = EnemyRNG.Next(35, 40);
            Hp = MaxHP;
            Block = 0;
            Buffs = new();
            Actions = new();
            Relics = new();
        }

        public override void EnemyAction(Hero hero, List<Enemy> encounter)
        {
            if (Intent == "Agonizing Slash")
            {
                Attack(hero, 10, encounter);
                hero.AddBuff(2, 2);
            }
            else if (Intent == "Cross Slash")
                Attack(hero, 15, encounter);
        }

        public override void SetEnemyIntent(int turnNumber, List<Enemy> encounter)
        {
            if (turnNumber == 0)
                Intent = "Mock";
            else if (turnNumber % 2 == 1)
                Intent = "Agonizing Slash";
            else Intent = "Cross Slash";
        }
    }
}