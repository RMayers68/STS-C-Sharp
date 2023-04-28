
namespace STV
{
    public class Aggregate : Card
    {
        public Aggregate(bool Upgraded = false)
        {
            Name = "Aggregate";
            Type = "Skill";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = ;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            MagicNumber = 4;
            EnergyGained = 1;
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
            return DescriptionModifier + $"Gain 1 Energy for every {MagicNumber} cards in your draw pile.";
        }

        public override Card AddCard()
        {
            return new Aggregate();
        }
    }
}
