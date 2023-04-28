
namespace STV
{
    public class Apotheosis : Card
    {
        public Apotheosis(bool Upgraded = false)
        {
            Name = "Apotheosis";
            Type = "Skill";
            Rarity = "Rare";
            DescriptionModifier = "";
            EnergyCost = ;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
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
            return DescriptionModifier + $"Upgrade ALL of your cards for the rest of combat. Exhaust.";
        }

        public override Card AddCard()
        {
            return new Apotheosis();
        }
    }
}