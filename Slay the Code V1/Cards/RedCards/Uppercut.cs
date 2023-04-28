
namespace STV
{
    public class Uppercut : Card
    {
        public Uppercut(bool Upgraded = false)
        {
            Name = "Uppercut";
            Type = "Attack";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = ;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            AttackDamage = 13;
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
            return DescriptionModifier + $"Deal {AttackDamage} damage. Apply {BuffAmount} Weak. Apply {BuffAmount} Vulnerable.";
        }

        public override Card AddCard()
        {
            return new Uppercut();
        }
    }
}