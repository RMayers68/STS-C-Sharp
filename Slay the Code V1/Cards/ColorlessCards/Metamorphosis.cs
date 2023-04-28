
namespace STV
{
    public class Metamorphosis : Card
    {
        public Metamorphosis(bool Upgraded = false)
        {
            Name = "Metamorphosis";
            Type = "Skill";
            Rarity = "Rare";
            DescriptionModifier = "";
            EnergyCost = ;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            MagicNumber = 3;
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
            return DescriptionModifier + $"Add {MagicNumber} random Attacks into your Draw Pile. They cost 0 this combat. Exhaust.";
        }

        public override Card AddCard()
        {
            return new Metamorphosis();
        }
    }
}