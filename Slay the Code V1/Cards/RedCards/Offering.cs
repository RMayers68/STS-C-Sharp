
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
            EnergyCost = 0;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(135, 166);
            CardsDrawn = 3;
            EnergyGained = 2;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            hero.SelfDamage(6);
            hero.GainTurnEnergy(EnergyGained);
            hero.DrawCards(CardsDrawn);
        }

        public override void UpgradeCard()
        {
            if (!Upgraded) 
                CardsDrawn += 2;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Lose 6 HP. Gain {EnergyGained} Energy. Draw {CardsDrawn} cards. Exhaust.";
        }

        public override Card AddCard()
        {
            return new Offering();
        }
    }
}