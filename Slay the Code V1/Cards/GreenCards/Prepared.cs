
namespace STV
{
    public class Prepared : Card
    {
        public Prepared(bool Upgraded = false)
        {
            Name = "Prepared";
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
            for (int i = 0; i < CardsDrawn; i++)
                PickCard(hero.Hand, "discard").Discard(hero, encounter, turnNumber);
        }

        public override void UpgradeCard()
        {
            if (!Upgraded) 
                CardsDrawn++;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Draw {CardsDrawn} card{(Upgraded ? $"s" : "")}. Discard {CardsDrawn} card{(Upgraded ? $"s" : "")}.";
        }

        public override Card AddCard()
        {
            return new Prepared();
        }
    }
}