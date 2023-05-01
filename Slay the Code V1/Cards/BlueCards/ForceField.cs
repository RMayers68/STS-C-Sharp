
namespace STV
{
    public class ForceField : Card
    {
        public ForceField(bool Upgraded = false)
        {
            Name = "Force Field";
            Type = "Skill";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = 4;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            BlockAmount = 12;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            hero.CardBlock(BlockAmount, encounter);
        }

        public override void UpgradeCard()
        {
            if (!Upgraded) 
                BlockAmount += 4;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Costs 1 less for each Power card played this combat. Gain {BlockAmount} Block.";
        }

        public override Card AddCard()
        {
            return new ForceField();
        }
    }
}