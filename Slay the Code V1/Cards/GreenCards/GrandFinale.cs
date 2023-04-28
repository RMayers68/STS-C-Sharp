
namespace STV
{
    public class GrandFinale : Card
    {
        public GrandFinale(bool Upgraded = false)
        {
            Name = "Grand Finale";
            Type = "Attack";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = ;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            AttackDamage = 50;
            AttackLoops = 1;
            AttackAll = true;
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
            return DescriptionModifier + $"Can only be played if there are no cards in your draw pile. Deal {AttackDamage} damage to ALL enemies.";
        }

        public override Card AddCard()
        {
            return new GrandFinale();
        }
    }
}