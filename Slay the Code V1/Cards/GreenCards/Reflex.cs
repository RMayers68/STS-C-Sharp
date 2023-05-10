
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
            EnergyCost = -2;
            if (EnergyCost >= 0)
                TmpEnergyCost = EnergyCost;
            GoldCost = CardRNG.Next(68, 83);
            CardsDrawn = 2;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            return;
        }

        public override void UpgradeCard()
        {
            if (!Upgraded) CardsDrawn++;
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