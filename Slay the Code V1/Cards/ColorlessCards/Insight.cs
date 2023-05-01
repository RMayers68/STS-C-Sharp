
namespace STV
{
    public class Insight : Card
    {
        public Insight(bool Upgraded = false)
        {
            Name = "Insight";
            Type = "Skill";
            Rarity = "Common";
            DescriptionModifier = "";
            EnergyCost = 0;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            CardsDrawn = 2;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
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
            return DescriptionModifier + $"Retain. Draw {CardsDrawn} cards. Exhaust.";
        }

        public override Card AddCard()
        {
            return new Insight();
        }
    }
}