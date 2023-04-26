namespace STV
{
    public class LargeAcidSlime : Enemy
    {
        public LargeAcidSlime()
        {
            this.Name = "Acid Slime (L)";
            this.TopHP = 70;
            this.BottomHP = 65;
            this.Intents = new() { "Corrosive Spit", "Lick", "Tackle" };
        }

        public LargeAcidSlime(Enemy e)
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
            if (Intent == "Corrosive Spit")
            {
                Attack(hero, 11, encounter);
                for (int i = 0; i < 2; i++)
                    hero.DiscardPile.Add(Dict.cardL[358]);
            }
            else if (Intent == "Tackle")
                Attack(hero, 16, encounter);
            else if (Intent == "Lick") 
                hero.AddBuff(2, 2);
            else
            {
                // Split Function
            }
        }

        public override void SetEnemyIntent(int turnNumber, List<Enemy> encounter)
        {
            Intent = EnemyRNG.Next(0, 20) switch
            {
                int i when i >= 0 && i <= 7 => "Corrosive Spit",
                int i when i >= 8 && i <= 15 => "Tackle",
                _ => "Lick",
            };
        }
    }
}
