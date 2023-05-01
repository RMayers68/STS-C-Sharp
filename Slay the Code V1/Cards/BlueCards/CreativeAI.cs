
namespace STV
{
    public class CreativeAI : Card
    {
        public CreativeAI(bool Upgraded = false)
        {
            Name = "Creative AI";
            Type = "Power";
            Rarity = "Rare";
            DescriptionModifier = "";
            EnergyCost = ;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            BuffID = 58;
            BuffAmount = 1;
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
            return DescriptionModifier + $"At the start of each turn, add a random Power card to your hand.";
        }

        public override Card AddCard()
        {
            return new CreativeAI();
        }
    }
}
