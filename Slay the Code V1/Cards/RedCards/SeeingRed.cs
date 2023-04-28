
namespace STV
{
    public class SeeingRed : Card
    {
        public SeeingRed(bool Upgraded = false)
        {
            Name = "Seeing Red";
            Type = "Skill";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = ;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            EnergyGained = 2;
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
            return DescriptionModifier + $"Gain {EnergyGained} Energy. Exhaust.";
        }

        public override Card AddCard()
        {
            return new SeeingRed();
        }
    }
}