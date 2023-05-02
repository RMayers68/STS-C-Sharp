
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
            EnergyCost = 3;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(135, 166);
            BuffID = 21;
            BuffAmount = 1;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            foreach (Card c in hero.Hand)
                c.TmpEnergyCost = 0;
            hero.AddBuff(21, 1);
        }

        public override void UpgradeCard()
        {
            if (!Upgraded) EnergyCost--;
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