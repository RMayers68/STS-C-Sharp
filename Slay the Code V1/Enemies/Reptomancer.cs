namespace STV
{
    public class Reptomancer : Enemy
    {
        public Reptomancer()
        {
            Name = "Reptomancer";
            MaxHP = EnemyRNG.Next(180,191);
            Hp = MaxHP;
            Block = 0;
            Buffs = new();
            Actions = new();
            Relics = new();
        }

        public override void EnemyAction(Hero hero, List<Enemy> encounter)
        {
            if (Intent == "Snake Strike")
            {
                for (int i = 0; i < 2; i++)
                    Attack(hero, 13, encounter);
                hero.AddBuff(2, 1);
            }
            else if (Intent == "Summon")
                encounter.Add(new Dagger());
            else Attack(hero, 30, encounter);
        }

        public override void SetEnemyIntent(int turnNumber, List<Enemy> encounter)
        {
            if (turnNumber == 0)
                Intent = "Summon";
            else if (encounter.Count > 4)
                Intent = EnemyRNG.Next(0, 30) switch
                {
                    int i when i >= 0 && i <= 19 => "Snake Strike",
                    _ => "Big Bite",
                };
            else Intent = EnemyRNG.Next(0, 30) switch
            {
                int i when i >= 0 && i <= 9 => "Snake Strike",
                int i when i >= 10 && i <= 19 => "Summon",
                _ => "Big Bite",
            };
        }
    }
}