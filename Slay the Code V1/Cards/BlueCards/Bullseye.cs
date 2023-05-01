
namespace STV
{
    public class Bullseye : Card
    {
        public Bullseye(bool Upgraded = false)
        {
            Name = "Bullseye";
            Type = "Attack";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = ;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            AttackDamage = 8;
            AttackLoops = 1;
            BuffID = 57;
            BuffAmount = 2;
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
            return DescriptionModifier + $"Deal {AttackDamage} damage. Apply {BuffAmount} Lock-On.";
        }

        public override Card AddCard()
        {
            return new Bullseye();
        }
    }
}