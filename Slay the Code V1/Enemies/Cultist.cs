namespace STV
{
    public class Cultist : Enemy
    {
        public Cultist()
        {
            Name = "Cultist";
            TopHP = 55;
            BottomHP = 48;
            Intents = new() { "Incantation", "Dark Strike" };
            MaxHP = EnemyRNG.Next(BottomHP, TopHP);
            Hp = MaxHP;
            Block = 0;
            Buffs = new();
            Actions = new();
            Relics = new();
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