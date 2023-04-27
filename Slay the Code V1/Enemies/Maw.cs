namespace STV
{
    public class Maw : Enemy
    {
        int T { get; set; }
        public Maw()
        {
            Name = "The Maw";
            MaxHP = 300;
            Hp = MaxHP;
            Block = 0;
            Buffs = new();
            Actions = new();
            Relics = new();
        }

        public override void EnemyAction(Hero hero, List<Enemy> encounter)
        {
            if (Intent == "Repulse")
                for (int i = 0; i < 2; i++)
                    hero.AddToDrawPile(new(Dict.cardL[356]), true);
            else Attack(hero, 11, encounter);
        }

        public override void SetEnemyIntent(int turnNumber, List<Enemy> encounter)
        {
            Intent = EnemyRNG.Next(0, 20) switch
            {
                int i when i >= 0 && i <= 15 => "Repulse",
                _ => "Bash",
            };
        }
    }
}