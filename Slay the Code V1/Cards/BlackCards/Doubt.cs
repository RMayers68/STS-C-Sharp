
namespace STV
{
    public class Doubt : Card
    {
        public Doubt()
        {
            Name = "Doubt";
            Type = "Curse";
            Rarity = "Common";
            DescriptionModifier = "";
            EnergyCost = -2;
            BuffID = 2;
            BuffAmount = 1;
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            hero.AddBuff(BuffID, BuffAmount);
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Unplayable. At the end of your turn, gain 1 Weak.";
        }

        public override Card AddCard()
        {
            return new Doubt();
        }
    }
}