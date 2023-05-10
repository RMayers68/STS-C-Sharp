namespace STV
{
    public class Snecko : Enemy
    {
        public Snecko()
        {
            Name = "Snecko";
            MaxHP = EnemyRNG.Next(114, 121);
            Hp = MaxHP;
            Block = 0;
            Buffs = new();
            Actions = new();
            Relics = new();
        }

        public override void EnemyAction(Hero hero, List<Enemy> encounter)
        {
            if (Intent == "Tail Whip")
            {
                Attack(hero, 8, encounter);
                hero.AddBuff(1, 2);
            }
            else if (Intent == "Bite")
                Attack(hero, 15, encounter);
            else hero.AddBuff(96, 1);
        }

        public override void SetEnemyIntent(int turnNumber, List<Enemy> encounter)
        {
            if (turnNumber == 0)
                Intent = "Perplexing Glare";
            else Intent = EnemyRNG.Next(0, 20) switch
            {
                int i when i >= 0 && i <= 11 => "Bite",
                _ => "Tail Whip",
            };
        }
    }
}