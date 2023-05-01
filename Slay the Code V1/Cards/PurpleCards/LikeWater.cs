
namespace STV
{
    public class LikeWater : Card
    {
        public LikeWater(bool Upgraded = false)
        {
            Name = "Like Water";
            Type = "Power";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = ;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            BuffID = 77;
            BuffAmount = 5;
            HeroBuff = true;
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
            return DescriptionModifier + $"At the end of your turn, if you are in Calm, gain {BuffAmount} Block.";
        }

        public override Card AddCard()
        {
            return new LikeWater();
        }
    }
}