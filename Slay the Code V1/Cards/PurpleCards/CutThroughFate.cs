
namespace STV
{
    public class CutThroughFate : Card
    {
        public CutThroughFate(bool Upgraded = false)
        {
            Name = "Cut Through Fate";
            Type = "Attack";
            Rarity = "Common";
            DescriptionModifier = "";
            EnergyCost = ;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            AttackDamage = 7;
            AttackLoops = 1;
            MagicNumber = 2;
            CardsDrawn = 1;
            Targetable = true;
            SingleAttack = true;
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
            return DescriptionModifier + $"Deal {AttackDamage} damage. Scry {MagicNumber}. Draw {CardsDrawn} card.";
        }

        public override Card AddCard()
        {
            return new CutThroughFate();
        }
    }
}