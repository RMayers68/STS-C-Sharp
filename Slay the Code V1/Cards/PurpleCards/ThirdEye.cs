
namespace STV
{
    public class ThirdEye : Card
    {
        public ThirdEye(bool Upgraded = false)
        {
            Name = "Third Eye";
            Type = "Skill";
            Rarity = "Common";
            DescriptionModifier = "";
            EnergyCost = 1;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            BlockAmount = 7;
            MagicNumber = 3;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            hero.CardBlock(BlockAmount);
            Scry(hero, MagicNumber);
        }

        public override void UpgradeCard()
        {
            if (!Upgraded)
            {
                BlockAmount += 2;
                MagicNumber += 2;
            }
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Gain {BlockAmount} Block. Scry {MagicNumber}.";
        }

        public override Card AddCard()
        {
            return new ThirdEye();
        }
    }
}