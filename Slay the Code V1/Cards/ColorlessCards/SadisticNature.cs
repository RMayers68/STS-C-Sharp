
namespace STV
{
    public class SadisticNature : Card
    {
        public SadisticNature(bool Upgraded = false)
        {
            Name = "Sadistic Nature";
            Type = "Power";
            Rarity = "Rare";
            DescriptionModifier = "";
            EnergyCost = 0;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(135, 166);
            BuffID = 87;
            BuffAmount = 5;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            hero.AddBuff(BuffID, BuffAmount);
        }

        public override void UpgradeCard()
        {
            if (!Upgraded) 
                BuffAmount += 2;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Whenever you apply a Debuff to an enemy, they take {BuffAmount} damage.";
        }

        public override Card AddCard()
        {
            return new SadisticNature();
        }
    }
}