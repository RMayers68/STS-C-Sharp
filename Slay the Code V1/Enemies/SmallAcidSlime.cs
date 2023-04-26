namespace STV
{
    public class SmallAcidSlime : Enemy
    {
        public SmallAcidSlime()
        {
            this.Name = "Acid Slime (S)";
            this.TopHP = 13;
            this.BottomHP = 8;
            this.Intents = new() { "Lick", "Tackle" };
        }

        public SmallAcidSlime(Enemy e)
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
            if (Intent == "Tackle")
                Attack(hero, 3, encounter);
            else hero.AddBuff(2, 1);
        }

        public override void SetEnemyIntent(int turnNumber, List<Enemy> encounter)
        {
            Intent = EnemyRNG.Next(0, 20) switch
            {
                int i when i >= 0 && i <= 9 => "Tackle",
                _ => "Lick",
            };
        }
    }
}