namespace STV
{
    public class Looter : Enemy
    {
        public Looter()
        {
            Name = "Looter";
            MaxHP = EnemyRNG.Next(44, 49);
            Hp = MaxHP;
            Block = 0;
            Buffs = new();
            AddBuff(18, 15);
            Actions = new();
            Relics = new();
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
            else if (turnNumber == 2)
                Intent = EnemyRNG.Next(0, 20) switch
                {
                    int i when i >= 0 && i <= 7 => "Lunge",
                    _ => "Smoke Bomb",
                };
            else if (Actions != null && Actions.Count >= 3)
                if (Actions[^1] == "Lunge")
                    Intent = "Smoke Bomb";
                else Intent = "Escape";
        }
    }
}
