
namespace STV
{
    public class IronWave : Card
    {
        public IronWave(bool Upgraded = false)
        {
            Name = "Iron Wave";
            Type = "Attack";
            Rarity = "Common";
            DescriptionModifier = "";
            EnergyCost = 1;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            AttackDamage = 5;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            int target = hero.DetermineTarget(encounter);
            hero.Attack(encounter[target], AttackDamage + extraDamage);
            hero.CardBlock(AttackDamage, encounter);
        }

        public override void UpgradeCard()
        {
            if (!Upgraded)
                AttackDamage += 2;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Gain {BlockAmount} Block. Deal {AttackDamage} damage.";
        }

        public override Card AddCard()
        {
            return new IronWave();
        }
    }
}