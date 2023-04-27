namespace STV
{
    public class OrbWalker : Enemy
    {
        public OrbWalker()
        {
            Name = "OrbWalker";
            MaxHP = EnemyRNG.Next(90, 97);
            Hp = MaxHP;
            Block = 0;
            Buffs = new();
            AddBuff(3, 3);
            Actions = new();
            Relics = new();
        }

        public override void EnemyAction(Hero hero, List<Enemy> encounter)
        {
            if (Intent == "Laser")
            {
                Attack(hero, 10, encounter);
                hero.AddToDrawPile(new(Dict.cardL[355]), true);
                hero.DiscardPile.Add(new(Dict.cardL[355]));
            }        
            else Attack(hero, 15, encounter);
        }

        public override void SetEnemyIntent(int turnNumber, List<Enemy> encounter)
        {
            Intent = EnemyRNG.Next(0, 20) switch
            {
                int i when i >= 0 && i <= 11 => "Laser",
                _ => "Claw",
            };
        }
    }
}