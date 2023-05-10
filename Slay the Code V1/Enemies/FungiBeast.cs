namespace STV
{
    public class FungiBeast : Enemy
    {
        public FungiBeast()
        {
            Name = "Fungi Beast";
            MaxHP = EnemyRNG.Next(24, 29);
            Hp = MaxHP;
            Block = 0;
            Buffs = new();
            AddBuff(102, 2);
            Actions = new();
            Relics = new();
        }

        public override void EnemyAction(Hero hero, List<Enemy> encounter)
        {
            if (Intent == "Bite")
                Attack(hero, 6, encounter);
            else AddBuff(4, 3);
        }

        public override void SetEnemyIntent(int turnNumber, List<Enemy> encounter)
        {
            Intent = EnemyRNG.Next(0, 20) switch
            {
                int i when i >= 0 && i <= 7 => "Grow",
                _ => "Bite",
            };
        }
    }
}
