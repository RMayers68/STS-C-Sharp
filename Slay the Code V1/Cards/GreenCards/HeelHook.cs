
namespace STV
{
    public class HeelHook : Card
    {
        public HeelHook(bool Upgraded = false)
        {
            Name = "Heel Hook";
            Type = "Attack";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = 1;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(68, 83);
            AttackDamage = 5;
            CardsDrawn = 1;
            EnergyGained = 1;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            int target = hero.DetermineTarget(encounter);
            hero.Attack(encounter[target], AttackDamage + extraDamage);
            if (encounter[target].HasBuff("Weak"))
            {
                hero.GainTurnEnergy(1);
                hero.DrawCards(CardsDrawn);
            }
        }

        public override void UpgradeCard()
        {
            if (!Upgraded) 
                AttackDamage += 3;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Deal {AttackDamage} damage. If the enemy is Weak, Gain 1 Energy and draw 1 card.";
        }

        public override Card AddCard()
        {
            return new HeelHook();
        }
    }
}