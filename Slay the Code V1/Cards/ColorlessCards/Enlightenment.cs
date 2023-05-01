
namespace STV
{
    public class Enlightenment : Card
    {
        public Enlightenment(bool Upgraded = false)
        {
            Name = "Enlightenment";
            Type = "Skill";
            Rarity = "Rare";
            DescriptionModifier = "";
            EnergyCost = ;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
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
            return DescriptionModifier + $"Reduce the cost of cards in your hand to 1 this turn.";
        }

        public override Card AddCard()
        {
            return new Enlightenment();
        }
    }
}
