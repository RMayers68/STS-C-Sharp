
namespace STV
{
    public class Expertise : Card
    {
        public Expertise(bool Upgraded = false)
        {
            Name = "Expertise";
            Type = "Skill";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = 1;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(68, 83);
            CardsDrawn = 6;
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
                CardsDrawn++;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Draw cards until you have {CardsDrawn} in hand.";
        }

        public override Card AddCard()
        {
            return new Expertise();
        }
    }
}