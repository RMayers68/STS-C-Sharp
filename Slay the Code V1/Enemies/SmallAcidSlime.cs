namespace STV
{
    public class SmallAcidSlime : Enemy
    {
        public SmallAcidSlime()
        {
            Name = "Acid Slime (S)";
            TopHP = 13;
            BottomHP = 8;
            Intents = new() { "Lick", "Tackle" };
            MaxHP = EnemyRNG.Next(BottomHP, TopHP);
            Hp = MaxHP;
            Block = 0;
            Buffs = new();
            Actions = new();
            Relics = new();
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