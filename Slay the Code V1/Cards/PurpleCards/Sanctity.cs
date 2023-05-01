
namespace STV
{
    public class Sanctity : Card
    {
        public Sanctity(bool Upgraded = false)
        {
            Name = "Sanctity";
            Type = "Skill";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = ;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            BlockAmount = 6;
            BlockLoops = 1;
            CardsDrawn = 2;
            if (Upgraded)
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
            return DescriptionModifier + $"Gain {BlockAmount} Block. If the last card played was a Skill, draw 2 cards.";
        }

        public override Card AddCard()
        {
            return new Sanctity();
        }
    }
}