
namespace STV
{
    public class Normality : Card
    {
        public Normality()
        {
            Name = "Normality";
            Type = "Curse";
            Rarity = "Common";
            DescriptionModifier = "";
            EnergyCost = -2;
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
        }
        public override string GetDescription()
        {
            return DescriptionModifier + $"Unplayable. You cannot play more than 3 cards this turn.";
        }

        public override Card AddCard()
        {
            return new Normality();
        }
    }
}