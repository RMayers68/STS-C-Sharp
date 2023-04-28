
namespace STV
{
    public class Perseverance : Card
    {
        public Perseverance(bool Upgraded = false)
        {
            Name = "Perseverance";
            Type = "Skill";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = ;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            BlockAmount = 5;
            BlockLoops = 1;
            MagicNumber = 2;
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
            return DescriptionModifier + $"Retain. Gain {BlockAmount} Block. When Retained, increase its Block by {MagicNumber} this combat.";
        }

        public override Card AddCard()
        {
            return new Perseverance();
        }
    }
}