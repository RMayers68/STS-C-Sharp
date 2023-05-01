
namespace STV
{
    public class GofortheEyes : Card
    {
        public GofortheEyes(bool Upgraded = false)
        {
            Name = "Go for the Eyes";
            Type = "Attack";
            Rarity = "Common";
            DescriptionModifier = "";
            EnergyCost = ;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            AttackDamage = 3;
            AttackLoops = 1;
            BuffID = 2;
            BuffAmount = 1;
            Targetable = true;
            EnemyBuff = true;
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
            return DescriptionModifier + $"Deal {AttackDamage} damage. If the enemy intends to attack, apply {BuffAmount} Weak.";
        }

        public override Card AddCard()
        {
            return new GofortheEyes();
        }
    }
}