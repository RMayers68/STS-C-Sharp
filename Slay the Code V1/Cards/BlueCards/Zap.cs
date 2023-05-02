
namespace STV
{
    public class Zap : Card
    {
        public Zap(bool Upgraded = false)
        {
            Name = "Zap";
            Type = "Skill";
            Rarity = "Basic";
            DescriptionModifier = "";
            EnergyCost = 1;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = 0;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            Orb.ChannelOrb(hero, encounter, 0);
        }

        public override void UpgradeCard()
        {
            if (!Upgraded) 
                EnergyCost--;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Channel 1 Lightning.";
        }

        public override Card AddCard()
        {
            return new Zap();
        }
    }
}