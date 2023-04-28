
namespace STV
{
    public class ThinkingAhead : Card
    {
        public ThinkingAhead(bool Upgraded = false)
        {
            Name = "Thinking Ahead";
            Type = "Skill";
            Rarity = "Rare";
            DescriptionModifier = "";
            EnergyCost = ;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            CardsDrawn = 2;
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
            return DescriptionModifier + $"Draw 2 cards. Place a card from your hand on top of your draw pile. {(Upgraded ? ";
                }

                public override Card AddCard()
                {
                        return new ThinkingAhead();
                }
        }
}