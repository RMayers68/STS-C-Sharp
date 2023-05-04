
namespace STV
{
    public class BowlingBash : Card
    {
        public BowlingBash(bool Upgraded = false)
        {
            Name = "Bowling Bash";
            Type = "Attack";
            Rarity = "Common";
            DescriptionModifier = "";
            EnergyCost = 1;
            if (EnergyCost >= 0)
                TmpEnergyCost = EnergyCost;
            GoldCost = CardRNG.Next(45, 56);
            AttackDamage = 7;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            int target = hero.DetermineTarget(encounter);
            for (int i = 0; i < encounter.Count; i++)
                hero.Attack(encounter[target], AttackDamage + extraDamage);
        }

        public override void UpgradeCard()
        {
            if (!Upgraded)
                AttackDamage += 3 ;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Deal {AttackDamage} damage for each enemy in combat.";
        }

        public override Card AddCard()
        {
            return new BowlingBash();
        }
    }
}