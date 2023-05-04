
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
            EnergyCost = 1;
            if (EnergyCost >= 0)
                TmpEnergyCost = EnergyCost;
            GoldCost = CardRNG.Next(68, 83);
            BlockAmount = 5;
            EnergyGained = 2;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            hero.CardBlock(BlockAmount, encounter);
        }

        public override void UpgradeCard()
        {
            if (!Upgraded)
            {
                BlockAmount += 3;
                EnergyGained++;
            }
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