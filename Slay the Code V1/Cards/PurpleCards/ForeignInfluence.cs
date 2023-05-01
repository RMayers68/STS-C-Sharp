
namespace STV
{
    public class ForeignInfluence : Card
    {
        public ForeignInfluence(bool Upgraded = false)
        {
            Name = "Foreign Influence";
            Type = "Skill";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = ;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            MagicNumber = 3;
            if (Upgraded)
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
            return DescriptionModifier + $"Choose 1 of 3 Attacks of any color to add into your hand.{(Upgraded ? ";
                }

                public override Card AddCard()
                {
                        return new ForeignInfluence();
                }
        }
}