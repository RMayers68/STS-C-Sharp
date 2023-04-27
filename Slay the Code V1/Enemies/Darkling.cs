namespace STV
{
    public class Darkling : Enemy
    {
        int D { get; set; }
        bool MiddleDarkling { get; set; }
        public Darkling(bool Middle = false)
        {
            Name = "Darkling";
            MaxHP = EnemyRNG.Next(48, 57);
            Hp = MaxHP;
            Block = 0;
            Buffs = new();
            Actions = new();
            Relics = new();
            D = EnemyRNG.Next(7, 12);
            MiddleDarkling = Middle;
        }

        public override void EnemyAction(Hero hero, List<Enemy> encounter)
        {
            if (Intent == "Reincarnate")
                HealHP(MaxHP / 2);
            else if (Intent == "Chomp")
                for (int i = 0; i < 2; i++)
                    Attack(hero, 8, encounter);
            else if (Intent == "Harden")
                GainBlock(12);
            else if (Intent == "Nip")
                Attack(hero, D, encounter);
            else Console.WriteLine("One of the defeated Darklings is regrowing");

        }

        public override void SetEnemyIntent(int turnNumber, List<Enemy> encounter)
        {
            if (turnNumber == 0)
            {
                if (EnemyRNG.Next(2) == 0)
                    Intent = "Nip";
                else Intent = "Harden";
            }
            else if (Hp <= 0)
            {
                if (Intent == "Regrow")
                    Intent = "Reincarnate";
                else Intent = "Regrow";
            }
            else
            {
                if (MiddleDarkling)
                {
                    if (EnemyRNG.Next(2) == 0)
                        Intent = "Nip";
                    else Intent = "Harden";
                }
                else Intent = EnemyRNG.Next(0, 20) switch
                {
                    int i when i >= 0 && i <= 5 => "Nip",
                    int i when i >= 6 && i <= 13 => "Chomp",
                    _ => "Harden",
                };
            }
        }
    }
}