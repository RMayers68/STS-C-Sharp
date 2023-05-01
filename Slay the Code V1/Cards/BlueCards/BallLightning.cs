

namespace STV
{
    public class BallLightning : Card
    {
        public BallLightning(bool Upgraded = false)
        {
            Name = "Ball Lightning";
            Type = "Attack";
            Rarity = "Common";
            DescriptionModifier = "";
            EnergyCost = ;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            AttackDamage = 7;
            AttackLoops = 1;
            BlockLoops = 1;
            Targetable = true;
            SingleAttack = true;
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
            return DescriptionModifier + $"Deal {AttackDamage} damage. Channel 1 Lightning.";
        }

        public override Card AddCard()
        {
            return new BallLightning();
        }
    }
}