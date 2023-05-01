
namespace STV
{
    public class CompileDriver : Card
    {
        public CompileDriver(bool Upgraded = false)
        {
            Name = "Compile Driver";
            Type = "Attack";
            Rarity = "Common";
            DescriptionModifier = "";
            EnergyCost = 1;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            AttackDamage = 7;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            int target = hero.DetermineTarget(encounter);
            hero.Attack(encounter[target], AttackDamage + extraDamage);
            foreach (Orb o in hero.Orbs.Distinct())
                hero.DrawCards(1);
        }

        public override void UpgradeCard()
        {
            if (!Upgraded) 
                AttackDamage += 3;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Deal {AttackDamage} damage. Draw 1 card for each unique Orb you have.";
        }

        public override Card AddCard()
        {
            return new CompileDriver();
        }
    }
}