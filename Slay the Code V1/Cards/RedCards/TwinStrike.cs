
namespace STV
{
    public class TwinStrike : Card
    {
        public TwinStrike(bool Upgraded = false)
        {
            Name = "Twin Strike";
            Type = "Attack";
            Rarity = "Common";
            DescriptionModifier = "";
            EnergyCost = 1;
            if (EnergyCost >= 0)
                TmpEnergyCost = EnergyCost;
            GoldCost = CardRNG.Next(45, 56);
            AttackDamage = 5;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            int target = DetermineTarget(encounter);
            for (int i = 0; i < 2; i++)
                hero.Attack(encounter[target], AttackDamage + extraDamage);
        }

        public override void UpgradeCard()
        {
            if (!Upgraded) 
                AttackDamage += 2;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Deal {AttackDamage} damage twice.";
        }

        public override Card AddCard()
        {
            return new TwinStrike();
        }
    }
}