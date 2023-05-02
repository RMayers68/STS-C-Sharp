
namespace STV
{
    public class FlameBarrier : Card
    {
        public FlameBarrier(bool Upgraded = false)
        {
            Name = "Flame Barrier";
            Type = "Skill";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = 2;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(68, 83);
            BlockAmount = 12;
            BuffID = 90;
            BuffAmount = 4;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            hero.CardBlock(BlockAmount, encounter);
            hero.AddBuff(BuffID, BuffAmount);
        }

        public override void UpgradeCard()
        {
            if (!Upgraded)
            {
                BlockAmount += 4;
                BuffAmount += 2;
            }
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Gain {BlockAmount} Block. Whenever you are attacked this turn, deal {BuffAmount} damage to the attacker.";
        }

        public override Card AddCard()
        {
            return new FlameBarrier();
        }
    }
}