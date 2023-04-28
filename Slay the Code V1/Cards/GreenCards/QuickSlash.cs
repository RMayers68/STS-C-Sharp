
namespace STV
{
    public class QuickSlash : Card
    {
        public QuickSlash(bool Upgraded = false)
        {
            Name = "Quick Slash";
            Type = "Attack";
            Rarity = "Common";
            DescriptionModifier = "";
            EnergyCost = ;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            AttackDamage = 8;
            AttackLoops = 1;
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
            return DescriptionModifier + $"Deal {AttackDamage} damage. Draw 1 card.";
        }

        public override Card AddCard()
        {
            return new QuickSlash();
        }
    }
}