
namespace STV
{
    public class FlyingKnee : Card
    {
        public FlyingKnee(bool Upgraded = false)
        {
            Name = "Flying Knee";
            Type = "Attack";
            Rarity = "Common";
            DescriptionModifier = "";
            EnergyCost = ;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            AttackDamage = 8;
            AttackLoops = 1;
            BuffID = 22;
            BuffAmount = 1;
            Targetable = true;
            HeroBuff = true;
            SingleAttack = true;
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
            return DescriptionModifier + $"Deal {AttackDamage} damage. Next turn gain {EnergyGained} Energy.";
        }

        public override Card AddCard()
        {
            return new FlyingKnee();
        }
    }
}
