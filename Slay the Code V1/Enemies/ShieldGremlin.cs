namespace STV
{
    public class ShieldGremlin : Enemy
    {
        public ShieldGremlin()
        {
            this.Name = "Shield Gremlin";
            this.TopHP = 16;
            this.BottomHP = 12;
            this.Intents = new() { "Protect" , "Shield Bash" };
        }

        public ShieldGremlin(Enemy e)
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
            if (Intent == "Protect")
            {
                if (encounter.Count > 1)
                    encounter.Find(x => x.Name != "Gremlin Wizard").GainBlock(7);
                else GainBlock(7);
            }
            else Attack(hero, 6, encounter);
        }

        public override void SetEnemyIntent(int turnNumber, List<Enemy> encounter)
        {
            if (encounter.Count < 2)
                Intent = "Shield Bash";
            else Intent = "Protect";
        }
    }
}