
namespace STV
{
    public class SneakyStrike : Card
    {
        public SneakyStrike(bool Upgraded = false)
        {
            Name = "Sneaky Strike";
            Type = "Attack";
            Rarity = "Common";
            DescriptionModifier = "";
            EnergyCost = 2;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            AttackDamage = 12;
            EnergyGained = 2;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            int target = hero.DetermineTarget(encounter);
            hero.Attack(encounter[target], AttackDamage + extraDamage);
            if (hero.FindTurnActions(turnNumber, "Discard").Count > 0)
                hero.GainTurnEnergy(EnergyGained);
        }

        public override void UpgradeCard()
        {
            if (!Upgraded) ;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Deal {AttackDamage} damage. If you have discarded a card this turn, gain 2 Energy.";
        }

        public override Card AddCard()
        {
            return new SneakyStrike();
        }
    }
}