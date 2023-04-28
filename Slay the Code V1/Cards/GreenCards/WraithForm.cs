
namespace STV
{
    public class WraithForm : Card
    {
        public WraithForm(bool Upgraded = false)
        {
            Name = "Wraith Form";
            Type = "Power";
            Rarity = "Rare";
            DescriptionModifier = "";
            EnergyCost = ;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            BuffID = 52;
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
            return DescriptionModifier + $"Gain {BuffAmount} Intangible. At the end of your turn, lose 1 Dexterity.";
        }

        public override Card AddCard()
        {
            return new WraithForm();
        }
    }
}