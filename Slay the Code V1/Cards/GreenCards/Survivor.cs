
namespace STV
{
    public class Survivor : Card
    {
        public Survivor(bool Upgraded = false)
        {
            Name = "Survivor";
            Type = "Skill";
            Rarity = "Basic";
            DescriptionModifier = "";
            EnergyCost = 1;
            if (EnergyCost >= 0)
                TmpEnergyCost = EnergyCost;
            GoldCost = 0;
            BlockAmount = 8;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            hero.CardBlock(BlockAmount, encounter);
            PickCard(hero.Hand, "discard").Discard(hero, encounter, turnNumber);
        }

        public override void UpgradeCard()
        {
            if (!Upgraded) 
                BlockAmount += 3;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Gain {BlockAmount} Block. Discard a card.";
        }

        public override Card AddCard()
        {
            return new Survivor();
        }
    }
}