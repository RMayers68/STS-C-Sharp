namespace STV
{
    public class Bloodletting : Card
    {
        public Bloodletting(bool Upgraded = false)
        {
            Name = "Bloodletting";
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
            hero.SelfDamage(3);
            hero.GainTurnEnergy(EnergyGained);
        }

        public override void UpgradeCard()
        {
            if (!Upgraded) 
                EnergyGained++;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Lose {MagicNumber} HP. Gain {EnergyGained} Energy.";
        }

        public override Card AddCard()
        {
            return new Bloodletting();
        }
    }
}
