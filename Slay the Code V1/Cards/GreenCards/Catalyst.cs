
namespace STV
{
    public class Catalyst : Card
    {
        public Catalyst(bool Upgraded = false)
        {
            Name = "Catalyst";
            Type = "Skill";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = 1;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(68, 83);
            MagicNumber = 2;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            int target = hero.DetermineTarget(encounter);
            if (encounter[target].FindBuff("Poison") is Buff poison && poison != null)
                encounter[target].AddBuff(39, poison.Intensity * MagicNumber, hero);
        }

        public override void UpgradeCard()
        {
            if (!Upgraded) 
                MagicNumber++;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"{(Upgraded ? $"Triple" : "Double")} an enemy's Poison. Exhaust.";
        }

        public override Card AddCard()
        {
            return new Catalyst();
        }
    }
}