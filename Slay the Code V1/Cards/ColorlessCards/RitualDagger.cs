
namespace STV
{
    public class RitualDagger : Card
    {
        public RitualDagger(bool Upgraded = false)
        {
            Name = "Ritual Dagger";
            Type = "Attack";
            Rarity = "Common";
            DescriptionModifier = "";
            EnergyCost = ;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            AttackDamage = 15;
            AttackLoops = 1;
            MagicNumber = 3;
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
            return DescriptionModifier + $"Deal {AttackDamage} Damage. If this kills an enemy, permanently increase this card's damage by {MagicNumber}. Exhaust.";
        }

        public override Card AddCard()
        {
            return new RitualDagger();
        }
    }
}
