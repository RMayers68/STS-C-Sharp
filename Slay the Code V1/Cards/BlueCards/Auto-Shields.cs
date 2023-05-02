namespace STV
{
    public class AutoShields : Card
    {
        public AutoShields(bool Upgraded = false)
        {
            Name = "Auto-Shields";
            Type = "Skill";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = 1;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(68, 83);
            BlockAmount = 11;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            if (hero.Block <= 0)
                hero.CardBlock(BlockAmount, encounter);
        }

        public override void UpgradeCard()
        {
            if (!Upgraded) 
                BlockAmount += 4;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"If you have 0 Block, gain {BlockAmount} Block.";
        }

        public override Card AddCard()
        {
            return new AutoShields();
        }
    }
}

