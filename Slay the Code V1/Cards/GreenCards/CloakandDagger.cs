
namespace STV
{
    public class CloakAndDagger : Card
    {
        public CloakAndDagger(bool Upgraded = false)
        {
            Name = "Cloak And Dagger";
            Type = "Skill";
            Rarity = "Common";
            DescriptionModifier = "";
            EnergyCost = ;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            BlockAmount = 6;
            BlockLoops = 1;
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
            return DescriptionModifier + $"Gain {BlockAmount} Block. Add {MagicNumber} Shiv to your hand.";
        }

        public override Card AddCard()
        {
            return new CloakAndDagger();
        }
    }
}