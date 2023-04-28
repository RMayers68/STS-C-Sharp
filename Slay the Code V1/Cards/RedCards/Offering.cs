
namespace STV
{
    public class Offering : Card
    {
        public Offering(bool Upgraded = false)
        {
            Name = "Offering";
            Type = "Skill";
            Rarity = "Rare";
            DescriptionModifier = "";
            EnergyCost = ;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            MagicNumber = 6;
            CardsDrawn = 3;
            EnergyGained = 2;
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
            return DescriptionModifier + $"Lose {MagicNumber} HP. Gain {EnergyGained} Energy. Draw {CardsDrawn} cards. Exhaust.";
        }

        public override Card AddCard()
        {
            return new Offering();
        }
    }
}