namespace STV
{
    public class Maw : Enemy
    {
        int T { get; set; }
        public Maw()
        {
            Name = "The Maw";
            MaxHP = 300;
            Hp = MaxHP;
            Block = 0;
            Buffs = new();
            Actions = new();
            Relics = new();
        }

        public override void EnemyAction(Hero hero, List<Enemy> encounter)
        {
            if (Intent == "Roar")
            {
                hero.AddBuff(2, 3);
                hero.AddBuff(6, 3);
            }
            else if (Intent == "Nom")
                for (int i = 0; i < T; i++)
                    Attack(hero, 5, encounter);
            else if (Intent == "Slam")
                Attack(hero, 25, encounter);
            else AddBuff(4, 3);
        }

        public override void SetEnemyIntent(int turnNumber, List<Enemy> encounter)
        {
            if (turnNumber == 0)
                Intent = "Roar";
            else if (Intent == "Roar" || Intent == "Drool")
                Intent = EnemyRNG.Next(0, 2) switch
                {
                    1 => "Slam",
                    _ => "Nom",
                };
            else if (Intent == "Slam")
                Intent = EnemyRNG.Next(0, 2) switch
                {
                    1 => "Roar",
                    _ => "Drool",
                };
            else Intent = "Drool";
            T = Convert.ToInt32(Math.Ceiling(turnNumber / 2.0));
        }
    }
}