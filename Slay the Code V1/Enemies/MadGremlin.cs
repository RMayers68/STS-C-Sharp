namespace STV
{
    public class MadGremlin : Enemy
    {
        public MadGremlin()
        {
            this.Name = "Mad Gremlin";
            this.TopHP = 25;
            this.BottomHP = 20;
            this.Intents = new() { "Scratch" };
        }

        public MadGremlin(Enemy e)
        {
            this.Name = e.Name;
            this.MaxHP = EnemyRNG.Next(e.BottomHP, e.TopHP);
            this.Hp = this.MaxHP;
            this.Block = 0;
            this.Intents = e.Intents;
            this.Buffs = new();
            AddBuff(103, 1);
            this.Actions = new();
            this.Relics = new();
        }

        public override void EnemyAction(Hero hero, List<Enemy> encounter)
        {
            Attack(hero, 4, encounter);
        }

        public override void SetEnemyIntent(int turnNumber, List<Enemy> encounter)
        {
            Intent = Intents.First();
        }
    }
}