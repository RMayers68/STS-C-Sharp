
namespace STV
{
    public class Omniscience : Card
    {
        public Omniscience(bool Upgraded = false)
        {
            Name = "Omniscience";
            Type = "Skill";
            Rarity = "Rare";
            DescriptionModifier = "";
            EnergyCost = ;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            MagicNumber = 2;
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
            return DescriptionModifier + $"Choose a card in your draw pile. Play the chosen card twice and Exhaust it. Exhaust.";
        }

        public override Card AddCard()
        {
            return new Omniscience();
        }
    }
}