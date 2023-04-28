
namespace STV
{
    public class SweepingBeam : Card
    {
        public SweepingBeam(bool Upgraded = false)
        {
            Name = "Sweeping Beam";
            Type = "Attack";
            Rarity = "Common";
            DescriptionModifier = "";
            EnergyCost = ;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            AttackDamage = 6;
            AttackLoops = 1;
            CardsDrawn = 1;
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
            return DescriptionModifier + $"Deal {AttackDamage} damage to ALL enemies. Draw 1 card.";
        }

        public override Card AddCard()
        {
            return new SweepingBeam();
        }
    }
}