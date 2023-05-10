
namespace STV
{
    public class Pummel : Card
    {
        public Pummel(bool Upgraded = false)
        {
            Name = "Pummel";
            Type = "Attack";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = 1;
            if (EnergyCost >= 0)
                TmpEnergyCost = EnergyCost;
            GoldCost = CardRNG.Next(68, 83);
            AttackDamage = 2;
            MagicNumber = 4;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            int target = DetermineTarget(encounter);
            for (int i = 0; i < MagicNumber; i++)
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
            return DescriptionModifier + $"Deal {AttackDamage} damage {MagicNumber} times. Exhaust.";
        }

        public override Card AddCard()
        {
            return new Pummel();
        }
    }
}