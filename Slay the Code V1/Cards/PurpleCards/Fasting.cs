
namespace STV
{
    public class Fasting : Card
    {
        public Fasting(bool Upgraded = false)
        {
            Name = "Fasting";
            Type = "Power";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = 2;
            if (EnergyCost >= 0)
                TmpEnergyCost = EnergyCost;
            GoldCost = CardRNG.Next(68, 83);
            BuffID = 4;
            BuffAmount = 3;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            hero.AddBuff(BuffID, BuffAmount);
            hero.AddBuff(9, BuffAmount);
            hero.MaxEnergy--;

        }

        public override void UpgradeCard()
        {
            if (!Upgraded) 
                BuffAmount++;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Gain {BuffAmount} Strength. Gain {BuffAmount} Dexterity. Gain 1 less Energy at the start of each turn.";
        }

        public override Card AddCard()
        {
            return new Fasting();
        }
    }
}