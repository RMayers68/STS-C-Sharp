namespace STV
{
    public class LargeSpikeSlime : Enemy
    {
        public LargeSpikeSlime()
        {
            this.Name = "Spike Slime (L)";
            this.TopHP = 71;
            this.BottomHP = 64;
            this.Intents = new() { "Split", "Lick", "Flame Tackle" };
        }

        public LargeSpikeSlime(Enemy e)
        {
            this.Name = e.Name;
            this.MaxHP = EnemyRNG.Next(e.BottomHP, e.TopHP);
            this.Hp = this.MaxHP;
            this.Block = 0;
            this.Intents = e.Intents;
            this.Buffs = new();
            AddBuff(101, 1);
            this.Actions = new();
            this.Relics = new();
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
                // Split Function
            }
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