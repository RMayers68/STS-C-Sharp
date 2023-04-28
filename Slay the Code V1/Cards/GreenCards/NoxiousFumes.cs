
namespace STV
{
    public class NoxiousFumes : Card
    {
        public NoxiousFumes(bool Upgraded = false)
        {
            Name = "Noxious Fumes";
            Type = "Power";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = ;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            BuffID = 48;
            BuffAmount = 2;
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
            return DescriptionModifier + $"At the start of your turn, apply {BuffAmount} Poison to ALL enemies.";
        }

        public override Card AddCard()
        {
            return new NoxiousFumes();
        }
    }
}
