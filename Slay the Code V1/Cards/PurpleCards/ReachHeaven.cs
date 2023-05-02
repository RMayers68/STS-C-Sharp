
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
            EnergyCost = 2;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(68, 83);
            AttackDamage = 10;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            int target = hero.DetermineTarget(encounter);
            hero.Attack(encounter[target], AttackDamage + extraDamage);
            hero.AddToDrawPile(new ThroughViolence(),true);
        }

        public override void UpgradeCard()
        {
            if (!Upgraded) 
                AttackDamage += 5;
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