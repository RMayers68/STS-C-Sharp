
namespace STV
{
    public class BouncingFlask : Card
    {
        public BouncingFlask(bool Upgraded = false)
        {
            Name = "Bouncing Flask";
            Type = "Skill";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = ;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            MagicNumber = 3;
            BuffID = 39;
            BuffAmount = 3;
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
            return DescriptionModifier + $"Apply {BuffAmount} Poison to a random enemy {MagicNumber} times.";
        }

        public override Card AddCard()
        {
            return new BouncingFlask();
        }
    }
}