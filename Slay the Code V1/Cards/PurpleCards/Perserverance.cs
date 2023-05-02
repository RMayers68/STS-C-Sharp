
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
            EnergyCost = 1;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(68, 83);
            BlockAmount = 5;
            MagicNumber = 2;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            hero.CardBlock(BlockAmount, encounter);
        }

        public override void UpgradeCard()
        {
            if (!Upgraded)
            {
                BlockAmount += 2;
                MagicNumber++;
            }
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