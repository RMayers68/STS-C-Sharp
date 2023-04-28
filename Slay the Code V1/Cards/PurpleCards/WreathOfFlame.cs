
namespace STV
{
    public class WreathofFlame : Card
    {
        public WreathofFlame(bool Upgraded = false)
        {
            Name = "Wreath of Flame";
            Type = "Skill";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = ;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            BuffID = 84;
            BuffAmount = 5;
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
            return DescriptionModifier + $"Your next Attack deals {BuffAmount} additional damage.";
        }

        public override Card AddCard()
        {
            return new WreathofFlame();
        }
    }
}