
namespace STV
{
    public class Electrodynamics : Card
    {
        public Electrodynamics(bool Upgraded = false)
        {
            Name = "Electrodynamics";
            Type = "Power";
            Rarity = "Rare";
            DescriptionModifier = "";
            EnergyCost = ;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            BlockLoops = 2;
            BuffID = 68;
            BuffAmount = 1;
            HeroBuff = true;
            OrbChannels = true;
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
            return DescriptionModifier + $"Lightning now hits ALL enemies. Channel {BlockLoops} Lightning.";
        }

        public override Card AddCard()
        {
            return new Electrodynamics();
        }
    }
}