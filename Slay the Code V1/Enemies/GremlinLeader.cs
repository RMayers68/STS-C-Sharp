namespace STV
{
    public class GremlinLeader : Enemy
    {

        public GremlinLeader()
        {
            Name = "Gremlin Leader";
            MaxHP = EnemyRNG.Next(140, 149);
            Hp = MaxHP;
            Block = 0;
            Buffs = new();
            Actions = new();
            Relics = new();
        }

        public override void EnemyAction(Hero hero, List<Enemy> encounter)
        {
            if (Intent == "Rally!")
                for (int i = 0; i < 2; i++)
                    encounter.Add(RandomGremlin(true));
            else if (Intent == "Encourage")
                foreach (Enemy e in encounter)
                {
                    e.AddBuff(4, 3);
                    if (e != this)
                        e.GainBlock(6);
                }
            else for (int i = 0; i < 3; i++)
                    Attack(hero, 6, encounter);
        }

        public override void SetEnemyIntent(int turnNumber, List<Enemy> encounter)
        {
            if (encounter.Count < 2)
                Intent = EnemyRNG.Next(0, 20) switch
                {
                    int i when i >= 0 && i <= 14 => "Rally!",
                    _ => "Stab",
                };
            else if (encounter.Count == 2)
                Intent = Actions.Last() switch
                {
                    "Stab" => EnemyRNG.Next(0, 30) switch
                    {
                        int i when i >= 0 && i <= 18 => "Rally!",
                        _ => "Encourage",
                    },
                    _ => EnemyRNG.Next(0, 30) switch
                    {
                        int i when i >= 0 && i <= 14 => "Rally!",
                        _ => "Stab",
                    },
                };
            else
                Intent = EnemyRNG.Next(0, 30) switch
                {
                    int i when i >= 0 && i <= 19 => "Encourage",
                    _ => "Stab",
                };
        }
    }
}