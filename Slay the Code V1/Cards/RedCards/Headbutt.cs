
namespace STV
{
    public class Headbutt : Card
    {
        public Headbutt(bool Upgraded = false)
        {
            Name = "Headbutt";
            Type = "Attack";
            Rarity = "Common";
            DescriptionModifier = "";
            EnergyCost = 1;
            if (EnergyCost >= 0)
                TmpEnergyCost = EnergyCost;
            GoldCost = CardRNG.Next(45, 56);
            AttackDamage = 9;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            int target = hero.DetermineTarget(encounter);
            hero.Attack(encounter[target], AttackDamage + extraDamage);
            if (hero.DiscardPile.Count > 0)
                PickCard(hero.DiscardPile, "send to the top of your drawpile").MoveCard(hero.DiscardPile, hero.DrawPile);
        }

        public override void UpgradeCard()
        {
            if (!Upgraded) 
                AttackDamage += 3;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Deal {AttackDamage} damage. Place a card from your discard pile on top of your draw pile.";
        }

        public override Card AddCard()
        {
            return new Headbutt();
        }
    }
}
