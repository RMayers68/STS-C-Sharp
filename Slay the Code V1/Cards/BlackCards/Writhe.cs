
namespace STV
{
    public class Writhe : Card
    {
        public Writhe()
        {
            Name = "Writhe";
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
            return DescriptionModifier + $"Unplayable. Innate.";
        }

        public override Card AddCard()
        {
            return new Writhe();
        }
    }
}