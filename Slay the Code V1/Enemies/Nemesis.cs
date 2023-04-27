namespace STV
{
    public class Nemesis : Enemy
    {
        public Nemesis()
        {
            Name = "Nemesis";
            MaxHP = 185;
            Hp = MaxHP;
            Block = 0;
            Buffs = new();
            Actions = new();
            Relics = new();
        }

        public override void EnemyAction(Hero hero, List<Enemy> encounter)
        {
            if (Intent == "Debuff")
                for (int i = 0; i < 3; i++)
                    hero.AddToDrawPile(new(Dict.cardL[355]), true);
            else if (Intent == "Attack")
                for (int i = 0; i < 3; i++)
                    Attack(hero, 6, encounter);
            else Attack(hero, 45, encounter);
        }

        public override void SetEnemyIntent(int turnNumber, List<Enemy> encounter)
        {
            if (turnNumber == 0)
                Intent = EnemyRNG.Next(0, 2) switch
                {
                    0 => "Debuff",
                    _ => "Attack",
                };
            else Intent = EnemyRNG.Next(0, 20) switch
            {
                int i when i >= 0 && i <= 6 => "Debuff",
                int i when i >= 7 && i <= 13 => "Attack",
                _ => "Scythe",
            };
            if (turnNumber % 2 == 1)
                AddBuff(52, 2);
        }
    }
}