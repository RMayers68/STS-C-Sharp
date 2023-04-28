
namespace STV
{
    public class Eviscerate : Card
    {
        public Eviscerate(bool Upgraded = false)
        {
            Name = "Eviscerate";
            Type = "Attack";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = ;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            AttackDamage = 7;
            AttackLoops = 3;
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
            return DescriptionModifier + $"Costs 1 less Energy for each card discarded this turn. Deal {AttackDamage} damage three times.";
        }

        public override Card AddCard()
        {
            return new Eviscerate();
        }
    }
}