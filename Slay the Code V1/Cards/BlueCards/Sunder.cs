
namespace STV
{
    public class Sunder : Card
    {
        public Sunder(bool Upgraded = false)
        {
            Name = "Sunder";
            Type = "Attack";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = 3;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(68, 83);
            AttackDamage = 24;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            int target = hero.DetermineTarget(encounter);
            hero.Attack(encounter[target], AttackDamage + extraDamage);
            if (encounter[target].Hp <= 0)
                hero.GainTurnEnergy(3);
        }

        public override void UpgradeCard()
        {
            if (!Upgraded) 
                AttackDamage += 8;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Deal {AttackDamage} damage. If this kills the enemy, gain 3 Energy.";
        }

        public override Card AddCard()
        {
            return new Sunder();
        }
    }
}