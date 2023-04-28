
namespace STV
{
    public class CalculatedGamble : Card
    {
        public CalculatedGamble(bool Upgraded = false)
        {
            Name = "Calculated Gamble";
            Type = "Skill";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = ;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            SelfDamage = true;
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
            return DescriptionModifier + $"Discard your hand, then draw that many cards. Exhaust.";
        }

        public override Card AddCard()
        {
            return new CalculatedGamble();
        }
    }
}