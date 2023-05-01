
namespace STV
{
    public class LimitBreak : Card
    {
        public LimitBreak(bool Upgraded = false)
        {
            Name = "Limit Break";
            Type = "Skill";
            Rarity = "Rare";
            DescriptionModifier = "";
            EnergyCost = 1;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            if (hero.FindBuff("Strength") is Buff str && str != null)
                hero.AddBuff(4, str.Intensity);
        }

        public override void UpgradeCard()
        {
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Double your Strength.{(Upgraded ? "" : " Exhaust.")}";
        }

        public override Card AddCard()
        {
            return new LimitBreak();
        }
    }
}