
namespace STV
{
    public class Metamorphosis : Card
    {
        public Metamorphosis(bool Upgraded = false)
        {
            Name = "Metamorphosis";
            Type = "Skill";
            Rarity = "Rare";
            DescriptionModifier = "";
            EnergyCost = 2;
            if (EnergyCost >= 0)
                TmpEnergyCost = EnergyCost;
            GoldCost = CardRNG.Next(135, 166);
            MagicNumber = 3;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            List<Card> metamorphosis = new(RandomCards(hero.Name, MagicNumber));
            foreach (Card c in metamorphosis)
                c.SetEnergyCost(0);
            hero.DrawPile.AddRange(metamorphosis);
            hero.ShuffleDrawPile();
        }

        public override void UpgradeCard()
        {
            if (!Upgraded) 
                MagicNumber += 2;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Add {MagicNumber} random Attacks into your Draw Pile. They cost 0 this combat. Exhaust.";
        }

        public override Card AddCard()
        {
            return new Metamorphosis();
        }
    }
}