
namespace STV
{
    public class GoodInstincts : Card
    {
        public GoodInstincts(bool Upgraded = false)
        {
            Name = "Good Instincts";
            Type = "Skill";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = 0;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            BlockAmount = 6;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            hero.CardBlock(BlockAmount, encounter);
        }

        public override void UpgradeCard()
        {
            if (!Upgraded)  
                BlockAmount += 3;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Gain {BlockAmount} Block.";
        }

        public override Card AddCard()
        {
            return new GoodInstincts();
        }
    }
}