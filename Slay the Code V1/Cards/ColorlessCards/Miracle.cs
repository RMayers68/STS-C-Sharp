
namespace STV
{
    public class Miracle : Card
    {
        public Miracle(bool Upgraded = false)
        {
            Name = "Miracle";
            Type = "Skill";
            Rarity = "Common";
            DescriptionModifier = "";
            EnergyCost = 0;
            if (EnergyCost >= 0)
                TmpEnergyCost = EnergyCost;
            GoldCost = CardRNG.Next(45, 56);
            EnergyGained = 1;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            hero.GainTurnEnergy(EnergyGained);
        }

        public override void UpgradeCard()
        {
            if (!Upgraded) 
                EnergyGained++;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Retain. Gain {EnergyGained} Energy. Exhaust.";
        }

        public override Card AddCard()
        {
            return new Miracle();
        }
    }
}