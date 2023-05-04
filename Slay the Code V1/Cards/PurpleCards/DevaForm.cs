
namespace STV
{
    public class DevaForm : Card
    {
        public DevaForm(bool Upgraded = false)
        {
            Name = "Deva Form";
            Type = "Power";
            Rarity = "Rare";
            DescriptionModifier = "";
            EnergyCost = 3;
            if (EnergyCost >= 0)
                TmpEnergyCost = EnergyCost;
            GoldCost = CardRNG.Next(135, 166);
            BuffID = 73;
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
            return DescriptionModifier + $"{(Upgraded ? $"" : "Ethereal. ")}At the start of your turn, gain Energy and increase this gain by 1.";
                }

                public override Card AddCard()
                {
                        return new DevaForm();
                }
        }
}