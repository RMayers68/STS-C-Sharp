
namespace STV
{
    public class Rage : Card
    {
        public Rage(bool Upgraded = false)
        {
            Name = "Rage";
            Type = "Skill";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = ;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            BuffID = 33;
            BuffAmount = 3;
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
            return DescriptionModifier + $"Whenever you play an Attack this turn, gain {BuffAmount} Block.";
        }

        public override Card AddCard()
        {
            return new Rage();
        }
    }
}