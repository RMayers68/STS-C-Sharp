
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
            EnergyCost = 2;
            if (EnergyCost >= 0)
                TmpEnergyCost = EnergyCost;
            GoldCost = CardRNG.Next(68, 83);
            BlockAmount = 11;
            BuffID = 2;
            BuffAmount = 2;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            int target = DetermineTarget(encounter);
            encounter[target].AddBuff(BuffID, BuffAmount,hero);
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