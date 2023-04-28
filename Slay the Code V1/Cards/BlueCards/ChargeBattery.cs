
namespace STV
{
    public class ChargeBattery : Card
    {
        public ChargeBattery(bool Upgraded = false)
        {
            Name = "Charge Battery";
            Type = "Skill";
            Rarity = "Common";
            DescriptionModifier = "";
            EnergyCost = ;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            BlockAmount = 7;
            BlockLoops = 1;
            BuffID = 22;
            BuffAmount = 1;
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
            return DescriptionModifier + $"Gain {BlockAmount} Block. Next turn gain 1 Energy.";
        }

        public override Card AddCard()
        {
            return new ChargeBattery();
        }
    }
}