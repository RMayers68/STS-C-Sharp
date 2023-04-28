
namespace STV
{
    public class Wound : Card
    {
        public Wound()
        {
            Name = "Wound";
            Type = "Status";
            Rarity = "Common";
            DescriptionModifier = "";
            EnergyCost = -2;
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage)
        {
            return;
        }
        public override string GetDescription()
        {
            return DescriptionModifier + $"Unplayable.";
        }

        public override Card AddCard()
        {
            return new Wound();
        }
    }
}