
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
            EnergyCost = 0;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(68, 83);
            BlockAmount = 30;
            BuffID = 13;
            BuffAmount = 2;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            hero.CardBlock(BlockAmount);
            hero.AddBuff(BuffID, BuffAmount);
        }

        public override void UpgradeCard()
        {
            if (!Upgraded)
                BlockAmount += 10 ;
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
