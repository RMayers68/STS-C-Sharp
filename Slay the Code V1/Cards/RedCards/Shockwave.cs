
namespace STV
{
    public class Shockwave : Card
    {
        public Shockwave(bool Upgraded = false)
        {
            Name = "Shockwave";
            Type = "Skill";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = 2;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            BuffID = 2;
            BuffAmount = 3;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            foreach (Enemy e in encounter)
            {
                e.AddBuff(BuffID, BuffAmount, hero);
                e.AddBuff(1,BuffAmount, hero);
            }
        }

        public override void UpgradeCard()
        {
            if (!Upgraded) 
                BuffAmount += 2;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Apply {BuffAmount} Weak and Vulnerable to ALL enemies. Exhaust.";
        }

        public override Card AddCard()
        {
            return new Shockwave();
        }
    }
}