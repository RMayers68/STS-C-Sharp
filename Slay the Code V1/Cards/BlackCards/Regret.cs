
namespace STV
{
    public class Regret : Card
    {
        public Regret()
        {
            Name = "Regret";
            Type = "Curse";
            Rarity = "Common";
            DescriptionModifier = "";
            EnergyCost = -2;
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            hero.NonAttackDamage(hero, hero.Hand.Count, "Regret");
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Unplayable. At the end of your turn, lose 1 HP for each card in your hand.";
        }

        public override Card AddCard()
        {
            return new Regret();
        }
    }
}