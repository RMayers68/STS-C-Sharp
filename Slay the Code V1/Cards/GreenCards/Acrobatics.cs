
namespace STV
{
    public class Acrobatics : Card
    {
        public Acrobatics(bool Upgraded = false)
        {
            Name = "Acrobatics";
            Type = "Skill";
            Rarity = "Common";
            DescriptionModifier = "";
            EnergyCost = 1;
            if (EnergyCost >= 0)
                TmpEnergyCost = EnergyCost;
            GoldCost = CardRNG.Next(45, 56);
            MagicNumber = 1;
            CardsDrawn = 3;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            hero.DrawCards(CardsDrawn);
            PickCard(hero.Hand,"discard").Discard(hero, encounter, turnNumber);
        }

        public override void UpgradeCard()
        {
            if (!Upgraded) 
                CardsDrawn++;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Draw {CardsDrawn} cards. Discard {MagicNumber} card.";
        }

        public override Card AddCard()
        {
            return new Acrobatics();
        }
    }
}