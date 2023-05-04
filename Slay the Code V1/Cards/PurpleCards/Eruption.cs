
namespace STV
{
    public class Eruption : Card
    {
        public Eruption(bool Upgraded = false)
        {
            Name = "Eruption";
            Type = "Attack";
            Rarity = "Basic";
            DescriptionModifier = "";
            EnergyCost = 2;
            if (EnergyCost >= 0)
                TmpEnergyCost = EnergyCost;
            GoldCost = 0;
            AttackDamage = 9;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            int target = hero.DetermineTarget(encounter);
            hero.Attack(encounter[target], AttackDamage + extraDamage);
            hero.SwitchStance("Wrath");
        }

        public override void UpgradeCard()
        {
            if (!Upgraded) 
                EnergyCost--;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Deal {AttackDamage} damage. Enter Wrath.";
        }

        public override Card AddCard()
        {
            return new Eruption();
        }
    }
}