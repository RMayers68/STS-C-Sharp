
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
            EnergyCost = 0;
            if (EnergyCost >= 0)
                TmpEnergyCost = EnergyCost;
            GoldCost = CardRNG.Next(68, 83);
            BuffID = 4;
            BuffAmount = -9;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            int target = DetermineTarget(encounter);
            encounter[target].AddBuff(BuffID, BuffAmount, hero);
            encounter[target].AddBuff(30, BuffAmount, hero);
        }

        public override void UpgradeCard()
        {
            if (!Upgraded) 
                BuffAmount -= 6;
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