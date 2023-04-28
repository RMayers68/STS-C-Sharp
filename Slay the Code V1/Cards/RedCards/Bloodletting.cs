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
            GoldCost = CardRNG.Next(45, 56);
            MagicNumber = 3;
            EnergyGained = 2;
            SelfDamage = true;
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
            return DescriptionModifier + $"Lose {MagicNumber} HP. Gain {EnergyGained} Energy.";
        }

        public override Card AddCard()
        {
            return new Bloodletting();
        }
    }
}
