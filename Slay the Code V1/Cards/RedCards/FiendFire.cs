
namespace STV
{
    public class FiendFire : Card
    {
        public FiendFire(bool Upgraded = false)
        {
            Name = "Fiend Fire";
            Type = "Attack";
            Rarity = "Rare";
            DescriptionModifier = "";
            EnergyCost = 2;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(135, 166);
            AttackDamage = 7;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            int fiendFire = 0;
            int target = hero.DetermineTarget(encounter);            
            for (int i = hero.Hand.Count; i >= 1; i--)
            {
                hero.Hand[i - 1].Exhaust(hero, encounter, hero.Hand);
                fiendFire++;
            }
            for (int i = 0;  i < fiendFire; i++)
                hero.Attack(encounter[target], AttackDamage + extraDamage);

        }

        public override void UpgradeCard()
        {
            if (!Upgraded)
                AttackDamage += 3;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Exhaust your hand. Deal {AttackDamage} damage for each Exhausted card. Exhaust.";
        }

        public override Card AddCard()
        {
            return new FiendFire();
        }
    }
}