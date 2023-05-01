
namespace STV
{
    public class RipandTear : Card
    {
        public RipandTear(bool Upgraded = false)
        {
            Name = "Rip and Tear";
            Type = "Attack";
            Rarity = "Uncommon";
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
            for (int i = 0; i < 2; i++)
                hero.Attack(encounter[CardRNG.Next(0, encounter.Count)], AttackDamage + extraDamage);
        }

        public override void UpgradeCard()
        {
            if (!Upgraded) 
                AttackDamage += 2 ;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Deal {AttackDamage} damage to a random enemy 2 times.";
        }

        public override Card AddCard()
        {
            return new RipandTear();
        }
    }
}