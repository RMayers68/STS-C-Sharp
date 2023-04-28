
namespace STV
{
    public class Shame : Card
    {
        public Shame()
        {
            Name = "Shame";
            Type = "Curse";
            Rarity = "Common";
            DescriptionModifier = "";
            EnergyCost = -2;
            BuffID = 6;
            BuffAmount = 1;
            HeroBuff = true;
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            hero.AddBuff(BuffID, BuffAmount);
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Unplayable. At the end of your turn, gain 1 Frail.";
        }

        public override Card AddCard()
        {
            return new Shame();
        }
    }
}