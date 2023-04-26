namespace STV
{
    public class MediumSpikeSlime : Enemy
    {
        public MediumSpikeSlime()
        {
            this.Name = "Spike Slime (M)";
            this.TopHP = 33;
            this.BottomHP = 28;
            this.Intents = new() { "Lick", " Flame Tackle" };
        }

        public MediumSpikeSlime(Enemy e)
        {
            this.Name = e.Name;
            this.MaxHP = EnemyRNG.Next(e.BottomHP, e.TopHP);
            this.Hp = this.MaxHP;
            this.Block = 0;
            this.Intents = e.Intents;
            this.Buffs = new();
            this.Actions = new();
            this.Relics = new();
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