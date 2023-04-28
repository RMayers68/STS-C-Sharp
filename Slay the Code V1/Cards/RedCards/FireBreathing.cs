
namespace STV
{
    public class FireBreathing : Card
    {
        public FireBreathing(bool Upgraded = false)
        {
            Name = "Fire Breathing";
            Type = "Power";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = ;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            BuffID = 92;
            BuffAmount = 6;
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
            return DescriptionModifier + $"Whenever you draw a Status or Curse card, deal {BuffAmount} damage to ALL enemies.";
        }

        public override Card AddCard()
        {
            return new FireBreathing();
        }
    }
}