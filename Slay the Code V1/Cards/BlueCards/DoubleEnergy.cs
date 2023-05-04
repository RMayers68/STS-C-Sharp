
namespace STV
{
    public class DoubleEnergy : Card
    {
        public DoubleEnergy(bool Upgraded = false)
        {
            Name = "Double Energy";
            Type = "Skill";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = 1;
            if (EnergyCost >= 0)
                TmpEnergyCost = EnergyCost;
            GoldCost = CardRNG.Next(68, 83);
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            hero.GainTurnEnergy(hero.Energy);
        }

        public override void UpgradeCard()
        {
            if (!Upgraded) EnergyCost--;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Double your Energy. Exhaust.";
        }

        public override Card AddCard()
        {
            return new DoubleEnergy();
        }
    }
}