
namespace STV
{
    public class DeepBreath : Card
    {
        public DeepBreath(bool Upgraded = false)
        {
            Name = "Deep Breath";
            Type = "Skill";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = ;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            CardsDrawn = 1;
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
            return DescriptionModifier + $"Shuffle your discard pile into your draw pile. Draw {(Upgraded ? ";
                }

                public override Card AddCard()
                {
                        return new DeepBreath();
                }
        }
}
