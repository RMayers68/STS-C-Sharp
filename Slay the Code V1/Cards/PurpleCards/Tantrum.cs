
namespace STV
{
    public class Tantrum : Card
    {
        public Tantrum(bool Upgraded = false)
        {
            Name = "Tantrum";
            Type = "Attack";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = ;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            AttackDamage = 3;
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
            return DescriptionModifier + $"Deal {AttackDamage} damage {AttackLoops} times. Enter Wrath. Shuffle this card into your draw pile.";
        }

        public override Card AddCard()
        {
            return new Tantrum();
        }
    }
}