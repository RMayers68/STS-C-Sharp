
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
            EnergyCost = 0;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(135, 166);
            CardsDrawn = 2;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            hero.DrawCards(CardsDrawn);
            Card thinkingAhead = (PickCard(hero.Hand, "add to the top of your drawpile"));
            thinkingAhead.MoveCard(hero.Hand, hero.DrawPile);
        }

        public override void UpgradeCard()
        {
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Draw 2 cards. Place a card from your hand on top of your draw pile. {(Upgraded ? $"" : " Exhaust.")}";
        }

        public override Card AddCard()
        {
            return new ThinkingAhead();
        }
    }
}