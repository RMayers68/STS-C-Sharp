
namespace STV
{
    public class Setup : Card
    {
        public Setup(bool Upgraded = false)
        {
            Name = "Setup";
            Type = "Skill";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = ;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
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
            return DescriptionModifier + $"Place a card in your hand on top of your draw pile. It cost 0 until it is played.";
        }

        public override Card AddCard()
        {
            return new Setup();
        }
    }
}