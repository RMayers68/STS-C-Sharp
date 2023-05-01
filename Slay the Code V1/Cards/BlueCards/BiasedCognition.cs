
namespace STV
{
    public class BiasedCognition : Card
    {
        public BiasedCognition(bool Upgraded = false)
        {
            Name = "Biased Cognition";
            Type = "Power";
            Rarity = "Rare";
            DescriptionModifier = "";
            EnergyCost = 1;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            BuffID = 7;
            BuffAmount = 4;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            hero.AddBuff(BuffID, BuffAmount);
            hero.AddBuff(55, 1);
        }

        public override void UpgradeCard()
        {
            if (!Upgraded) 
                BuffAmount++;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Gain {BuffAmount} Focus. At the start of each turn, lose 1 Focus.";
        }

        public override Card AddCard()
        {
            return new BiasedCognition();
        }
    }
}
