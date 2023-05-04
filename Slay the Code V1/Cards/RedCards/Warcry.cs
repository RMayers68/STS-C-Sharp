
namespace STV
{
    public class Warcry : Card
    {
        public Warcry(bool Upgraded = false)
        {
            Name = "Warcry";
            Type = "Skill";
            Rarity = "Common";
            DescriptionModifier = "";
            EnergyCost = 0;
            if (EnergyCost >= 0)
                TmpEnergyCost = EnergyCost;
            GoldCost = CardRNG.Next(45, 56);
            CardsDrawn = 1;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            hero.DrawCards(CardsDrawn);
            PickCard(hero.Hand, "add to the top of your drawpile").MoveCard(hero.Hand, hero.DrawPile);
        }

        public override void UpgradeCard()
        {
            if (!Upgraded) 
                CardsDrawn++;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Draw {CardsDrawn} card{(Upgraded ? $"s" : "")}.  Place a card from your hand on top of your draw pile. Exhaust.";
                }

                public override Card AddCard()
                {
                        return new Warcry();
                }
        }
}