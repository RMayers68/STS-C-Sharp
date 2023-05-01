
namespace STV
{
    public class FlurryOfBlows : Card
    {
        public FlurryOfBlows(bool Upgraded = false)
        {
            Name = "Flurry Of Blows";
            Type = "Attack";
            Rarity = "Common";
            DescriptionModifier = "";
            EnergyCost = ;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            AttackDamage = 4;
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
            return DescriptionModifier + $"Deal {AttackDamage} damage. Whenever you change stances, return this from the discard pile to your hand.";
        }

        public override Card AddCard()
        {
            return new FlurryOfBlows();
        }
    }
}
