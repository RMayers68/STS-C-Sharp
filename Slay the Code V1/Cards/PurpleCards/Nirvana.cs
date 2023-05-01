
namespace STV
{
    public class Nirvana : Card
    {
        public Nirvana(bool Upgraded = false)
        {
            Name = "Nirvana";
            Type = "Power";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = ;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            BuffID = 79;
            BuffAmount = 3;
            HeroBuff = true;
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
            return DescriptionModifier + $"Whenever you Scry, gain {BuffAmount} Block.";
        }

        public override Card AddCard()
        {
            return new Nirvana();
        }
    }
}