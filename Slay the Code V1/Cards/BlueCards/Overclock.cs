
namespace STV
{
    public class Overclock : Card
    {
        public Overclock(bool Upgraded = false)
        {
            Name = "Overclock";
            Type = "Skill";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = 0;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(68, 83);
            CardsDrawn = 2;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            hero.DrawCards(CardsDrawn);
            hero.AddToDiscard(new Burn());
        }

        public override void UpgradeCard()
        {
            if (!Upgraded) 
                CardsDrawn++;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Draw {CardsDrawn} cards. Add a Burn into your discard pile.";
        }

        public override Card AddCard()
        {
            return new Overclock();
        }
    }
}
