
namespace STV
{
    public class TURBO : Card
    {
        public TURBO(bool Upgraded = false)
        {
            Name = "TURBO";
            Type = "Skill";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = 0;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(68, 83);
            EnergyGained = 2;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            hero.GainTurnEnergy(EnergyGained);
            hero.AddToDiscard(new Void());
        }

        public override void UpgradeCard()
        {
            if (!Upgraded) 
                EnergyGained++;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Gain {EnergyGained} Energy. Add a Void into your discard pile.";
        }

        public override Card AddCard()
        {
            return new TURBO();
        }
    }
}