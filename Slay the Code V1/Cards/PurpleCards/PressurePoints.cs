

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
            EnergyCost = 1;
            if (EnergyCost >= 0)
                TmpEnergyCost = EnergyCost;
            GoldCost = CardRNG.Next(45, 56);
            BuffID = 12;
            BuffAmount = 8;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            int target = DetermineTarget(encounter);
            encounter[target].AddBuff(BuffID, BuffAmount,hero);
            foreach (Enemy e in encounter)
                if (e.FindBuff("Mark") is Buff mark && mark != null)
                    hero.NonAttackDamage(e, mark.Intensity, "Pressure Points");
        }

        public override void UpgradeCard()
        {
            if (!Upgraded) 
                BuffAmount += 3;
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