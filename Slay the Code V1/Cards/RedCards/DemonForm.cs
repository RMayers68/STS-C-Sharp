namespace STV
{
    public class DemonForm : Card
    {
        public DemonForm(bool Upgraded = false)
        {
            Name = "Demon Form";
            Type = "Power";
            Rarity = "Rare";
            DescriptionModifier = "";
            EnergyCost = ;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            BuffID = 3;
            BuffAmount = 2;
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
            return DescriptionModifier + $"At the start of each turn, gain {BuffAmount} Strength.";
        }

        public override Card AddCard()
        {
            return new DemonForm();
        }
    }
}
