
namespace STV
{
    public class Injury : Card
    {
        public Injury()
        {
            Name = "Injury";
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
            return DescriptionModifier + $"Unplayable.";
        }

        public override Card AddCard()
        {
            return new Injury();
        }
    }
}