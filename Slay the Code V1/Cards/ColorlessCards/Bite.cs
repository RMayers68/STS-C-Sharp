
namespace STV
{
    public class Bite : Card
    {
        public Bite(bool Upgraded = false)
        {
            Name = "Bite";
            Type = "Attack";
            Rarity = "Common";
            DescriptionModifier = "";
            EnergyCost = 1;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            AttackDamage = 7;
            MagicNumber = 2;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            int target = hero.DetermineTarget(encounter);
            hero.Attack(encounter[target], AttackDamage + extraDamage);
            hero.CombatHeal(MagicNumber);
        }

        public override void UpgradeCard()
        {
            if (!Upgraded)
            {
                AttackDamage++;
                MagicNumber++;
            }
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Deal {AttackDamage} damage. Heal {MagicNumber} HP.";
        }

        public override Card AddCard()
        {
            return new Bite();
        }
    }
}