namespace STV
{
    public class WellLaidPlans : Card
    {
        public WellLaidPlans(bool Upgraded = false)
        {
        Name = "Well-Laid Plans";
        Type = "Power";
        Rarity = "Uncommon";
        DescriptionModifier = "";
        EnergyCost = 1;
        if (EnergyCost >= 0)
            SetTmpEnergyCost(EnergyCost);
        GoldCost = CardRNG.Next(45, 56);
        BuffID = 51;
        BuffAmount = 1;
        if (Upgraded)
            UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            hero.AddBuff(BuffID, BuffAmount);
        }

        public override void UpgradeCard()
        {
            if (!Upgraded) 
                BuffAmount++;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"At the end of your turn, Retain up to {BuffAmount} card.";
        }

        public override Card AddCard()
        {
            return new WellLaidPlans();
        }
    }
}