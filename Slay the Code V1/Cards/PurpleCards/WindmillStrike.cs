
namespace STV
{
    public class WindmillStrike : Card
    {
        public WindmillStrike(bool Upgraded = false)
        {
            Name = "Windmill Strike";
            Type = "Attack";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = ;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            AttackDamage = 7;
            AttackLoops = 1;
            MagicNumber = 4;
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
            return DescriptionModifier + $"Retain. Deal {AttackDamage} damage. When Retained, increase its damage by {MagicNumber} this combat.";
        }

        public override Card AddCard()
        {
            return new WindmillStrike();
        }
    }
}