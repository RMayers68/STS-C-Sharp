
namespace STV
{
    public class Tranquility : Card
    {
        public Tranquility(bool Upgraded = false)
        {
            Name = "Tranquility";
            Type = "Skill";
            Rarity = "Common";
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
            return DescriptionModifier + $"Retain. Enter Calm. Exhaust.";
        }

        public override Card AddCard()
        {
            return new Tranquility();
        }
    }
}