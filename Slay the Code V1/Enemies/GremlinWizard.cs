namespace STV
{
    public class GremlinWizard : Enemy
    {
        public GremlinWizard(bool minion = false)
        {
            Name = "Gremlin Wizard";
            MaxHP = EnemyRNG.Next(23, 26);
            Hp = MaxHP;
            Block = 0;
            Buffs = new();
            if (minion)
                AddBuff(118, 1);
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
