namespace STV
{
    public class SpireGrowth : Enemy
    {
        public SpireGrowth()
        {
            Name = "Spire Growth";
            MaxHP = 170;
            Hp = MaxHP;
            Block = 0;
            Buffs = new();
            Actions = new();
            Relics = new();
        }

        public override void EnemyAction(Hero hero, List<Enemy> encounter)
        {
            if (Intent == "Constrict")
                hero.AddBuff(110, 10);
            else if (Intent == "Quick Tackle")
                Attack(hero, 16, encounter);
            else Attack(hero, 22, encounter);
        }

        public override void SetEnemyIntent(int turnNumber, List<Enemy> encounter)
        {
            if (turnNumber == 0)
                Intent = "Constrict";
            else
                Intent = EnemyRNG.Next(0, 2) switch
                {
                    1 => "Quick Tackle",
                    _ => "Smash",
                };
        }
    }
}
