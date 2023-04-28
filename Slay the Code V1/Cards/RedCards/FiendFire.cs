
namespace STV
{
    public class FiendFire : Card
    {
        public FiendFire(bool Upgraded = false)
        {
            Name = "Fiend Fire";
            Type = "Attack";
            Rarity = "Rare";
            DescriptionModifier = "";
            EnergyCost = ;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            AttackDamage = 7;
            Targetable = true;
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
            return DescriptionModifier + $"Exhaust your hand. Deal {AttackDamage} damage for each Exhausted card. Exhaust.";
        }

        public override Card AddCard()
        {
            return new FiendFire();
        }
    }
}