namespace STV
{
    public class BlueSlaver : Enemy
    {
        public BlueSlaver()
        {
            Name = "Blue Slaver";
            TopHP = 51;
            BottomHP = 46;
            Intents = new() { "Stab", "Rake" };
            MaxHP = EnemyRNG.Next(BottomHP, TopHP);
            Hp = MaxHP;
            Block = 0;
            Buffs = new();
            Actions = new();
            Relics = new();
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