
namespace STV
{
    public class MentalFortress : Card
    {
        public MentalFortress(bool Upgraded = false)
        {
            Name = "Mental Fortress";
            Type = "Power";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = ;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            BuffID = 11;
            BuffAmount = 4;
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
            return DescriptionModifier + $"Whenever you change Stances, gain {BuffAmount} Block.";
        }

        public override Card AddCard()
        {
            return new MentalFortress();
        }
    }
}
