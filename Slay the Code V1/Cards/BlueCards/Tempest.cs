
namespace STV
{
    public class Tempest : Card
    {
        public Tempest(bool Upgraded = false)
        {
            Name = "Tempest";
            Type = "Skill";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = -1;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            MagicNumber = 0;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            for (int i = 0; i < hero.Energy + MagicNumber; i++)
                Orb.ChannelOrb(hero, encounter, i);
            hero.Energy = 0;
        }

        public override void UpgradeCard()
        {
            if (!Upgraded) 
                MagicNumber++;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Channel X{(Upgraded ? $"+1" : "")} Lightning. Exhaust.";
        }

        public override Card AddCard()
        {
            return new Tempest();
        }
    }
}