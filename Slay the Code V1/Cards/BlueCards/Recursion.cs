
namespace STV
{
    public class Recursion : Card
    {
        public Recursion(bool Upgraded = false)
        {
            Name = "Recursion";
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
            return DescriptionModifier + $"Evoke your next Orb. Channel the Orb that was just Evoked.";
        }

        public override Card AddCard()
        {
            return new Recursion();
        }
    }
}