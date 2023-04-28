
namespace STV
{
    public class DoubleEnergy : Card
    {
        public DoubleEnergy(bool Upgraded = false)
        {
            Name = "Double Energy";
            Type = "Skill";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = ;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            MagicNumber = 2;
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
            return DescriptionModifier + $"Double your Energy. Exhaust.";
        }

        public override Card AddCard()
        {
            return new DoubleEnergy();
        }
    }
}