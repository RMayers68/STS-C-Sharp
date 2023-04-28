
namespace STV
{
    public class AThousandCuts : Card
    {
        public AThousandCuts(bool Upgraded = false)
        {
            Name = "A Thousand Cuts";
            Type = "Power";
            Rarity = "Rare";
            DescriptionModifier = "";
            EnergyCost = ;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            BuffID = 35;
            BuffAmount = 1;
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
            return DescriptionModifier + $"Whenever you play a card, deal {BuffAmount} damage to ALL enemies.";
        }

        public override Card AddCard()
        {
            return new AThousandCuts();
        }
    }
}