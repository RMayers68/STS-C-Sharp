u
namespace STV
{
    public class BootSequence : Card
    {
        public BootSequence(bool Upgraded = false)
        {
            Name = "Boot Sequence";
            Type = "Skill";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = ;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            BlockAmount = 10;
            BlockLoops = 1;
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
            return DescriptionModifier + $" Innate. Gain {BlockAmount} Block. Exhaust.";
        }

        public override Card AddCard()
        {
            return new BootSequence();
        }
    }
}
