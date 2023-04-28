
namespace STV
{
    public class Rebound : Card
    {
        public Rebound(bool Upgraded = false)
        {
            Name = "Rebound";
            Type = "Attack";
            Rarity = "Common";
            DescriptionModifier = "";
            EnergyCost = ;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            AttackDamage = 9;
            AttackLoops = 1;
            BuffID = 64;
            BuffAmount = 1;
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
            return DescriptionModifier + $"Deal {AttackDamage} damage. Place the next card you play this turn on top of your draw pile.";
        }

        public override Card AddCard()
        {
            return new Rebound();
        }
    }
}