
namespace STV
{
    public class SimmeringFury : Card
    {
        public SimmeringFury(bool Upgraded = false)
        {
            Name = "Simmering Fury";
            Type = "Skill";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = ;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            BuffID = 81;
            BuffAmount = 2;
            HeroBuff = true;
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
            return DescriptionModifier + $"At the start of your next turn, enter Wrath and draw {BuffAmount} cards.";
        }

        public override Card AddCard()
        {
            return new SimmeringFury();
        }
    }
}
