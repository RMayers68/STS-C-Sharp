
namespace STV
{
    public class BulletTime : Card
    {
        public BulletTime(bool Upgraded = false)
        {
            Name = "Bullet Time";
            Type = "Skill";
            Rarity = "Rare";
            DescriptionModifier = "";
            EnergyCost = ;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            BuffID = 21;
            BuffAmount = 1;
            HeroBuff = true;
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
            return DescriptionModifier + $"You cannot draw any cards this turn. Reduce the cost of cards in your hand to 0 this turn.";
        }

        public override Card AddCard()
        {
            return new BulletTime();
        }
    }
}