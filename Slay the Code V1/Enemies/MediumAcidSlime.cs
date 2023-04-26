namespace STV
{
    public class MediumAcidSlime : Enemy
    {
        public MediumAcidSlime()
        {
            this.Name = "Acid Slime (M)";
            this.TopHP = 33;
            this.BottomHP = 28;
            this.Intents = new() {"Corrosive Spit", "Lick", "Tackle" };
        }

        public MediumAcidSlime(Enemy e)
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
            if (Intent == "Corrosive Spit")
            {
                Attack(hero, 7, encounter);
                hero.DiscardPile.Add(Dict.cardL[358]);
            }
            else if (Intent == "Tackle")
                Attack(hero, 10, encounter);
            else hero.AddBuff(2, 1);
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