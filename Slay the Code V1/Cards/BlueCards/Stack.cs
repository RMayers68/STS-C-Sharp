
namespace STV
{
    public class Stack : Card
    {
        public Stack(bool Upgraded = false)
        {
            Name = "Stack";
            Type = "Skill";
            Rarity = "Common";
            DescriptionModifier = "";
            EnergyCost = 1;
            if (EnergyCost >= 0)
                TmpEnergyCost = EnergyCost;
            GoldCost = CardRNG.Next(45, 56);
            BlockAmount = 0;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            hero.CardBlock(hero.DiscardPile.Count + BlockAmount, encounter);
        }

        public override void UpgradeCard()
        {
            if (!Upgraded) 
                BlockAmount += 3;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Gain Block equal to the number of cards in your discard pile{(Upgraded ? $" +3" : "")}.";
                }

                public override Card AddCard()
                {
                        return new Stack();
                }
        }
}