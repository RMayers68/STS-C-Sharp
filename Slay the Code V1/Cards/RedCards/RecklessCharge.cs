
namespace STV
{
    public class RecklessCharge : Card
    {
        public RecklessCharge(bool Upgraded = false)
        {
            Name = "Reckless Charge";
            Type = "Attack";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = 0;
            if (EnergyCost >= 0)
                TmpEnergyCost = EnergyCost;
            GoldCost = CardRNG.Next(68, 83);
            AttackDamage = 7;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            int target = hero.DetermineTarget(encounter);
            hero.Attack(encounter[target], AttackDamage + extraDamage);
            hero.AddToDrawPile(new Dazed(), true);
        }

        public override void UpgradeCard()
        {
            if (!Upgraded) 
                AttackDamage += 3;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Deal {AttackDamage} damage. Shuffle a Dazed into your draw pile.";
        }

        public override Card AddCard()
        {
            return new RecklessCharge();
        }
    }
}
