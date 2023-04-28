
namespace STV
{
    public class Sunder : Card
    {
        public Sunder(bool Upgraded = false)
        {
            Name = "Sunder";
            Type = "Attack";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = ;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            AttackDamage = 24;
            AttackLoops = 1;
            EnergyGained = 3;
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
            return DescriptionModifier + $"Deal {AttackDamage} damage. If this kills the enemy, gain 3 Energy.";
        }

        public override Card AddCard()
        {
            return new Sunder();
        }
    }
}