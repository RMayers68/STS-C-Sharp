
namespace STV
{
    public class CripplingCloud : Card
    {
        public CripplingCloud(bool Upgraded = false)
        {
            Name = "Crippling Cloud";
            Type = "Skill";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = ;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            BuffID = 39;
            BuffAmount = 4;
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
            return DescriptionModifier + $"Apply {BuffAmount} Poison and 2 Weak to ALL enemies. Exhaust.";
        }

        public override Card AddCard()
        {
            return new CripplingCloud();
        }
    }
}