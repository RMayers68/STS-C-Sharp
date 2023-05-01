
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
            EnergyCost = 2;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            BuffID = 68;
            BuffAmount = 1;
            MagicNumber = 2;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            hero.AddBuff(BuffID, BuffAmount);
            for (int i = 0; i < MagicNumber; i++)
                Orb.ChannelOrb(hero, encounter, 0);
        }

        public override void UpgradeCard()
        {
            if (!Upgraded) 
                MagicNumber++;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Lightning now hits ALL enemies. Channel {MagicNumber} Lightning.";
        }

        public override Card AddCard()
        {
            return new Electrodynamics();
        }
    }
}