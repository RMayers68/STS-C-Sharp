
namespace STV
{
    public class PowerThrough : Card
    {
        public PowerThrough(bool Upgraded = false)
        {
            Name = "Power Through";
            Type = "Skill";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = ;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            BlockAmount = 20;
            BlockLoops = 1;
            MagicNumber = 2;
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
            return DescriptionModifier + $"Add 2 Wounds to your hand. Gain {BlockAmount} Block.";
        }

        public override Card AddCard()
        {
            return new PowerThrough();
        }
    }
}
