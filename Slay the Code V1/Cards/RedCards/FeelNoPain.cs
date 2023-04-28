
namespace STV
{
    public class FeelNoPain : Card
    {
        public FeelNoPain(bool Upgraded = false)
        {
            Name = "Feel No Pain";
            Type = "Power";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = ;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            BuffID = 29;
            BuffAmount = 3;
            HeroBuff = true;
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
            return DescriptionModifier + $"Whenever a card is Exhausted, gain {BuffAmount} Block.";
        }

        public override Card AddCard()
        {
            return new FeelNoPain();
        }
    }
}