
namespace STV
{
    public class Judgment : Card
    {
        public Judgment(bool Upgraded = false)
        {
            Name = "Judgment";
            Type = "Skill";
            Rarity = "Rare";
            DescriptionModifier = "";
            EnergyCost = ;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            MagicNumber = 30;
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
            return DescriptionModifier + $"If the enemy has {MagicNumber} or less HP, set their HP to 0.";
        }

        public override Card AddCard()
        {
            return new Judgment();
        }
    }
}