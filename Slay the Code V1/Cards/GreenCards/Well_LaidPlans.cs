namespace STV
{
    public class Well_LaidPlans : Card
    {
        public Well_LaidPlans(bool Upgraded = false)
        {
        Name = "Well-Laid Plans";
        Type = "Power";
        Rarity = "Uncommon";
        DescriptionModifier = "";
        EnergyCost = ;
        if (EnergyCost >= 0)
            SetTmpEnergyCost(EnergyCost);
        GoldCost = CardRNG.Next(45, 56);
        BuffID = 51;
        BuffAmount = 1;
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
            return DescriptionModifier + $"At the end of your turn, Retain up to {BuffAmount} card.";
        }

        public override Card AddCard()
        {
            return new Well_LaidPlans();
        }
    }
}