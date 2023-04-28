
namespace STV
{
    public class Reaper : Card
    {
        public Reaper(bool Upgraded = false)
        {
            Name = "Reaper";
            Type = "Attack";
            Rarity = "Rare";
            DescriptionModifier = "";
            EnergyCost = ;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            AttackDamage = 4;
            AttackLoops = 1;
            AttackAll = true;
            if (upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
        }

        public override void UpgradeCard()
        {
            if (!Upgraded) ;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Deal {AttackDamage} damage to ALL enemies. Heal for unblocked damage. Exhaust.";
        }

        public override Card AddCard()
        {
            return new Reaper();
        }
    }
}
