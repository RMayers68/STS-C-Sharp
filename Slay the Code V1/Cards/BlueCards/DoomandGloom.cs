
namespace STV
{
    public class DoomandGloom : Card
    {
        public DoomandGloom(bool Upgraded = false)
        {
            Name = "Doom and Gloom";
            Type = "Attack";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = ;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            AttackDamage = 10;
            AttackLoops = 1;
            MagicNumber = 2;
            AttackAll = true;
            OrbChannels = true;
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
            return DescriptionModifier + $"Deal {AttackDamage} damage to ALL enemies. Channel 1 Dark.";
        }

        public override Card AddCard()
        {
            return new DoomandGloom();
        }
    }
}