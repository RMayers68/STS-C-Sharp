namespace STV
{
    public class Cultist : Enemy
    {
        public Cultist(bool minion = false)
        {
            Name = "Cultist";
            MaxHP = EnemyRNG.Next(48, 55);
            Hp = MaxHP;
            Block = 0;
            Buffs = new();
            if (minion)
                AddBuff(118, 1);
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