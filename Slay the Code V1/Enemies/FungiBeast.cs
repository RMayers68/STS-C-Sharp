namespace STV
{
    public class FungiBeast : Enemy
    {
        public FungiBeast()
        {
            this.Name = "Fungi Beast";
            this.TopHP = 29;
            this.BottomHP = 24;
            this.Intents = new() { "Bite", "Grow" };
        }

        public FungiBeast(Enemy e)
        {
            this.Name = e.Name;
            this.MaxHP = EnemyRNG.Next(e.BottomHP, e.TopHP);
            this.Hp = this.MaxHP;
            this.Block = 0;
            this.Intents = e.Intents;
            this.Buffs = new();
            AddBuff(102, 2);
            this.Actions = new();
            this.Relics = new();
        }

        public override void EnemyAction(Hero hero, List<Enemy> encounter)
        {
            if (Intent == "Bite")
                Attack(hero, 6, encounter);
            else AddBuff(4, 3);
        }

        public override void SetEnemyIntent(int turnNumber, List<Enemy> encounter)
        {
            Intent = EnemyRNG.Next(0, 20) switch
            {
                int i when i >= 0 && i <= 7 => "Grow",
                _ => "Bite",
            };
        }
    }
}
