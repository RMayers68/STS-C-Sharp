
namespace STV
{
    public class Skewer : Card
    {
        public Skewer(bool Upgraded = false)
        {
            Name = "Skewer";
            Type = "Attack";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = -1;
            if (EnergyCost >= 0)
                TmpEnergyCost = EnergyCost;
            GoldCost = CardRNG.Next(68, 83);
            AttackDamage = 7;      
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            int target = DetermineTarget(encounter);
            for (int i = 0; i < hero.Energy; i++) 
                hero.Attack(encounter[target], AttackDamage + extraDamage);
            hero.Energy = 0;
        }

        public override void UpgradeCard()
        {
            if (!Upgraded) 
                AttackDamage += 3;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Deal {AttackDamage} damage X times.";
        }

        public override Card AddCard()
        {
            return new Skewer();
        }
    }
}