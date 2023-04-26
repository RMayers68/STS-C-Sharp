namespace STV
{
    public class JawWorm : Enemy
    {
        public JawWorm()
        {
            this.Name = "Jaw Worm";
            this.TopHP = 45;
            this.BottomHP = 40;
            this.Intents = new() { "Chomp", "Thrash", "Bellow" };
        }

        public JawWorm(Enemy e)
        {
            this.Name = e.Name;
            this.MaxHP = EnemyRNG.Next(e.BottomHP, e.TopHP);
            this.Hp = this.MaxHP;
            this.Block = 0;
            this.Intents = e.Intents;
            this.Buffs = new();
            this.Actions = new();
            this.Relics = new();
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
