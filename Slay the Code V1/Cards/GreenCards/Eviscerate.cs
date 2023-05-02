
namespace STV
{
    public class Eviscerate : Card
    {
        public Eviscerate(bool Upgraded = false)
        {
            Name = "Eviscerate";
            Type = "Attack";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = 3;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(68, 83);
            AttackDamage = 7;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            int target = hero.DetermineTarget(encounter);
            for (int i = 0; i < 3; i++)
                hero.Attack(encounter[target], AttackDamage + extraDamage);
        }

        public override void UpgradeCard()
        {
            if (!Upgraded) 
                AttackDamage += 2;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Costs 1 less Energy for each card discarded this turn. Deal {AttackDamage} damage three times.";
        }

        public override Card AddCard()
        {
            return new Eviscerate();
        }
    }
}