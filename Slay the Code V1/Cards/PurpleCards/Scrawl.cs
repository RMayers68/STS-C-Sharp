
namespace STV
{
    public class Scrawl : Card
    {
        public Scrawl(bool Upgraded = false)
        {
            Name = "Scrawl";
            Type = "Skill";
            Rarity = "Rare";
            DescriptionModifier = "";
            EnergyCost = 1;
            if (EnergyCost >= 0)
                TmpEnergyCost = EnergyCost;
            GoldCost = CardRNG.Next(135, 166);
            CardsDrawn = 10;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            hero.DrawCards(CardsDrawn - hero.Hand.Count);
        }

        public override void UpgradeCard()
        {
            if (!Upgraded) 
                EnergyCost--;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Draw cards until your hand is full. Exhaust.";
        }

        public override Card AddCard()
        {
            return new Scrawl();
        }
    }
}
