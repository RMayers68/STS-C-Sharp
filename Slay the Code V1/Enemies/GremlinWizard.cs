namespace STV
{
    public class GremlinWizard : Enemy
    {
        public GremlinWizard()
        {
            Name = "Gremlin Wizard";
            TopHP = 26;
            BottomHP = 23;
            Intents = new() { "Charging" , "Ultimate Blast" };
            MaxHP = EnemyRNG.Next(BottomHP, TopHP);
            Hp = MaxHP;
            Block = 0;
            Buffs = new();
            Actions = new();
            Relics = new();
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
