namespace STV
{
    public class Dagger : Enemy
    {
        public Dagger()
        {
            Name = "Dagger";
            MaxHP = EnemyRNG.Next(20,26);
            Hp = MaxHP;
            Block = 0;
            Buffs = new();
            Actions = new();
            Relics = new();
        }

        public override void EnemyAction(Hero hero, List<Enemy> encounter)
        {
            if (Intent == "Stab")
            {
                Attack(hero, 9, encounter);
                hero.DiscardPile.Add(new(Dict.cardL[357]));
            }               
            else
            {
                Console.WriteLine("The Dagger... exploded. Weird flex but okay.");
                Attack(hero, 25, encounter);
                Hp = 0;
            }
        }

        public override void SetEnemyIntent(int turnNumber, List<Enemy> encounter)
        {
            if (Actions.Count == 0)
                Intent = "Stab";
            else Intent = "Explode";
        }
    }
}