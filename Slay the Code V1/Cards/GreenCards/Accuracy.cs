
namespace STV
{
    public class Accuracy : Card
    {
        public Accuracy(bool Upgraded = false)
        {
            Name = "Accuracy";
            Type = "Power";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = ;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            BuffID = 36;
            BuffAmount = 4;
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
            return DescriptionModifier + $"Shivs deal {BuffAmount} additional damage.";
        }

        public override Card AddCard()
        {
            return new Accuracy();
        }
    }
}
