
namespace STV
{
    public class Streamline : Card
    {
        public Streamline(bool Upgraded = false)
        {
            Name = "Streamline";
            Type = "Attack";
            Rarity = "Common";
            DescriptionModifier = "";
            EnergyCost = 2;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            AttackDamage = 15;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            int target = hero.DetermineTarget(encounter);
            hero.Attack(encounter[target], AttackDamage + extraDamage);
            if (EnergyCost != 0)
                SetEnergyCost(EnergyCost-1);
        }

        public override void UpgradeCard()
        {
            if (!Upgraded) 
                AttackDamage += 5;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Deal {AttackDamage} damage. Whenever you play Streamline, reduce its cost by 1 for this combat.";
        }

        public override Card AddCard()
        {
            return new Streamline();
        }
    }
}