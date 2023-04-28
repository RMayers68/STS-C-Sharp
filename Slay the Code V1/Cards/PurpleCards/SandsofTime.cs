
namespace STV
{
    public class SandsofTime : Card
    {
        public SandsofTime(bool Upgraded = false)
        {
            Name = "Sands of Time";
            Type = "Attack";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = ;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            AttackDamage = 20;
            AttackLoops = 1;
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
            return DescriptionModifier + $"Retain. Deal {AttackDamage} damage. When Retained, lower its cost by 1 this combat.";
        }

        public override Card AddCard()
        {
            return new SandsofTime();
        }
    }
}