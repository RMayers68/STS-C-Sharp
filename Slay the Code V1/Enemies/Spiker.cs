namespace STV
{
    public class Spiker : Enemy
    {
        public Spiker()
        {
            Name = "Spiker";
            MaxHP = EnemyRNG.Next(42, 57);
            Hp = MaxHP;
            Block = 0;
            Buffs = new();
            AddBuff(41, 3);
            Actions = new();
            Relics = new();
        }

        public override void EnemyAction(Hero hero, List<Enemy> encounter)
        {
            if (Intent == "Cut")
                Attack(hero, 7, encounter);
            else AddBuff(41, 2);
        }

        public override void SetEnemyIntent(int turnNumber, List<Enemy> encounter)
        {
            if (Actions.FindAll(x => x == "Spike").Count != 6)
                Intent = EnemyRNG.Next(0, 2) switch
                {
                    1 => "Spike",
                    _ => "Cut",
                };
            else Intent = "Cut";
        }
    }
}