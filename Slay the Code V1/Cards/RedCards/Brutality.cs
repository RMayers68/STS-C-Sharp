
namespace STV
{
    public class Brutality : Card
    {
        public Brutality(bool Upgraded = false)
        {
            Name = "Brutality";
            Type = "Power";
            Rarity = "Rare";
            DescriptionModifier = "";
            EnergyCost = 0;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            BuffID = 23;
            BuffAmount = 1;
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
            return DescriptionModifier + $"At the start of your turn, lose 1 HP and draw 1 card."
        }
        public override Card AddCard()
        {
            return new Brutality();
        }
    }
}