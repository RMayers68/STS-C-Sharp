
namespace STV
{
    public class PoisonedStab : Card
    {
        public PoisonedStab(bool Upgraded = false)
        {
            Name = "Poisoned Stab";
            Type = "Attack";
            Rarity = "Common";
            DescriptionModifier = "";
            EnergyCost = 1;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            AttackDamage = 6;
            BuffID = 39;
            BuffAmount = 3;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            int target = hero.DetermineTarget(encounter);
            hero.Attack(encounter[target], AttackDamage + extraDamage);
            encounter[target].AddBuff(BuffID, BuffAmount,hero);
        }

        public override void UpgradeCard()
        {
            if (!Upgraded)
            {
                AttackDamage += 2;
                BuffAmount++;
            }
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Deal {AttackDamage} damage. Apply {BuffAmount} Poison.";
        }

        public override Card AddCard()
        {
            return new PoisonedStab();
        }
    }
}