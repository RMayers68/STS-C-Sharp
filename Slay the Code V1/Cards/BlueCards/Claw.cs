
namespace STV
{
    public class Claw : Card
    {
        public Claw(bool Upgraded = false)
        {
            Name = "Claw";
            Type = "Attack";
            Rarity = "Common";
            DescriptionModifier = "";
            EnergyCost = ;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            AttackDamage = 3;
            AttackLoops = 1;
            MagicNumber = 2;
            Targetable = true;
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
            return DescriptionModifier + $"Deal {AttackDamage} damage. All Claw cards deal 2 additional damage this combat.";
        }

        public override Card AddCard()
        {
            return new Claw();
        }
    }
}