
namespace STV
{
    public class Intimidate : Card
    {
        public Intimidate(bool Upgraded = false)
        {
            Name = "Intimidate";
            Type = "Skill";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = 0;
            if (EnergyCost >= 0)
                TmpEnergyCost = EnergyCost;
            GoldCost = CardRNG.Next(68, 83);
            BuffID = 2;
            BuffAmount = 1;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            foreach (Enemy enemy in encounter)
                enemy.AddBuff(BuffID, BuffAmount, hero);
        }

        public override void UpgradeCard()
        {
            if (!Upgraded) 
                BuffAmount++;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Apply {BuffAmount} Weak to ALL enemies. Exhaust.";
        }

        public override Card AddCard()
        {
            return new Intimidate();
        }
    }
}