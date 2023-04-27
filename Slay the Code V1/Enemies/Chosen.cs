namespace STV
{
    public class Chosen : Enemy
    {
        public Chosen()
        {
            Name = "Chosen";
            MaxHP = EnemyRNG.Next(95, 100);
            Hp = MaxHP;
            Block = 0;
            Buffs = new();
            Actions = new();
            Relics = new();
        }

        public override void EnemyAction(Hero hero, List<Enemy> encounter)
        {
            if (Intent == "Poke")
                for (int i = 0; i < 2; i++)
                    Attack(hero, 5, encounter);
            else if (Intent == "Debilitate")
            {
                Attack(hero, 10, encounter);
                hero.AddBuff(1, 2);
            }
            else if (Intent == "Drain")
            {
                AddBuff(4, 3);
                hero.AddBuff(2, 3);
            }
            else if (Intent == "Zap")
                Attack(hero, 18, encounter);
            else hero.AddBuff(105, 1);
        }

        public override void SetEnemyIntent(int turnNumber, List<Enemy> encounter)
        {
            if (turnNumber == 0)
                Intent = "Poke";
            else if (turnNumber == 1)
                Intent = "Hex";
            else if (turnNumber % 2 == 0)
            {
                Intent = EnemyRNG.Next(0, 20) switch
                {
                    int i when i >= 0 && i <= 9 => "Debilitate",
                    _ => "Drain",
                };
            }
            else
            {
                Intent = EnemyRNG.Next(0, 20) switch
                {
                    int i when i >= 0 && i <= 11 => "Poke",
                    _ => "Zap",
                };
            }
        }
    }
}