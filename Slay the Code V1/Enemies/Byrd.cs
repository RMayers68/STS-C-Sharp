namespace STV
{
    public class Byrd : Enemy
    {
        public Byrd()
        {
            Name = "Byrd";
            MaxHP = EnemyRNG.Next(25, 32);
            Hp = MaxHP;
            Block = 0;
            Buffs = new();
            AddBuff(104, 3);
            Actions = new();
            Relics = new();
        }

        public override void EnemyAction(Hero hero, List<Enemy> encounter)
        {
            if (Intent == "Peck")
                for (int i = 0; i < 5; i++)
                    Attack(hero, 1, encounter);
            else if (Intent == "Swoop")
                Attack(hero, 12, encounter);
            else if (Intent == "Caw")
                AddBuff(4, 1);
            else if (Intent == "Headbutt")
                Attack(hero, 3, encounter);
            else AddBuff(104, 3);
        }

        public override void SetEnemyIntent(int turnNumber, List<Enemy> encounter)
        {
            if (turnNumber == 0)
            {
                Intent = EnemyRNG.Next(0, 50) switch
                {
                    int i when i >= 0 && i <= 18 => "Caw",
                    _ => "Peck",
                };
            }
            else if (!HasBuff("Flying"))
            {
                if (Actions[^1] != "Headbutt")
                    Intent = "Headbutt";
                else Intent = "Fly";
            }
            else
            {
                Intent = EnemyRNG.Next(0, 20) switch
                {
                    int i when i >= 0 && i <= 9 => "Peck",
                    int i when i >= 10 && i <= 13 => "Swoop",
                    _ => "Caw",
                };
            }
        }
    }
}