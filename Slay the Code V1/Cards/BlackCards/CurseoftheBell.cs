
namespace STV
{
    public class CurseoftheBell : Card
    {
        public CurseoftheBell()
        {
            Name = "Curse of the Bell";
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
            return DescriptionModifier + $"Unplayable. Cannot be removed from your deck.";
        }

        public override Card AddCard()
        {
            return new CurseoftheBell();
        }
    }
}
