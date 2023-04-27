namespace STV
{
    public class Centurion : Enemy
    {
        public Centurion()
        {
            Name = "Centurion";
            TopHP = 81;
            BottomHP = 76;
            Intents = new() { "Slash", "Fury", "Defend" };
            MaxHP = EnemyRNG.Next(BottomHP, TopHP);
            Hp = MaxHP;
            Block = 0;
            Buffs = new();
            Actions = new();
            Relics = new();
        }

        public override void EnemyAction(Hero hero, List<Enemy> encounter)
        {
            if (Intent == "Defend" && encounter.Count > 1)
                encounter[1].GainBlock(15);
            else if (Intent == "Fury")
                for (int i = 0; i < 3; i++)
                    Attack(hero, 6, encounter);
            else Attack(hero, 12, encounter);                    
        }

        public override void SetEnemyIntent(int turnNumber, List<Enemy> encounter)
        {
            if (encounter.Count > 1)
            {
                Intent = EnemyRNG.Next(0, 20) switch
                {
                    int i when i >= 0 && i <= 12 => "Slash",
                    _ => "Defend",
                };
            }
            else
            {
                Intent = EnemyRNG.Next(0, 20) switch
                {
                    int i when i >= 0 && i <= 12 => "Slash",
                    _ => "Fury",
                };
            }
        }
    }
}