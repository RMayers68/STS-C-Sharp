
namespace STV
{
    public class EndlessAgony : Card
    {
        public EndlessAgony(bool Upgraded = false)
        {
            Name = "Endless Agony";
            Type = "Attack";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = ;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            AttackDamage = 4;
            AttackLoops = 1;
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
            return DescriptionModifier + $"Whenever you draw this card, add a copy of it to your hand. Deal {AttackDamage} damage. Exhaust.";
        }

        public override Card AddCard()
        {
            return new EndlessAgony();
        }
    }
}
