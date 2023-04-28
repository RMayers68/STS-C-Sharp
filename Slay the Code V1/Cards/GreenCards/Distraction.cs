
namespace STV
{
    public class Distraction : Card
    {
        public Distraction(bool Upgraded = false)
        {
            Name = "Distraction";
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
            return DescriptionModifier + $"Add a random Skill to your hand. It costs 0 this turn. Exhaust.";
        }

        public override Card AddCard()
        {
            return new Distraction();
        }
    }
}