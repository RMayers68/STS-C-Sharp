
namespace STV
{
    public class DeepBreath : Card
    {
        public DeepBreath(bool Upgraded = false)
        {
            Name = "Deep Breath";
            Type = "Skill";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = 0;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            CardsDrawn = 1;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            hero.Discard2Draw();
            hero.DrawCards(CardsDrawn);
        }

        public override void UpgradeCard()
        {
            if (!Upgraded) 
                CardsDrawn++;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Shuffle your discard pile into your draw pile. Draw {(Upgraded ? "2 cards" : "1 card.")}";
        }

        public override Card AddCard()
        {
            return new DeepBreath();
        }
    }
}
