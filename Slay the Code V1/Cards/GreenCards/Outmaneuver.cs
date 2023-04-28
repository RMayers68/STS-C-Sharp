
namespace STV
{
    public class Outmaneuver : Card
    {
        public Outmaneuver(bool Upgraded = false)
        {
            Name = "Outmaneuver";
            Type = "Skill";
            Rarity = "Common";
            DescriptionModifier = "";
            EnergyCost = ;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            BuffID = 22;
            BuffAmount = 2;
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
            return DescriptionModifier + $"Next turn gain {BuffAmount} Energy.";
        }

        public override Card AddCard()
        {
            return new Outmaneuver();
        }
    }
}