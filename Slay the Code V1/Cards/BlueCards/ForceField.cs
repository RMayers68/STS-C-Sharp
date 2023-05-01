
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
            EnergyCost = ;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            BlockAmount = 12;
            BlockLoops = 1;
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
            return DescriptionModifier + $"Costs 1 less for each Power card played this combat. Gain {BlockAmount} Block.";
        }

        public override Card AddCard()
        {
            return new ForceField();
        }
    }
}