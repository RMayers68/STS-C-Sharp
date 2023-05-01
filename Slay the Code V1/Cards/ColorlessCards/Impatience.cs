
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
            EnergyCost = ;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            CardsDrawn = 2;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
        }

        public override void UpgradeCard()
        {
            if (!Upgraded) ;
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
