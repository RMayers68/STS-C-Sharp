
namespace STV
{
    public class Metallicize : Card
    {
        public Metallicize(bool Upgraded = false)
        {
            Name = "Metallicize";
            Type = "Power";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = ;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            BuffID = 32;
            BuffAmount = 3;
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
            return DescriptionModifier + $"At the end of your turn, gain {BuffAmount} Block.";
        }

        public override Card AddCard()
        {
            return new Metallicize();
        }
    }
}