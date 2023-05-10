
namespace STV
{
    public class Dazed : Card
    {
        public Dazed()
        {
            Name = "Dazed";
            Type = "Status";
            Rarity = "Common";
            DescriptionModifier = "";
            EnergyCost = 0;
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
            return new Dazed();
        }
    }
}
