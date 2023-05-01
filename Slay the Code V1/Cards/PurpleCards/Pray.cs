namespace STV
{
    public class Pray : Card
    {
        public Pray(bool Upgraded = false)
        {
            Name = "Pray";
            Type = "Skill";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = ;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            BuffID = 10;
            BuffAmount = 3;
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
            return DescriptionModifier + $"Gain {MagicNumber} Mantra. Shuffle an Insight into your draw pile.";
        }

        public override Card AddCard()
        {
            return new Pray();
        }
    }
}