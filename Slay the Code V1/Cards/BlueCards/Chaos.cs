
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
            EnergyCost = ;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            BlockLoops = 1;
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
            return DescriptionModifier + $"Channel {BlockLoops} random Orb{(Upgraded ? ";
                }

                public override Card AddCard()
                {
                        return new Chaos();
                }
        }
}
