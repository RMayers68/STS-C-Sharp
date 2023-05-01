

namespace STV
{
    public class PressurePoints : Card
    {
        public PressurePoints(bool Upgraded = false)
        {
            Name = "Pressure Points";
            Type = "Skill";
            Rarity = "Common";
            DescriptionModifier = "";
            EnergyCost = ;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            BuffID = 12;
            BuffAmount = 8;
            Targetable = true;
            EnemyBuff = true;
            if (Upgraded)
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
            return DescriptionModifier + $"Apply {BuffAmount} Mark. ALL enemies lose HP equal to their Mark.";
        }

        public override Card AddCard()
        {
            return new PressurePoints();
        }
    }
}