
namespace STV
{
    public class GeneticAlgorithm : Card
    {
        public GeneticAlgorithm(bool Upgraded = false)
        {
            Name = "Genetic Algorithm";
            Type = "Skill";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = ;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            BlockAmount = 1;
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
            return DescriptionModifier + $"Gain {BlockAmount} Block. When played, permanently increase this card's Block by {MagicNumber}. Exhaust.";
        }

        public override Card AddCard()
        {
            return new GeneticAlgorithm();
        }
    }
}
