
namespace STV
{
    public class SashWhip : Card
    {
        public SashWhip(bool Upgraded = false)
        {
            Name = "Sash Whip";
            Type = "Attack";
            Rarity = "Common";
            DescriptionModifier = "";
            EnergyCost = ;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            AttackDamage = 8;
            AttackLoops = 1;
            BuffID = 2;
            BuffAmount = 1;
            Targetable = true;
            EnemyBuff = true;
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
            return DescriptionModifier + $"Deal {AttackDamage} damage. If the last card played this combat was an Attack, apply {BuffAmount} Weak.";
        }

        public override Card AddCard()
        {
            return new SashWhip();
        }
    }
}