
namespace STV
{
    public class PoisonedStab : Card
    {
        public PoisonedStab(bool Upgraded = false)
        {
            Name = "Poisoned Stab";
            Type = "Attack";
            Rarity = "Common";
            DescriptionModifier = "";
            EnergyCost = ;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            AttackDamage = 6;
            AttackLoops = 1;
            BuffID = 39;
            BuffAmount = 3;
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
            return DescriptionModifier + $"Deal {AttackDamage} damage. Apply {BuffAmount} Poison.";
        }

        public override Card AddCard()
        {
            return new PoisonedStab();
        }
    }
}