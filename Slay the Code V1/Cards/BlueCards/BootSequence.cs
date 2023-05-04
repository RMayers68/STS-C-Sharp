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
            EnergyCost = 0;
            if (EnergyCost >= 0)
                TmpEnergyCost = EnergyCost;
            GoldCost = CardRNG.Next(68, 83);
            BlockAmount = 10;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            hero.CardBlock(BlockAmount);
        }

        public override void UpgradeCard()
        {
            if (!Upgraded) 
                BlockAmount += 3;
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
