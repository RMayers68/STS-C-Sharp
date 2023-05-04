

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
            EnergyCost = 1;
            if (EnergyCost >= 0)
                TmpEnergyCost = EnergyCost;
            GoldCost = CardRNG.Next(45, 56);
            AttackDamage = 7;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            int target = hero.DetermineTarget(encounter);
            encounter[target].AddBuff(BuffID, BuffAmount,hero);
            Orb.ChannelOrb(hero, encounter, 0);
        }

        public override void UpgradeCard()
        {
            if (!Upgraded) 
                AttackDamage += 3;
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