
namespace STV
{
    public class Indignation : Card
    {
        public Indignation(bool Upgraded = false)
        {
            Name = "Indignation";
            Type = "Skill";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = ;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            BuffID = 1;
            BuffAmount = 3;
            EnemyBuff = true;
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
            return DescriptionModifier + $"If you are in Wrath, apply {BuffAmount} Vulnerable to ALL enemies, otherwise enter Wrath.";
        }

        public override Card AddCard()
        {
            return new Indignation();
        }
    }
}