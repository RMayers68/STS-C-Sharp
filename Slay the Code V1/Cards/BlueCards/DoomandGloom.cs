
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
            EnergyCost = 2;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(68, 83);
            AttackDamage = 10;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            foreach (Enemy e in encounter)
                hero.Attack(e, AttackDamage + extraDamage, encounter);
            Orb.ChannelOrb(hero, encounter, 2);
        }

        public override void UpgradeCard()
        {
            if (!Upgraded) 
                AttackDamage += 4;
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