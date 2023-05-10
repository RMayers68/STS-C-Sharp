
namespace STV
{
    public class Purchased : Card
    {
        public Purchased()
        {
            Name = "Purchased";
            Type = "Curse";
            Rarity = "Common";
            DescriptionModifier = "";
            EnergyCost = -2;
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            hero.AddToDrawPile(AddCard(), false);
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Innate. At the end of your turn, place a copy of this card on top of your draw pile.";
        }

        public override Card AddCard()
        {
            return new Purchased();
        }
    }
}