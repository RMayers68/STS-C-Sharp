
namespace STV
{
    public class Decay : Card
    {
        public Decay()
        {
            Name = "Decay";
            Type = "Curse";
            Rarity = "Common";
            DescriptionModifier = "";
            EnergyCost = -2;
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            hero.NonAttackDamage(hero, 2, "Decay");
        }
        public override string GetDescription()
        {
            return DescriptionModifier + $"Unplayable. At the end of your turn, take 2 damage.";
        }

        public override Card AddCard()
        {
            return new Decay();
        }
    }
}
