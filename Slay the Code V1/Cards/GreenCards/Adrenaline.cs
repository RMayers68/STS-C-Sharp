
namespace STV
{
    public class Adrenaline : Card
    {
        public Adrenaline(bool Upgraded = false)
        {
            Name = "Adrenaline";
            Type = "Skill";
            Rarity = "Rare";
            DescriptionModifier = "";
            EnergyCost = ;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            CardsDrawn = 2;
            EnergyGained = 1;
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
            return DescriptionModifier + $"Gain {EnergyGained} Energy. Draw {CardsDrawn} cards. Exhaust.";
        }

        public override Card AddCard()
        {
            return new Adrenaline();
        }
    }
}