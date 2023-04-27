namespace STV
{
    public class ShieldGremlin : Enemy
    {
        public ShieldGremlin()
        {
            Name = "Shield Gremlin";
            TopHP = 16;
            BottomHP = 12;
            Intents = new() { "Protect" , "Shield Bash" };
            MaxHP = EnemyRNG.Next(BottomHP, TopHP);
            Hp = MaxHP;
            Block = 0;
            Buffs = new();
            Actions = new();
            Relics = new();
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