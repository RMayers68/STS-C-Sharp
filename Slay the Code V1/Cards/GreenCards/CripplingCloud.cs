
namespace STV
{
    public class CripplingCloud : Card
    {
        public CripplingCloud(bool Upgraded = false)
        {
            Name = "Crippling Cloud";
            Type = "Skill";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = 2;
            if (EnergyCost >= 0)
                TmpEnergyCost = EnergyCost;
            GoldCost = CardRNG.Next(68, 83);
            BuffID = 39;
            BuffAmount = 4;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            foreach (Enemy e in encounter)
            {
                e.AddBuff(BuffID, BuffAmount,hero);
                hero.AddBuff(1, 2, hero);
            }
        }

        public override void UpgradeCard()
        {
            if (!Upgraded) 
                BuffAmount += 3;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Apply {BuffAmount} Poison and 2 Weak to ALL enemies. Exhaust.";
        }

        public override Card AddCard()
        {
            return new CripplingCloud();
        }
    }
}