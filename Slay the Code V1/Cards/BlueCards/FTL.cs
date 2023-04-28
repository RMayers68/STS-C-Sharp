
namespace STV
{
    public class FTL : Card
    {
        public FTL(bool Upgraded = false)
        {
            Name = "FTL";
            Type = "Attack";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = ;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            AttackDamage = 5;
            AttackLoops = 1;
            MagicNumber = 3;
            CardsDrawn = 1;
            Targetable = true;
            SingleAttack = true;
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
            return DescriptionModifier + $"Deal {AttackDamage} damage. If you have played less than {MagicNumber} cards this turn, draw 1 card.";
        }

        public override Card AddCard()
        {
            return new FTL();
        }
    }
}