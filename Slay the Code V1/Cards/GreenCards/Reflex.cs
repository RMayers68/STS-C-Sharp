
namespace STV
{
    public class Reflex : Card
    {
        public Reflex(bool Upgraded = false)
        {
            Name = "Reflex";
            Type = "Skill";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = ;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            CardsDrawn = 2;
            if (upgraded)
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
            return DescriptionModifier + $"Unplayable. If this card is discarded from your hand, draw {CardsDrawn} card.";
        }

        public override Card AddCard()
        {
            return new Reflex();
        }
    }
}