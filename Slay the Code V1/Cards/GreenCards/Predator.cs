
namespace STV
{
    public class Predator : Card
    {
        public Predator(bool Upgraded = false)
        {
            Name = "Predator";
            Type = "Attack";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = ;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            AttackDamage = 15;
            AttackLoops = 1;
            BuffID = 45;
            BuffAmount = 2;
            Targetable = true;
            HeroBuff = true;
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
            return DescriptionModifier + $"Deal {AttackDamage} damage. Draw 2 more cards next turn.";
        }

        public override Card AddCard()
        {
            return new Predator();
        }
    }
}