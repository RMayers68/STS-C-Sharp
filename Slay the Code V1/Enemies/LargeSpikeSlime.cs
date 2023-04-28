namespace STV
{
    public class LargeSpikeSlime : Enemy
    {
        public LargeSpikeSlime(int SlimeBossHP = 0)
        {
            Name = "Spike Slime (L)";
            if (SlimeBossHP > 0)
                MaxHP = SlimeBossHP;
            else MaxHP = EnemyRNG.Next(64, 71);
            Hp = MaxHP;
            Block = 0;
            Buffs = new();
            AddBuff(101, 1);
            Actions = new();
            Relics = new();
        }

        public override void EnemyAction(Hero hero, List<Enemy> encounter)
        {
            if (Intent == "Flame Tackle")
            {
                Attack(hero, 16, encounter);
                for (int i = 0; i < 2; i++)
                    hero.DiscardPile.Add(Dict.cardL[358]);
            }
            else if (Intent == "Lick")
                hero.AddBuff(6, 2);
            else
            {
                encounter.Remove(this);
                encounter.Add(new MediumSpikeSlime(Hp));
                encounter.Add(new MediumSpikeSlime(Hp));
            }
        }

        public override void SetEnemyIntent(int turnNumber, List<Enemy> encounter)
        {
            if (Hp <= MaxHP / 2)
                Intent = "Split";
            else Intent = EnemyRNG.Next(0, 20) switch
            {
                int i when i >= 0 && i <= 5 => "Flame Tackle",
                _ => "Lick",
            };
        }
    }
}