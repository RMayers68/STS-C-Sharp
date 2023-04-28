

namespace STV
{
    public class WheelKick : Card
    {
        public WheelKick(bool Upgraded = false)
        {
            Name = "Wheel Kick";
            Type = "Attack";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = ;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            AttackDamage = 15;
            AttackLoops = 1;
            CardsDrawn = 2;
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
            return DescriptionModifier + $"Deal {AttackDamage} damage. Draw 2 cards.";
        }

        public override Card AddCard()
        {
            return new WheelKick();
        }
    }
}
