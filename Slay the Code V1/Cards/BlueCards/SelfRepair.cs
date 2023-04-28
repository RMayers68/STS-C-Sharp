
namespace STV
{
    public class SelfRepair : Card
    {
        public SelfRepair(bool Upgraded = false)
        {
            Name = "Self Repair";
            Type = "Power";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = ;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            BuffID = 65;
            BuffAmount = 7;
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
            return DescriptionModifier + $"At the end of combat, heal {BuffAmount} HP.";
        }

        public override Card AddCard()
        {
            return new SelfRepair();
        }
    }
}
