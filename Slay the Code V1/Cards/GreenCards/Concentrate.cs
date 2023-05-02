
namespace STV
{
    public class Concentrate : Card
    {
        public Concentrate(bool Upgraded = false)
        {
            Name = "Concentrate";
            Type = "Skill";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = 0;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(68, 83);
            MagicNumber = 3;
            EnergyGained = 2;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            for (int i = 0; i < MagicNumber; i++)
                PickCard(hero.Hand, "discard").Discard(hero, encounter, turnNumber);
            hero.GainTurnEnergy(EnergyGained);
        }

        public override void UpgradeCard()
        {
            if (!Upgraded) 
                MagicNumber--;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Discard {MagicNumber} cards. Gain {EnergyGained} Energy.";
        }

        public override Card AddCard()
        {
            return new Concentrate();
        }
    }
}