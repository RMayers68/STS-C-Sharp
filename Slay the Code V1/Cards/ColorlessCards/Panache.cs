
namespace STV
{
    public class Panache : Card
    {
        public Panache(bool Upgraded = false)
        {
            Name = "Panache";
            Type = "Power";
            Rarity = "Rare";
            DescriptionModifier = "";
            EnergyCost = ;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            MagicNumber = 10;
            BuffID = 93;
            BuffAmount = 10;
            HeroBuff = true;
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
            return DescriptionModifier + $"Every time you play 5 cards in a single turn, deal {BuffAmount} damage to ALL enemies.";
        }

        public override Card AddCard()
        {
            return new Panache();
        }
    }
}