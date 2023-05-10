
namespace STV
{
    public class Smite : Card
    {
        public Smite(bool Upgraded = false)
        {
            Name = "Smite";
            Type = "Attack";
            Rarity = "Common";
            DescriptionModifier = "";
            EnergyCost = 1;
            if (EnergyCost >= 0)
                TmpEnergyCost = EnergyCost;
            GoldCost = CardRNG.Next(45, 56);
            AttackDamage = 12;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            int target = DetermineTarget(encounter);
            hero.Attack(encounter[target], AttackDamage + extraDamage);

        }

        public override void UpgradeCard()
        {
            if (!Upgraded)
                AttackDamage += 4; ;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Retain. Deal {AttackDamage} damage. Exhaust.";
        }

        public override Card AddCard()
        {
            return new Smite();
        }
    }
}