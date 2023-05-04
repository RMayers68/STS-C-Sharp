
namespace STV
{
    public class PerfectedStrike : Card
    {
        public PerfectedStrike(bool Upgraded = false)
        {
            Name = "Perfected Strike";
            Type = "Attack";
            Rarity = "Common";
            DescriptionModifier = "";
            EnergyCost = 2;
            if (EnergyCost >= 0)
                TmpEnergyCost = EnergyCost;
            GoldCost = CardRNG.Next(45, 56);
            AttackDamage = 6;
            MagicNumber = 2;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            foreach (Card c in FindAllCardsInCombat(hero, "Strike"))
                extraDamage += MagicNumber;
            int target = hero.DetermineTarget(encounter);
            hero.Attack(encounter[target], AttackDamage + extraDamage);
        }

        public override void UpgradeCard()
        {
            if (!Upgraded) 
                MagicNumber++;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Deal {AttackDamage} damage. Deals additional {MagicNumber} damage for ALL of your cards containing Strike.";
        }

        public override Card AddCard()
        {
            return new PerfectedStrike();
        }
    }
}