
namespace STV
{
    public class Tactician : Card
    {
        public Tactician(bool Upgraded = false)
        {
            Name = "Tactician";
            Type = "Skill";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = -2;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            EnergyGained = 1;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            return;
        }

        public override void UpgradeCard()
        {
            if (!Upgraded) 
                EnergyGained++;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Unplayable. If this card is discarded from your hand, gain {EnergyGained} Energy.";
        }

        public override Card AddCard()
        {
            return new Tactician();
        }
    }
}