
namespace STV
{
    public class ShrugItOff : Card
    {
        public ShrugItOff(bool Upgraded = false)
        {
            Name = "Shrug It Off";
            Type = "Skill";
            Rarity = "Common";
            DescriptionModifier = "";
            EnergyCost = ;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            BlockAmount = 8;
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
            return DescriptionModifier + $"Gain {BlockAmount} Block. Draw {CardsDrawn} card.";
        }

        public override Card AddCard()
        {
            return new ShrugItOff();
        }
    }
}
