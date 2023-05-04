
namespace STV
{
    public class Impatience : Card
    {
        public Impatience(bool Upgraded = false)
        {
            Name = "Impatience";
            Type = "Skill";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = 0;
            if (EnergyCost >= 0)
                TmpEnergyCost = EnergyCost;
            GoldCost = CardRNG.Next(68, 83);
            CardsDrawn = 2;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            if (!hero.Hand.Any(x => x.Type == "Attack"))
                hero.DrawCards(CardsDrawn);
        }

        public override void UpgradeCard()
        {
            if (!Upgraded) 
                CardsDrawn++;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"If you have no Attack cards in your hand, draw {CardsDrawn} cards.";
        }

        public override Card AddCard()
        {
            return new Impatience();
        }
    }
}
