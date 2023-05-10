namespace STV
{
    public class Void : Card
    {
        public Void()
        {
            Name = "Void";
            Type = "Status";
            Rarity = "Common";
            DescriptionModifier = "";
            EnergyCost = -2;
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            hero.GainTurnEnergy(-1);
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Unplayable. When drawn, lose 1 Energy.";
        }

        public override Card AddCard()
        {
            return new Void();
        }
    }
}