
using System.Xml.Linq;

namespace STV
{
    public class Multi_Cast : Card
    {
        public Multi_Cast(bool Upgraded = false)
        {
            Name = "Multi-Cast";
            Type = "Skill";
            Rarity = "Rare";
            DescriptionModifier = "";
            EnergyCost = ;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
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
            return DescriptionModifier + $"Evoke your next Orb ";
        }

        public override Card AddCard()
        {
            return new Multi_Cast();
        }
    }
}