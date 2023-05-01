
namespace STV
{
    public class PanicButton : Card
    {
        public PanicButton(bool Upgraded = false)
        {
            Name = "Panic Button";
            Type = "Skill";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = ;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            BlockAmount = 30;
            BlockLoops = 1;
            BuffID = 13;
            BuffAmount = 2;
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
            return DescriptionModifier + $"Gain {BlockAmount} Block. You cannot gain Block from cards for the next 2 turns. Exhaust.";
        }

        public override Card AddCard()
        {
            return new PanicButton();
        }
    }
}
