
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
            EnergyCost = ;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            MagicNumber = 1;
            CardsDrawn = 3;
            SelfDamage = true;
            if (upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
        }

        public override void UpgradeCard()
        {
            if (!Upgraded) ;
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