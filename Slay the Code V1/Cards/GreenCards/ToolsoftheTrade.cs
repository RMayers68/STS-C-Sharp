
namespace STV
{
    public class ToolsoftheTrade : Card
    {
        public ToolsoftheTrade(bool Upgraded = false)
        {
            Name = "Tools of the Trade";
            Type = "Power";
            Rarity = "Rare";
            DescriptionModifier = "";
            EnergyCost = ;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            BuffID = 50;
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
            return DescriptionModifier + $"At the start of your turn, draw 1 card and discard 1 card.";
        }

        public override Card AddCard()
        {
            return new ToolsoftheTrade();
        }
    }
}