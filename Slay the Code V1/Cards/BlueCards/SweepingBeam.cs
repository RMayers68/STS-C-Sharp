
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
            EnergyCost = 1;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            AttackDamage = 6;
            CardsDrawn = 1;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            foreach (Enemy e in encounter)
                hero.Attack(e, AttackDamage + extraDamage, encounter);
            hero.DrawCards(CardsDrawn);
        }

        public override void UpgradeCard()
        {
            if (!Upgraded)
                AttackDamage += 3 ;
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