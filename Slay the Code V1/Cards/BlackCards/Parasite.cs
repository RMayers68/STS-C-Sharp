

namespace STV
{
    public class Parasite : Card
    {
        public Parasite()
        {
            Name = "Parasite";
            Type = "Curse";
            Rarity = "Common";
            DescriptionModifier = "";
            EnergyCost = -2;
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            hero.SetMaxHP(-3);
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Unplayable. If transformed or removed from your deck, lose 3 Max HP.";
        }

        public override Card AddCard()
        {
            return new Parasite();
        }
    }
}