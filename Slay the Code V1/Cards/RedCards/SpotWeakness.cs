
namespace STV
{
    public class SpotWeakness : Card
    {
        public SpotWeakness(bool Upgraded = false)
        {
            Name = "Spot Weakness";
            Type = "Skill";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = 1;
            if (EnergyCost >= 0)
                TmpEnergyCost = EnergyCost;
            GoldCost = CardRNG.Next(68, 83);
            BuffID = 4;
            BuffAmount = 3;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            int target = hero.DetermineTarget(encounter);
            if (Enemy.AttackIntents().Contains(encounter[target].Intent))
                hero.AddBuff(BuffID, BuffAmount);
        }

        public override void UpgradeCard()
        {
            if (!Upgraded) 
                BuffAmount++;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"If an enemy intends to attack, gain {BuffAmount} Strength.";
        }

        public override Card AddCard()
        {
            return new SpotWeakness();
        }
    }
}