
namespace STV
{
    public class Evaluate : Card
    {
        public Evaluate(bool Upgraded = false)
        {
            Name = "Evaluate";
            Type = "Skill";
            Rarity = "Common";
            DescriptionModifier = "";
            EnergyCost = 1;
            if (EnergyCost >= 0)
                TmpEnergyCost = EnergyCost;
            GoldCost = CardRNG.Next(45, 56);
            BlockAmount = 6;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            hero.CardBlock(BlockAmount);
            hero.AddToDrawPile(new Evaluate(), true);
        }

        public override void UpgradeCard()
        {
            if (!Upgraded) 
                BlockAmount += 4;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Gain {BlockAmount} Block. Shuffle an Insight into your draw pile.";
        }

        public override Card AddCard()
        {
            return new Evaluate();
        }
    }
}