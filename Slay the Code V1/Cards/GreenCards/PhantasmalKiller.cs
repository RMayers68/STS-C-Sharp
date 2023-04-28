us
namespace STV
{
    public class PhantasmalKiller : Card
    {
        public PhantasmalKiller(bool Upgraded = false)
        {
            Name = "Phantasmal Killer";
            Type = "Skill";
            Rarity = "Rare";
            DescriptionModifier = "";
            EnergyCost = ;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            BuffID = 49;
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
            return DescriptionModifier + $"On your next turn, your Attack damage is doubled.";
        }

        public override Card AddCard()
        {
            return new PhantasmalKiller();
        }
    }
}