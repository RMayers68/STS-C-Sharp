namespace STV
{
    public class JawWorm : Enemy
    {
        public JawWorm(bool act3 = false)
        {
            Name = "Jaw Worm";
            MaxHP = EnemyRNG.Next(40, 45);
            Hp = MaxHP;
            Block = 0;
            Buffs = new();
            if (act3)
            {
                AddBuff(4, 3);
                GainBlock(6);
            }
            Actions = new();
            Relics = new();
        }

        public override void EnemyAction(Hero hero, List<Enemy> encounter)
        {
            if (Intent == "Bellow")
            {
                AddBuff(4, 3);
                GainBlock(6);
            }
            else if (Intent == "Thrash")
            {
                Attack(hero, 7, encounter);
                GainBlock(5);
            }               
            else Attack(hero, 11, encounter);           
        }

        public override void SetEnemyIntent(int turnNumber, List<Enemy> encounter)
        {
            if (turnNumber == 0)
                Intent = "Chomp";
            else Intent = EnemyRNG.Next(0, 20) switch
            {
                int i when i >= 0 && i <= 4 => "Chomp",
                int i when i >= 5 && i <= 10 => "Thrash",
                _ => "Bellow",
            };
        }
    }
}
