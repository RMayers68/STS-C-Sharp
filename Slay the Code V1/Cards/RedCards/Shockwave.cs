
namespace STV
{
    public class Shockwave : Card
    {
        public Shockwave(bool Upgraded = false)
        {
            Name = "Shockwave";
            Type = "Skill";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = ;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            BuffID = 2;
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
            return DescriptionModifier + $"Apply {BuffAmount} Weak and Vulnerable to ALL enemies. Exhaust.";
        }

        public override Card AddCard()
        {
            return new Shockwave();
        }
    }
}