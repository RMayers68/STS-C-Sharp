
namespace STV
{
    public class LegSweep : Card
    {
        public LegSweep(bool Upgraded = false)
        {
            Name = "Leg Sweep";
            Type = "Skill";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = ;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            BlockAmount = 11;
            BuffID = 2;
            BuffAmount = 2;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            int target = hero.DetermineTarget(encounter);
            encounter[target].AddBuff(BuffID, BuffAmount);
            hero.CardBlock(BlockAmount, encounter);
        }

        public override void UpgradeCard()
        {
            if (!Upgraded)
            {
                BuffAmount++;
                BlockAmount += 3;
            }
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Apply {BuffAmount} Weak. Gain {BlockAmount} Block.";
        }

        public override Card AddCard()
        {
            return new LegSweep();
        }
    }
}