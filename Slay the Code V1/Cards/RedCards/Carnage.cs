
namespace STV
{
    public class Carnage : Card
    {
        public Carnage(bool Upgraded = false)
        {
            Name = "Carnage";
            Type = "Attack";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = 2;
            if (EnergyCost >= 0)
                TmpEnergyCost = EnergyCost;
            GoldCost = CardRNG.Next(68, 83);
            AttackDamage = 20;
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
                AttackDamage += 8;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Ethereal. Deal {AttackDamage} damage.";
        }

        public override Card AddCard()
        {
            return new Carnage();
        }
    }
}
