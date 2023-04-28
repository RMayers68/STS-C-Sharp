
namespace STV
{
    public class LiveForever : Card
    {
        public LiveForever(bool Upgraded = false)
        {
            Name = "Live Forever";
            Type = "Power";
            Rarity = "Common";
            DescriptionModifier = "";
            EnergyCost = 0;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            BuffID = 95;
            BuffAmount = 6;
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
            return DescriptionModifier + $"Gain {BuffAmount} Plated Armor.";
        }

        public override Card AddCard()
        {
            return new LiveForever();
        }
    }
}
