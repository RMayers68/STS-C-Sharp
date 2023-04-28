
namespace STV
{
    public class Concentrate : Card
    {
        public Concentrate(bool Upgraded = false)
        {
            Name = "Concentrate";
            Type = "Skill";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = ;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            MagicNumber = 3;
            EnergyGained = 2;
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
            return DescriptionModifier + $"Discard {MagicNumber} cards. Gain {EnergyGained} Energy.";
        }

        public override Card AddCard()
        {
            return new Concentrate();
        }
    }
}