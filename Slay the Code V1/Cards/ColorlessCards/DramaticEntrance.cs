
namespace STV
{
    public class DramaticEntrance : Card
    {
        public DramaticEntrance(bool Upgraded = false)
        {
            Name = "Dramatic Entrance";
            Type = "Attack";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = ;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            AttackDamage = 8;
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
            return DescriptionModifier + $"Innate. Deal {AttackDamage} damage to ALL enemies. Exhaust.";
        }

        public override Card AddCard()
        {
            return new DramaticEntrance();
        }
    }
}