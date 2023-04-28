
namespace STV
{
    public class PiercingWail : Card
    {
        public PiercingWail(bool Upgraded = false)
        {
            Name = "Piercing Wail";
            Type = "Skill";
            Rarity = "Common";
            DescriptionModifier = "";
            EnergyCost = ;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            BuffID = 4;
            Targetable = true;
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
            return DescriptionModifier + $"ALL enemies lose {-1 * BuffAmount} Strength for 1 turn. Exhaust.";
        }

        public override Card AddCard()
        {
            return new PiercingWail();
        }
    }
}