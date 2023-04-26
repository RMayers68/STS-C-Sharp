namespace STV
{
    public class GremlinWizard : Enemy
    {
        public GremlinWizard()
        {
            this.Name = "Gremlin Wizard";
            this.TopHP = 26;
            this.BottomHP = 23;
            this.Intents = new() { "Charging" , "Ultimate Blast" };
        }

        public GremlinWizard(Enemy e)
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
            if (Intent == "Ultimate Blast")
                Attack(hero, 25, encounter);
            else Console.WriteLine($"{Name} is charging up!");
        }

        public override void SetEnemyIntent(int turnNumber, List<Enemy> encounter)
        {
            if (Actions != null && Actions.Count % 3 == 0)
                Intent = "Ultimate Blast";
            else Intent = "Charging";
        }
    }
}
