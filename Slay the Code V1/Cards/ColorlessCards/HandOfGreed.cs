
namespace STV
{
    public class HandofGreed : Card
    {
        public HandofGreed(bool Upgraded = false)
        {
            Name = "Hand of Greed";
            Type = "Attack";
            Rarity = "Rare";
            DescriptionModifier = "";
            EnergyCost = ;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            AttackDamage = 20;
            AttackLoops = 1;
            MagicNumber = 20;
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
            return DescriptionModifier + $"Deal {AttackDamage} damage. If Fatal, gain {MagicNumber} Gold.";
        }

        public override Card AddCard()
        {
            return new HandofGreed();
        }
    }
}