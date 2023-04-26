namespace STV
{
    public class Cultist : Enemy
    {
        public Cultist()
        {
            this.Name = "Cultist";
            this.TopHP = 55;
            this.BottomHP = 48;
            this.Intents = new() { "Incantation", "Dark Strike" };
        }

        public Cultist(Enemy e)
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
            if (Intent == "Dark Strike")
                Attack(hero, 6, encounter);
            else AddBuff(3, 3);
        }

        public override void SetEnemyIntent(int turnNumber, List<Enemy> encounter)
        {
            if (turnNumber == 0)
                Intent = "Incantation";
            else Intent = "Dark Strike";
        }
    }
}
