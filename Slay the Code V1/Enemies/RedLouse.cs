namespace STV
{
    public class RedLouse : Enemy
    {
        int D { get; set; }
        public RedLouse()
        {
            Name = "Red Louse";
            MaxHP = EnemyRNG.Next(10, 15);
            Hp = MaxHP;
            Block = 0;
            Buffs = new();
            AddBuff(5, EnemyRNG.Next(3, 8));
            Actions = new();
            Relics = new();
            D = EnemyRNG.Next(5,8);
        }

        public override void EnemyAction(Hero hero, List<Enemy> encounter)
        {
            if (Intent == "Bite")
                Attack(hero, D, encounter);
            else AddBuff(4, 3);
        }

        public override void SetEnemyIntent(int turnNumber, List<Enemy> encounter)
        {
            Intent = EnemyRNG.Next(0, 20) switch
            {
                int i when i >= 0 && i <= 4 => "Grow",
                _ => "Bite",
            };
        }
    }
}