namespace STV
{
    public class RedLouse : Enemy
    {
        public RedLouse()
        {
            this.Name = "Red Louse";
            this.TopHP = 15;
            this.BottomHP = 10;
            this.Intents = new() { "Bite", "Grow" };
        }

        public RedLouse(Enemy e)
        {
            this.Name = e.Name;
            this.MaxHP = EnemyRNG.Next(e.BottomHP, e.TopHP);
            this.Hp = this.MaxHP;
            this.Block = 0;
            this.Intents = e.Intents;
            this.Buffs = new();
            AddBuff(3, EnemyRNG.Next(3, 8));
            this.Actions = new();
            this.Relics = new();
        }

        public override void EnemyAction(Hero hero, List<Enemy> encounter)
        {
            if (Intent == "Bite")
                Attack(hero, EnemyRNG.Next(5,8), encounter);
            else AddBuff(4, 3);
        }

        public override void SetEnemyIntent(int turnNumber, List<Enemy> encounter)
        {
            Intent = EnemyRNG.Next(0, 20) switch
            {
                int i when i >= 0 && i <= 4 => "Grow",
                _ => "Bite",
            };
        }
    }
}