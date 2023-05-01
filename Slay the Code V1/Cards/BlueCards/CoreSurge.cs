
namespace STV
{
    public class CoreSurge : Card
    {
        public CoreSurge(bool Upgraded = false)
        {
            Name = "Core Surge";
            Type = "Attack";
            Rarity = "Rare";
            DescriptionModifier = "";
            EnergyCost = ;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            AttackDamage = 11;
            AttackLoops = 1;
            BuffID = 8;
            BuffAmount = 1;
            Targetable = true;
            HeroBuff = true;
            SingleAttack = true;
            if (Upgraded)
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
            return DescriptionModifier + $"Deal {AttackDamage} damage. Gain 1 Artifact. Exhaust.";
        }

        public override Card AddCard()
        {
            return new CoreSurge();
        }
    }
}