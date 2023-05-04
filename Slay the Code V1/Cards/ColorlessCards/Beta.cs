
namespace STV
{
    public class Beta : Card
    {
        public Beta(bool Upgraded = false)
        {
            Name = "Beta";
            Type = "Skill";
            Rarity = "Common";
            DescriptionModifier = "";
            EnergyCost = 2;
            if (EnergyCost >= 0)
                TmpEnergyCost = EnergyCost;
            GoldCost = CardRNG.Next(45, 56);
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            hero.AddToDrawPile(new Omega(), true);
        }

        public override void UpgradeCard()
        {
            if (!Upgraded) 
                EnergyCost--;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Shuffle an Omega into your draw pile. Exhaust.";
        }

        public override Card AddCard()
        {
            return new Beta();
        }
    }
}