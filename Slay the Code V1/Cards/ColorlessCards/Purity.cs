
namespace STV
{
    public class Purity : Card
    {
        public Purity(bool Upgraded = false)
        {
            Name = "Purity";
            Type = "Skill";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = 0;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            MagicNumber = 3;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            for (int i = 0; i < MagicNumber; i++)
                PickCard(hero.Hand, "exhaust").Exhaust(hero, encounter, hero.Hand);
        }

        public override void UpgradeCard()
        {
            if (!Upgraded) 
                MagicNumber +=2;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Choose and exhaust up to {MagicNumber} cards in your hand. Exhaust.";
        }

        public override Card AddCard()
        {
            return new Purity();
        }
    }
}