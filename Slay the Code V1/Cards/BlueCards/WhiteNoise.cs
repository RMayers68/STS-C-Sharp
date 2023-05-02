
namespace STV
{
    public class WhiteNoise : Card
    {
        public WhiteNoise(bool Upgraded = false)
        {
            Name = "White Noise";
            Type = "Skill";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = 1;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(68, 83);
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            Card whiteNoise = SpecificTypeRandomCard(hero.Name, "Power");
            hero.AddToHand(whiteNoise);
            whiteNoise.TmpEnergyCost = 0;
        }

        public override void UpgradeCard()
        {
            if (!Upgraded) 
                EnergyCost--;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Add a random Power to your hand. It costs 0 this turn. Exhaust.";
        }

        public override Card AddCard()
        {
            return new WhiteNoise();
        }
    }
}
