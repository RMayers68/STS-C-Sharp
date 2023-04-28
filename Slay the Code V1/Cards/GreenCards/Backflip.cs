
namespace STV
{
    public class Backflip : Card
    {
        public Backflip(bool Upgraded = false)
        {
            Name = "Backflip";
            Type = "Skill";
            Rarity = "Common";
            DescriptionModifier = "";
            EnergyCost = ;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            BlockAmount = 5;
            BlockLoops = 1;
            CardsDrawn = 2;
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
            return DescriptionModifier + $"Gain {BlockAmount} Block. Draw 2 cards.";
        }

        public override Card AddCard()
        {
            return new Backflip();
        }
    }
}