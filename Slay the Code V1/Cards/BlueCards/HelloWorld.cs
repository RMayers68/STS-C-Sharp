
namespace STV
{
    public class HelloWorld : Card
    {
        public HelloWorld(bool Upgraded = false)
        {
            Name = "Hello World";
            Type = "Power";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = 1;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            BuffID = 61;
            BuffAmount = 1;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            hero.AddBuff(BuffID, BuffAmount);
        }

        public override void UpgradeCard()
        {
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"{(Upgraded ? $"Innate. " : "")}At the start of your turn, add a random Common card into your hand.";
        }

        public override Card AddCard()
        {
            return new HelloWorld();
        }
    }
}