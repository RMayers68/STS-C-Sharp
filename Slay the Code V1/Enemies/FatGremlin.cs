namespace STV
{
    public class FatGremlin : Enemy
    {
        public FatGremlin()
        {
            this.Name = "Fat Gremlin";
            this.TopHP = 18;
            this.BottomHP = 13;
            this.Intents = new() { "Smash" };
        }

        public FatGremlin(Enemy e)
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
            Attack(hero, 4, encounter);
            hero.AddBuff(2, 1);
        }

        public override void SetEnemyIntent(int turnNumber, List<Enemy> encounter)
        {
            Intent = Intents.First();
        }
    }
}
