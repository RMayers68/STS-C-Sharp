
namespace STV
{
    public class WindmillStrike : Card
    {
        public WindmillStrike(bool Upgraded = false)
        {
            Name = "Windmill Strike";
            Type = "Attack";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = 2;
            if (EnergyCost >= 0)
                TmpEnergyCost = EnergyCost;
            GoldCost = CardRNG.Next(68, 83);
            AttackDamage = 7;
            MagicNumber = 4;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            int target = hero.DetermineTarget(encounter);
            hero.Attack(encounter[target], AttackDamage + extraDamage);
        }

        public override void UpgradeCard()
        {
            if (!Upgraded)
            {
                AttackDamage += 3;
                MagicNumber++;
            }
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Retain. Deal {AttackDamage} damage. When Retained, increase its damage by {MagicNumber} this combat.";
        }

        public override Card AddCard()
        {
            return new WindmillStrike();
        }
    }
}