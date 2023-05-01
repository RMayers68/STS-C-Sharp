
namespace STV
{
    public class DarkShackles : Card
    {
        public DarkShackles(bool Upgraded = false)
        {
            Name = "Dark Shackles";
            Type = "Skill";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = ;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            BuffID = 4;
            Targetable = true;
            EnemyBuff = true;
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
            return DescriptionModifier + $"Enemy loses {-1 * BuffAmount} Strength for the rest of this turn. Exhaust.";
        }

        public override Card AddCard()
        {
            return new DarkShackles();
        }
    }
}