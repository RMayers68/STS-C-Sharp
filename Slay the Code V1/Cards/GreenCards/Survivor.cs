
namespace STV
{
    public class Survivor : Card
    {
        public Survivor(bool Upgraded = false)
        {
            Name = "Survivor";
            Type = "Skill";
            Rarity = "Common";
            DescriptionModifier = "";
            EnergyCost = ;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            BlockAmount = 8;
            BlockLoops = 1;
            MagicNumber = 1;
            Discards = true;
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
            return DescriptionModifier + $"Gain {BlockAmount} Block. Discard a card.";
        }

        public override Card AddCard()
        {
            return new Survivor();
        }
    }
}