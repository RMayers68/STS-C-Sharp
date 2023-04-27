namespace STV
{
    public class BronzeOrb : Enemy
    {
        Card? stolenCard;

        public BronzeOrb()
        {
            Name = "Bronze Orb";
            MaxHP = EnemyRNG.Next(52, 59);
            Hp = MaxHP;
            Block = 0;
            Buffs = new();
            Actions = new();
            Relics = new();
        }

        public override void EnemyAction(Hero hero, List<Enemy> encounter)
        {
            if (Intent == "Stasis")
            {
                if (hero.DrawPile.Count > 0)
                {
                    stolenCard = hero.DrawPile.Find(x => x.Rarity == "Rare") is Card rare && rare != null ? rare :
                        hero.DrawPile.Find(x => x.Rarity == "Uncommon") is Card uncommon && uncommon != null ? uncommon :
                        hero.DrawPile.Find(x => x.Rarity == "Common");
                    if (stolenCard != null)
                        hero.DrawPile.Remove(stolenCard);
                }
                if (stolenCard == null)
                {
                    stolenCard = hero.DiscardPile.Find(x => x.Rarity == "Rare") is Card rare && rare != null ? rare :
                        hero.DiscardPile.Find(x => x.Rarity == "Uncommon") is Card uncommon && uncommon != null ? uncommon :
                        hero.DiscardPile.Find(x => x.Rarity == "Common");
                    if (stolenCard != null)
                        hero.DiscardPile.Remove(stolenCard);
                }    
            }
            else if (Intent == "Support Beam")
                encounter.Find(x => x.Name == "Bronze Automaton").GainBlock(12);
            else Attack(hero, 8, encounter);
        }

        public override void SetEnemyIntent(int turnNumber, List<Enemy> encounter)
        {
            if (!Actions.Contains("Stasis"))
                Intent = EnemyRNG.Next(20) switch
                {
                    int i when i >= 0 && i <= 14 => "Stasis",
                    15 => "Beam",
                    _ => "Support Beam",
                };
            else Intent = EnemyRNG.Next(20) switch
            {
                int i when i >= 0 && i <= 14 => "Support Beam",
                _ => "Beam",
            };
        }
    }
}
