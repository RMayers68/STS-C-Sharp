
namespace STV
{
    public class Clumsy : Card
    {
        public Clumsy()
        {
            Name = "Clumsy";
            Type = "Curse";
            Rarity = "Common";
            DescriptionModifier = "";
            EnergyCost = -2;
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            return;
        }
        public override string GetDescription()
        {
            return DescriptionModifier + $"Unplayable. Ethereal.";
        }

        public override Card AddCard()
        {
            return new Clumsy();
        }
    }
}