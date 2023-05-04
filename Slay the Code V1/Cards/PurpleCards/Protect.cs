
namespace STV
{
    public class Protect : Card
    {
        public Protect(bool Upgraded = false)
        {
            Name = "Protect";
            Type = "Skill";
            Rarity = "Common";
            DescriptionModifier = "";
            EnergyCost = 2;
            if (EnergyCost >= 0)
                TmpEnergyCost = EnergyCost;
            GoldCost = CardRNG.Next(45, 56);
            BlockAmount = 12;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            hero.CardBlock(BlockAmount);
        }

        public override void UpgradeCard()
        {
            if (!Upgraded) 
                BlockAmount +=4;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Retain. Gain {BlockAmount} Block.";
        }

        public override Card AddCard()
        {
            return new Protect();
        }
    }
}
