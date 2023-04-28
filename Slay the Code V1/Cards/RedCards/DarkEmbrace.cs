namespace STV
{
    public class DarkEmbrace : Card
    {
        public DarkEmbrace(bool Upgraded = false)
        {
            Name = "Dark Embrace";
            Type = "Power";
            Rarity = "Rare";
            DescriptionModifier = "";
            EnergyCost = ;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            BuffID = 26;
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
            return DescriptionModifier + $"Whenever a card is Exhausted, draw {BuffAmount} card.";
        }

        public override Card AddCard()
        {
            return new DarkEmbrace();
        }
    }
}
