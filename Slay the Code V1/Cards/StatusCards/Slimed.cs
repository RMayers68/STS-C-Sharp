
namespace STV
{
    public class Slimed : Card
    {
        public Slimed()
        {
            Name = "Slimed";
            Type = "Status";
            Rarity = "Common";
            DescriptionModifier = "";
            EnergyCost = 1;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Exhaust.";
        }

        public override Card AddCard()
        {
            return new Slimed();
        }
    }
}