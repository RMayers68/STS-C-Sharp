
namespace STV
{
    public class Brilliance : Card
    {
        public Brilliance(bool Upgraded = false)
        {
            Name = "Brilliance";
            Type = "Attack";
            Rarity = "Rare";
            DescriptionModifier = "";
            EnergyCost = 1;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(135, 166);
            AttackDamage = 12;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            foreach (string s in hero.Actions)
                if (s.Contains("Mantra"))
                    extraDamage += Int32.Parse(s.Last().ToString());
            int target = hero.DetermineTarget(encounter);
            hero.Attack(encounter[target], AttackDamage + extraDamage);
        }

        public override void UpgradeCard()
        {
            if (!Upgraded)
                AttackDamage += 4;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Deal {AttackDamage} damage. Deals additional damage equal to Mantra gained this combat.";
        }

        public override Card AddCard()
        {
            return new Brilliance();
        }
    }
}