
namespace STV
{
    public class HeelHook : Card
    {
        public HeelHook(bool Upgraded = false)
        {
            Name = "Heel Hook";
            Type = "Attack";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = ;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            AttackDamage = 5;
            AttackLoops = 1;
            CardsDrawn = 1;
            EnergyGained = 1;
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
            return DescriptionModifier + $"Deal {AttackDamage} damage. If the enemy is Weak, Gain 1 Energy and draw 1 card.";
        }

        public override Card AddCard()
        {
            return new HeelHook();
        }
    }
}