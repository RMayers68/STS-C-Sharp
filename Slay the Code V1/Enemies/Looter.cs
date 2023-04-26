namespace STV
{
    public class Looter : Enemy
    {
        public Looter()
        {
            this.Name = "Looter";
            this.TopHP = 49;
            this.BottomHP = 44;
            this.Intents = new() { "Lunge", "Mug" };
        }

        public Looter(Enemy e)
        {
            this.Name = e.Name;
            this.MaxHP = EnemyRNG.Next(e.BottomHP, e.TopHP);
            this.Hp = this.MaxHP;
            this.Block = 0;
            this.Intents = e.Intents;
            this.Buffs = new();
            AddBuff(18, 15);
            this.Actions = new();
            this.Relics = new();
        }

        public override void EnemyAction(Hero hero, List<Enemy> encounter)
        {
            if (Intent == "Lunge")
                Attack(hero, 12, encounter);
            else if (Intent == "Mug")
                Attack(hero, 10, encounter);
            else if (Intent == "Smoke Bomb")
                GainBlock(6);
            if (Intent == "Lunge" || Intent == "Mug")
            {
                hero.GoldChange(-15);
                Gold += 15;
                Console.WriteLine($"The {Name} stole 15 Gold!");
            }
            else
            {
                Console.WriteLine($"The {Name} has escaped!");
                encounter.Remove(this);
            }
        }

        public override void SetEnemyIntent(int turnNumber, List<Enemy> encounter)
        {
            if (turnNumber == 0 || turnNumber == 1)
                Intent = "Mug";
            else if (turnNumber == 3)
                Intent = EnemyRNG.Next(0, 20) switch
                {
                    int i when i >= 0 && i <= 7 => "Lunge",
                    _ => "Smoke Bomb",
                };
            if (Actions != null && Actions.Count >= 3)
                if (Actions[^1] == "Lunge")
                    Intent = "Smoke Bomb";
                else Intent = "Escape";
        }
    }
}
