namespace STV
{
    public class BlueSlaver : Enemy
    {
        public BlueSlaver()
        {
            this.Name = "Blue Slaver";
            this.TopHP = 51;
            this.BottomHP = 46;
            this.Intents = new() { "Stab", "Rake" };
        }

        public BlueSlaver(Enemy e)
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
            if (Intent == "Rake")
            {
                Attack(hero, 7, encounter);
                hero.AddBuff(2, 1);
            }
            else Attack(hero, 12, encounter);
        }

        public override void SetEnemyIntent(int turnNumber, List<Enemy> encounter)
        {
            Intent = EnemyRNG.Next(0, 20) switch
            {
                int i when i >= 0 && i <= 7 => "Rake",
                _ => "Stab",
            };
        }
    }
}