
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
            BlockLoops = 1;
            BuffID = 2;
            BuffAmount = 2;
            Targetable = true;
            EnemyBuff = true;
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
            return DescriptionModifier + $"Apply {BuffAmount} Weak. Gain {BlockAmount} Block.";
        }

        public override Card AddCard()
        {
            return new LegSweep();
        }
    }
}