namespace STV
{
    public class MediumSpikeSlime : Enemy
    {
        public MediumSpikeSlime(int LargeSlimeHP = 0)
        {
            Name = "Spike Slime (M)";
            if (LargeSlimeHP > 0)
                MaxHP = LargeSlimeHP;
            else MaxHP = EnemyRNG.Next(28, 33);
            Hp = MaxHP;
            Block = 0;
            Buffs = new();
            Actions = new();
            Relics = new();
        }

        public override void EnemyAction(Hero hero, List<Enemy> encounter)
        {
            if (Intent == "Flame Tackle")
            {
                Attack(hero, 8, encounter);
                hero.DiscardPile.Add(Dict.cardL[358]);
            }
            else hero.AddBuff(6, 1);
        }

        public override void SetEnemyIntent(int turnNumber, List<Enemy> encounter)
        {
            Intent = EnemyRNG.Next(0, 20) switch
            {
                int i when i >= 0 && i <= 5 => "Flame Tackle",
                _ => "Lick",
            };
        }
    }
}