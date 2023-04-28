
namespace STV
{
    public class BecomeAlmighty : Card
    {
        public BecomeAlmighty(bool Upgraded = false)
        {
            Name = "Become Almighty";
            Type = "Power";
            Rarity = "Common";
            DescriptionModifier = "";
            EnergyCost = 0;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            BuffID = 4;
            BuffAmount = 3;
            HeroBuff = true;
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
            return DescriptionModifier + $"Gain {BuffAmount} Strength.";
        }

        public override Card AddCard()
        {
            return new BecomeAlmighty();
        }
    }
}