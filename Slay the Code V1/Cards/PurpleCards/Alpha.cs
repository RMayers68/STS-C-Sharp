
namespace STV
{
    public class Alpha : Card
    {
        public Alpha(bool Upgraded = false)
        {
            Name = "Alpha";
            Type = "Skill";
            Rarity = "Rare";
            DescriptionModifier = "";
            EnergyCost = 1;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(135, 166);
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            hero.AddToDrawPile(new Beta(), true);
        }

        public override void UpgradeCard()
        {
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"{(Upgraded ? $"Innate. " : "")} Shuffle a Beta into your draw pile. Exhaust.";
        }

        public override Card AddCard()
        {
            return new Alpha();
        }
    }
}