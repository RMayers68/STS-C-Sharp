namespace STV
{
    public class Repulsor : Enemy
    {
        public Repulsor()
        {
            Name = "Repulsor";
            MaxHP = EnemyRNG.Next(29, 36);
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
            else Attack(hero,11,encounter);
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