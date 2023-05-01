
namespace STV
{
    public class EscapePlan : Card
    {
        public EscapePlan(bool Upgraded = false)
        {
            Name = "Escape Plan";
            Type = "Skill";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = 0;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            BlockAmount = 3;
            CardsDrawn = 1;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            int count = hero.Hand.Count;
            hero.DrawCards(CardsDrawn);
            if (count < 10)
                if (hero.Hand.Last().Type == "Skill")
                    hero.CardBlock(BlockAmount, encounter);

        }

        public override void UpgradeCard()
        {
            if (!Upgraded)
                BlockAmount += 2;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Draw {CardsDrawn} card. If the card is a Skill, gain {BlockAmount} Block.";
        }

        public override Card AddCard()
        {
            return new EscapePlan();
        }
    }
}