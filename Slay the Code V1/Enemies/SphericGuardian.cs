namespace STV
{
    public class SphericGuardian : Enemy
    {
        public SphericGuardian()
        {
            Name = "Spheric Guardian";
            Intents = new() { "Slam", "Activate", "Harden", "Attack/Debuff" };
            Hp = 20;
            Block = 40;
            Buffs = new();
            AddBuff(20, 1);
            AddBuff(8, 3);
            Actions = new();
            Relics = new();
        }

        public override void EnemyAction(Hero hero, List<Enemy> encounter)
        {
            if (Intent == "Attack/Debuff")
            {
                Attack(hero, 10, encounter);
                hero.AddBuff(6, 5);
            }
            else if (Intent == "Harden")
            {
                Attack(hero, 10, encounter);
                GainBlock(15);
            }
            else if (Intent == "Activate")
                GainBlock(25);
            else for (int i = 0; i < 2; i++)
                    Attack(hero, 10, encounter);
        }

        public override void SetEnemyIntent(int turnNumber, List<Enemy> encounter)
        {
            if (turnNumber == 0)
                Intent = "Activate";
            else if (turnNumber == 1)
                Intent = "Attack/Debuff";
            else if (turnNumber % 2 == 0)
                Intent = "Slam";
            else Intent = "Harden";
        }
    }
}