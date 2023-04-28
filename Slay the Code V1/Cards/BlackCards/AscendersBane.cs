namespace STV
{
    public class AscendersBane : Card
    {
        public AscendersBane()
        {
            Name = "Ascender's Bane";
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
            return DescriptionModifier + $"Unplayable. Ethereal. Cannot be removed from your deck.";
        }

        public override Card AddCard()
        {
            return new AscendersBane();
        }
    }
}

