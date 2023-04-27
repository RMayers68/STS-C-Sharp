namespace STV
{
    public class ShelledParasite : Enemy
    {
        public ShelledParasite()
        {
            Name = "Shelled Parasite";
            TopHP = 73;
            BottomHP = 68;
            Intents = new() { "Double Strike", "Suck", "Fell" };
            MaxHP = EnemyRNG.Next(BottomHP, TopHP);
            Hp = MaxHP;
            Block = 0;
            Buffs = new();
            AddBuff(95, 14);
            Actions = new();
            Relics = new();
        }

        public override void EnemyAction(Hero hero, List<Enemy> encounter)
        {
            if (Intent == "Fell")
            {
                Attack(hero, 18, encounter);
                hero.AddBuff(6, 2);
            }
            else if (Intent == "Suck")
            {
                int heroHP = hero.Hp;
                Attack(hero, 10, encounter);
                heroHP -= hero.Hp;
                HealHP(heroHP);
            }
            else for (int i = 0; i < 2; i++)
                    Attack(hero, 6, encounter);
        }

        public override void SetEnemyIntent(int turnNumber, List<Enemy> encounter)
        {
            if (turnNumber == 0)
            {
                Intent = EnemyRNG.Next(0, 20) switch
                {
                    int i when i >= 0 && i <= 9 => "Suck",
                    _ => "Double Strike",
                };
            }
            else
            {
                Intent = EnemyRNG.Next(0, 20) switch
                {
                    int i when i >= 0 && i <= 7 => "Suck",
                    int i when i >= 8 && i <= 15 => "Double Strike",
                    _ => "Fell",
                };
            }
        }
    }
}