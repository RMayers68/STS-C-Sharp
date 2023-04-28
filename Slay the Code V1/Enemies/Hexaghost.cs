namespace STV
{
    public class Hexaghost : Enemy
    {
        public Hexaghost()
        {
            Name = "Hexaghost";
            MaxHP = 250;
            Hp = MaxHP;
            Block = 0;
            Buffs = new();
            Actions = new();
            Relics = new();
        }

        public override void EnemyAction(Hero hero, List<Enemy> encounter)
        {
            if (Intent == "Inferno")
            {
                for (int i = 0; i < 6; i++)
                    Attack(hero, 2, encounter);
                for (int i = 0;i < 3; i++)
                    //hero.DiscardPile.Add(new Card(Dict.cardL[355]));
                Console.WriteLine($"{Name} has added 3 Burns to your Deck!");
            }
            else if (Intent == "Divider")
            {
                int damage = hero.Hp / 12 + 1;
                for (int i = 0; i < 6; i++)
                    Attack(hero, damage, encounter);
            }
            else if (Intent == "Sear")
            {
                Attack(hero, 6, encounter);
                //hero.DiscardPile.Add(new Card(Dict.cardL[355]));
                Console.WriteLine($"{Name} has added a Burn to your Deck!");
            }
            else if (Intent == "Inflame")
            {
                AddBuff(4, 2);
                GainBlock(12);
            }
            else if (Intent == "Activate")
                Console.WriteLine("This enemy is preparing a huge attack!");
            else for (int i = 0; i < 2 ; i++)
                    Attack(hero, 5, encounter);
        }

        public override void SetEnemyIntent(int turnNumber, List<Enemy> encounter)
        {
            if (turnNumber == 0)
                Intent = "Activate";
            if (turnNumber == 1)
                Intent = "Divider";
            Intent = ((Actions.Count - 2) % 7) switch
            {
                int i when i == 0 || i == 2 || i == 5 => "Sear",
                int i when i == 1 || i == 4 => "Slice",
                int i when i == 3 => "Inflame",
                _ => "Inferno",
            };
        }
    }
}