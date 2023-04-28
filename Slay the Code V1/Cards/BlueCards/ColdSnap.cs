
namespace STV
{
    public class ColdSnap : Card
    {
        public ColdSnap(bool Upgraded = false)
        {
            Name = "Cold Snap";
            Type = "Attack";
            Rarity = "Common";
            DescriptionModifier = "";
            EnergyCost = ;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            AttackDamage = 6;
            AttackLoops = 1;
            BlockLoops = 1;
            MagicNumber = 1;
            Targetable = true;
            SingleAttack = true;
            OrbChannels = true;
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
            return DescriptionModifier + $"Deal {AttackDamage} damage. Channel 1 Frost.";
        }

        public override Card AddCard()
        {
            return new ColdSnap();
        }
    }
}
