namespace STV
{
    public class WrithingMass : Enemy
    {
        public WrithingMass()
        {
            Name = "Writhing Mass";
            MaxHP = 160;
            Hp = MaxHP;
            Block = 0;
            Buffs = new();
            AddBuff(106, 3);
            AddBuff(113, 1);
            Actions = new();
            Relics = new();
        }

        public override void EnemyAction(Hero hero, List<Enemy> encounter)
        {
            if (Intent == "Wither")
            {
                Attack(hero, 10, encounter);
                hero.AddBuff(2, 2);
                hero.AddBuff(1, 2);
            }
            else if (Intent == "Flail")
            {
                Attack(hero, 15, encounter);
                GainBlock(16);
            }
            else if (Intent == "Multi-Strike")
                for (int i = 0; i < 3; i++)
                    Attack(hero, 7, encounter);
            else if (Intent == "Implant")
                hero.AddToDeck(Dict.cardL[343]);
            else Attack(hero, 32, encounter);
        }

        public override void SetEnemyIntent(int turnNumber, List<Enemy> encounter)
        {
            if (turnNumber == 0)
                Intent = EnemyRNG.Next(0, 4) switch
                {
                    1 => "Multi-Strike",
                    2 => "Strong Strike",
                    3 => "Flail",
                    _ => "Wither",
                };
            else if (!Actions.Contains("Implant"))
                Intent = EnemyRNG.Next(0, 20) switch
                {
                    int i when i>= 0 && i <= 1 => "Implant",
                    int i when i >= 2 && i <= 3 => "Strong Strike",
                    int i when i >= 4 && i <= 9 => "Flail",
                    int i when i >= 10 && i <= 15 => "Multi-Strike",
                    _ => "Wither",
                };
            else Intent = EnemyRNG.Next(0, 20) switch
            {
                int i when i >= 0 && i <= 2 => "Strong Strike",
                int i when i >= 3 && i <= 8 => "Flail",
                int i when i >= 9 && i <= 14 => "Multi-Strike",
                _ => "Wither",
            };
        }
    }
}