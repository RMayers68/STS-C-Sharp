
namespace STV
{
    public class Chaos : Card
    {
        public Chaos(bool Upgraded = false)
        {
            Name = "Chaos";
            Type = "Skill";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = 1;
            if (EnergyCost >= 0)
                TmpEnergyCost = EnergyCost;
            GoldCost = CardRNG.Next(68, 83);
            MagicNumber = 1;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            for (int i = 0; i < MagicNumber; i++)
                Orb.ChannelOrb(hero, encounter, CardRNG.Next(4));
        }

        public override void UpgradeCard()
        {
            if (!Upgraded) 
                MagicNumber++;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Channel {MagicNumber} random Orb{(Upgraded ? $"s" : "")}.";
        }

        public override Card AddCard()
        {
            return new Chaos();
        }
    }
}
