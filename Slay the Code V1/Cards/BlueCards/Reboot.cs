
namespace STV
{
    public class Reboot : Card
    {
        public Reboot(bool Upgraded = false)
        {
            Name = "Reboot";
            Type = "Skill";
            Rarity = "Rare";
            DescriptionModifier = "";
            EnergyCost = 0;
            if (EnergyCost >= 0)
                TmpEnergyCost = EnergyCost;
            GoldCost = CardRNG.Next(135, 166);
            CardsDrawn = 4;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            foreach (Card c in hero.DiscardPile)
                c.MoveCard(hero.DiscardPile, hero.DrawPile);
            foreach (Card c in hero.Hand)
                c.MoveCard(hero.Hand, hero.DrawPile);
            hero.ShuffleDrawPile();
            hero.DrawCards(CardsDrawn);
        }

        public override void UpgradeCard()
        {
            if (!Upgraded) 
                CardsDrawn += 2;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Shuffle all of your cards into your draw pile, then draw {CardsDrawn} cards. Exhaust.";
        }

        public override Card AddCard()
        {
            return new Reboot();
        }
    }
}