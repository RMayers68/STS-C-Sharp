
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
            EnergyCost = ;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            MagicNumber = 1;
            if (upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
        }

        public override void UpgradeCard()
        {
            if (!Upgraded) ;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Add {MagicNumber} random Colorless card{(Upgraded ? ";
                }

                public override Card AddCard()
                {
                        return new JackOfAllTrades();
                }
        }
}