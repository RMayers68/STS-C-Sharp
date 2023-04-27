namespace STV
{
    public class Lagavulin : Enemy
    {
        public Lagavulin()
        {
            Name = "Lagavulin";
            TopHP = 112;
            BottomHP = 109;
            Intents = new() { "Sleeping", "Attack", "Siphon Soul" };
            MaxHP = EnemyRNG.Next(BottomHP, TopHP);
            Hp = MaxHP;
            Block = 0;
            Buffs = new();
            AddBuff(32, 8);
            AddBuff(15, 3);
            Actions = new();
            Relics = new();
        }

        public override void EnemyAction(Hero hero, List<Enemy> encounter)
        {
            if (Intent == "Siphon Soul")
            {
                hero.AddBuff(9, -1);
                hero.AddBuff(4, -1);
            }
            else if (Intent == "Attack")
                Attack(hero, 18, encounter);
            else Console.WriteLine($"{Name} is sleeping, be cautious on waking it...");
        }

        public override void SetEnemyIntent(int turnNumber, List<Enemy> encounter)
        {
            if (HasBuff("Asleep"))
                Intent = "Sleeping";
            if (Actions.Count >= 3 && Actions[^1] == "Attack" && Actions[^2] == "Attack")
                Intent = "Siphon Soul";
            else Intent = "Attack";
            if (!HasBuff("Asleep") && FindBuff("Metallicize") is Buff metal && metal !=  null)
                Buffs.Remove(metal);
        }
    }
}