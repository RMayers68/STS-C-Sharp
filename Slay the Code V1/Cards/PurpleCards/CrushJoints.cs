namespace STV
{
    public class CrushJoints : Card
    {
        public CrushJoints(bool Upgraded = false)
        {
            Name = "Crush Joints";
            Type = "Attack";
            Rarity = "Common";
            DescriptionModifier = "";
            EnergyCost = ;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            AttackDamage = 8;
            AttackLoops = 1;
            BuffID = 1;
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
            return DescriptionModifier + $"Deal {AttackDamage} damage. If the last card played this combat was a Skill, apply {BuffAmount} Vulnerable.";
        }

        public override Card AddCard()
        {
            return new CrushJoints();
        }
    }
}