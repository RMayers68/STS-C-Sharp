
namespace STV
{
    public class ReachHeaven : Card
    {
        public ReachHeaven(bool Upgraded = false)
        {
            Name = "Reach Heaven";
            Type = "Attack";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = ;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            AttackDamage = 10;
            AttackLoops = 1;
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
            return DescriptionModifier + $"Deal {AttackDamage} damage. Shuffle a Through Violence into your draw pile.";
        }

        public override Card AddCard()
        {
            return new ReachHeaven();
        }
    }
}