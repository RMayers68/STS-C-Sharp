
namespace STV
{
    public class Thunderclap : Card
    {
        public Thunderclap(bool Upgraded = false)
        {
            Name = "Thunderclap";
            Type = "Attack";
            Rarity = "Common";
            DescriptionModifier = "";
            EnergyCost = ;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            AttackDamage = 4;
            AttackLoops = 1;
            BuffID = 1;
            BuffAmount = 1;
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
            return DescriptionModifier + $"Deal {AttackDamage} damage and apply {BuffAmount} Vulnerable to ALL enemies.";
        }

        public override Card AddCard()
        {
            return new Thunderclap();
        }
    }
}