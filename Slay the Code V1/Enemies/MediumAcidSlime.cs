namespace STV
{
    public class MediumAcidSlime : Enemy
    {
        public MediumAcidSlime()
        {
            Name = "Acid Slime (M)";
            MaxHP = EnemyRNG.Next(28, 33);
            Hp = MaxHP;
            Block = 0;
            Buffs = new();
            Actions = new();
            Relics = new();
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