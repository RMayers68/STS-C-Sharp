
namespace STV
{
    public class SecondWind : Card
    {
        public SecondWind(bool Upgraded = false)
        {
            Name = "Second Wind";
            Type = "Skill";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = ;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            BlockAmount = 5;
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
            return DescriptionModifier + $"Exhaust all non-Attack cards in your hand and gain {BlockAmount} Block for each.";
        }

        public override Card AddCard()
        {
            return new SecondWind();
        }
    }
}