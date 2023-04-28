
namespace STV
{
    public class SpotWeakness : Card
    {
        public SpotWeakness(bool Upgraded = false)
        {
            Name = "Spot Weakness";
            Type = "Skill";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = ;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            BuffID = 4;
            BuffAmount = 3;
            HeroBuff = true;
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
            return DescriptionModifier + $"If an enemy intends to attack, gain {BuffAmount} Strength.";
        }

        public override Card AddCard()
        {
            return new SpotWeakness();
        }
    }
}