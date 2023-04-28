
namespace STV
{
    public class Pain : Card
    {
        public Pain()
        {
            Name = "Pain";
            Type = "Curse";
            Rarity = "Common";
            DescriptionModifier = "";
            EnergyCost = -2;
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            hero.SelfDamage(1);
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Unplayable. While in hand, lose 1 HP when other cards are played.";
        }

        public override Card AddCard()
        {
            return new Pain();
        }
    }
}
