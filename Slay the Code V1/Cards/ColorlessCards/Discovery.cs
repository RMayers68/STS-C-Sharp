
namespace STV
{
    public class Discovery : Card
    {
        public Discovery(bool Upgraded = false)
        {
            Name = "Discovery";
            Type = "Skill";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = ;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            MagicNumber = 3;
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
            return DescriptionModifier + $"Choose 1 of 3 random cards to add to your hand. It costs 0 this turn. {(Upgraded ? ";
                }

                public override Card AddCard()
                {
                        return new Discovery();
                }
        }
}
