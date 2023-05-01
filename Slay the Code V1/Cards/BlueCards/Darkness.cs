
namespace STV
{
    public class Darkness : Card
    {
        public Darkness(bool Upgraded = false)
        {
            Name = "Darkness";
            Type = "Skill";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = ;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            BlockLoops = 1;
            MagicNumber = 2;
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
            return DescriptionModifier + $"Channel 1 Dark. {(Upgraded ? ";
                }

                public override Card AddCard()
                {
                        return new Darkness();
                }
        }
}