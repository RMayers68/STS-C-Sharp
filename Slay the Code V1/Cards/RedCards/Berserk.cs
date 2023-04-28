namespace STV
{
    public class Berserk : Card
    {
        public Berserk(bool Upgraded = false)
        {
            Name = "Berserk";
            Type = "Power";
            Rarity = "Rare";
            DescriptionModifier = "";
            EnergyCost = 0;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            BuffID = 1;
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
            return DescriptionModifier + $"Gain {BuffAmount} Vulnerable. At the start of your turn, gain 1 Energy.";
        }

        public override Card AddCard()
        {
            return new Berserk();
        }
    }
}
