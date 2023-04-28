
namespace STV
{
    public class Dualcast : Card
    {
        public Dualcast(bool Upgraded = false)
        {
            Name = "Dualcast";
            Type = "Skill";
            Rarity = "Common";
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
            return DescriptionModifier + $"Evoke your next Orb twice.";
        }

        public override Card AddCard()
        {
            return new Dualcast();
        }
    }
}