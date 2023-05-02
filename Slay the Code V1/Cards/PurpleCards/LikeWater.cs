
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
            EnergyCost = 1;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(68, 83);
            BuffID = 77;
            BuffAmount = 5;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            hero.AddBuff(BuffID, BuffAmount);
        }

        public override void UpgradeCard()
        {
            if (!Upgraded) 
                BuffAmount += 2;
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