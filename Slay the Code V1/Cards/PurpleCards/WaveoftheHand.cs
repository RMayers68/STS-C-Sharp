
namespace STV
{
    public class WaveoftheHand : Card
    {
        public WaveoftheHand(bool Upgraded = false)
        {
            Name = "Wave of the Hand";
            Type = "Skill";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = ;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            BuffID = 84;
            BuffAmount = 1;
            HeroBuff = true;
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
            return DescriptionModifier + $"Whenever you gain Block this turn, apply {BuffAmount} Weak to ALL enemies.";
        }

        public override Card AddCard()
        {
            return new WaveoftheHand();
        }
    }
}