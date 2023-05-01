
namespace STV
{
    public class InnerPeace : Card
    {
        public InnerPeace(bool Upgraded = false)
        {
            Name = "Inner Peace";
            Type = "Skill";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = ;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            CardsDrawn = 3;
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
            return DescriptionModifier + $"If you are in Calm, draw{CardsDrawn} cards. Otherwise, enter Calm.";
        }

        public override Card AddCard()
        {
            return new InnerPeace();
        }
    }
}