
namespace STV
{
    public class Glacier : Card
    {
        public Glacier(bool Upgraded = false)
        {
            Name = "Glacier";
            Type = "Skill";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = ;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            BlockAmount = 7;
            BlockLoops = 1;
            MagicNumber = 1;
            OrbChannels = true;
            if (upgraded)
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
            return DescriptionModifier + $"Gain {BlockAmount} Block. Channel 2 Frost.";
        }

        public override Card AddCard()
        {
            return new Glacier();
        }
    }
}