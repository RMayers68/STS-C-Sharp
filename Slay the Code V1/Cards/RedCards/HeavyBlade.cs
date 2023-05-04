
namespace STV
{
    public class HeavyBlade : Card
    {
        public HeavyBlade(bool Upgraded = false)
        {
            Name = "Heavy Blade";
            Type = "Attack";
            Rarity = "Common";
            DescriptionModifier = "";
            EnergyCost = 2;
            if (EnergyCost >= 0)
                TmpEnergyCost = EnergyCost;
            GoldCost = CardRNG.Next(45, 56);
            AttackDamage = 14;
            MagicNumber = 2;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            if (hero.FindBuff("Strength") is Buff heavyBlade && heavyBlade != null)
                extraDamage += heavyBlade.Intensity * MagicNumber;
            int target = hero.DetermineTarget(encounter);
            hero.Attack(encounter[target], AttackDamage + extraDamage);
        }

        public override void UpgradeCard()
        {
            if (!Upgraded) 
                MagicNumber += 2;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Deal {AttackDamage} damage. Strength affects Heavy Blade {MagicNumber + 1} times.";
        }

        public override Card AddCard()
        {
            return new HeavyBlade();
        }
    }
}