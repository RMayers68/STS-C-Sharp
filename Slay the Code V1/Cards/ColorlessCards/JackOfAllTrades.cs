
namespace STV
{
    public class JackOfAllTrades : Card
    {
        public JackOfAllTrades(bool Upgraded = false)
        {
            Name = "Jack Of All Trades";
            Type = "Skill";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = 0;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(68, 83);
            MagicNumber = 1;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            for (int i = 0; i < MagicNumber; i++)
                hero.AddToHand(RandomCard("Colorless"));
        }

        public override void UpgradeCard()
        {
            if (!Upgraded) 
                MagicNumber++;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Add {MagicNumber} random Colorless card{(Upgraded ? "s" : "")} to your hand.";
        }

        public override Card AddCard()
        {
            return new JackOfAllTrades();
        }
    }
}