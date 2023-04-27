namespace STV
{
    public class Exploder : Enemy
    {
        public Exploder()
        {
            Name = "Exploder";
            MaxHP = 30;
            Hp = MaxHP;
            Block = 0;
            Buffs = new();
            AddBuff(109, 3);
            Actions = new();
            Relics = new();
        }

        public override void EnemyAction(Hero hero, List<Enemy> encounter)
        {
            if (Intent == "Slam")
                Attack(hero, 9, encounter);
            else
            {
                Console.WriteLine("The Exploder... exploded. Obviously.");
                Attack(hero, 30, encounter);
                Hp = 0;
            }
        }

        public override void SetEnemyIntent(int turnNumber, List<Enemy> encounter)
        {
            if (turnNumber != 2)
                Intent = "Slam";
            else Intent = "Explode";
        }
    }
}