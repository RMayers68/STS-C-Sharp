
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
            EnergyCost = 1;
            if (EnergyCost >= 0)
                TmpEnergyCost = EnergyCost;
            GoldCost = CardRNG.Next(68, 83);
            BlockAmount = 1;
            MagicNumber = 2;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            hero.Deck.FindAll(x => x.Name == Name).Find(x => x.BlockAmount == BlockAmount).BlockAmount += MagicNumber;
            BlockAmount += MagicNumber;
        }

        public override void UpgradeCard()
        {
            if (!Upgraded) 
                MagicNumber++;
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
