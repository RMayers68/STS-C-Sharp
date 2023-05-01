
namespace STV
{
    public class Exhume : Card
    {
        public Exhume(bool Upgraded = false)
        {
            Name = "Exhume";
            Type = "Skill";
            Rarity = "Rare";
            DescriptionModifier = "";
            EnergyCost = 0;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            PickCard(hero.ExhaustPile, "bring back from the Exhaust Pile").MoveCard(hero.ExhaustPile,hero.Hand);
        }

        public override void UpgradeCard()
        {
            if (!Upgraded) 
                EnergyCost--;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Choose an Exhausted card and put it in your hand. Exhaust.";
        }

        public override Card AddCard()
        {
            return new Exhume();
        }
    }
}