
namespace STV
{
    public class EchoForm : Card
    {
        public EchoForm(bool Upgraded = false)
        {
            Name = "Echo Form";
            Type = "Power";
            Rarity = "Rare";
            DescriptionModifier = "";
            EnergyCost = 3;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            BuffID = 59;
            BuffAmount = 1;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            hero.AddBuff(BuffID, BuffAmount);
        }

        public override void UpgradeCard()
        {
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"{(Upgraded ? $"" : "Ethereal. ")}The first card you play each turn is played twice.";
        }

        public override Card AddCard()
        {
            return new EchoForm();
        }
    }
}