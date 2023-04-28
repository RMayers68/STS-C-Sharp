
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
            EnergyCost = ;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            BlockAmount = 3;
            BlockLoops = 1;
            CardsDrawn = 1;
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
            return DescriptionModifier + $"Draw {CardsDrawn} card. If the card is a Skill, gain {BlockAmount} Block.";
        }

        public override Card AddCard()
        {
            return new EscapePlan();
        }
    }
}