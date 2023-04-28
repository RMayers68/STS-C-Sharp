
namespace STV
{
    public class DaggerThrow : Card
    {
        public DaggerThrow(bool Upgraded = false)
        {
            Name = "Dagger Throw";
            Type = "Attack";
            Rarity = "Common";
            DescriptionModifier = "";
            EnergyCost = ;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            AttackDamage = 9;
            AttackLoops = 1;
            MagicNumber = 1;
            CardsDrawn = 1;
            Targetable = true;
            SingleAttack = true;
            Discards = true;
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
            return DescriptionModifier + $"Deal {AttackDamage} damage. Draw {CardsDrawn} card. Discard {MagicNumber} card.";
        }

        public override Card AddCard()
        {
            return new DaggerThrow();
        }
    }
}