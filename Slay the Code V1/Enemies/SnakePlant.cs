namespace STV
{
    public class SnakePlant : Enemy
    {
        public SnakePlant()
        {
            Name = "Snake Plant";
            TopHP = 80;
            BottomHP = 75;
            Intents = new() { "Chomp", "Enfeebling Spores" };
            MaxHP = EnemyRNG.Next(BottomHP, TopHP);
            Hp = MaxHP;
            Block = 0;
            Buffs = new();
            AddBuff(106, 3);
            Actions = new();
            Relics = new();
        }

        public override void EnemyAction(Hero hero, List<Enemy> encounter)
        {
            if (Intent == "Enfeebling Spores")
            {
                hero.AddBuff(2, 2);
                hero.AddBuff(6, 2);
            }
            else for (int i = 0; i < 3; i++)
                    Attack(hero, 7, encounter);
        }

        public override void SetEnemyIntent(int turnNumber, List<Enemy> encounter)
        {
            Intent = EnemyRNG.Next(0, 20) switch
            {
                int i when i >= 0 && i <= 12 => "Chomp",
                _ => "Enfeebling Spores",
            };
        }
    }
}