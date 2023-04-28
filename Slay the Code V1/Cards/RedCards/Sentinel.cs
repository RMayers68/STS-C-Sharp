
namespace STV
{
    public class Sentinel : Card
    {
        public Sentinel(bool Upgraded = false)
        {
            Name = "Sentinel";
            Type = "Skill";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = ;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            BlockAmount = 5;
            BlockLoops = 1;
            EnergyGained = 2;
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
            return DescriptionModifier + $"Gain {BlockAmount} Block. If this card is Exhausted, gain {EnergyGained} Energy.";
        }

        public override Card AddCard()
        {
            return new Sentinel();
        }
    }
}