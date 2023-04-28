
namespace STV
{
    public class GhostlyArmor : Card
    {
        public GhostlyArmor(bool Upgraded = false)
        {
            Name = "Ghostly Armor";
            Type = "Skill";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = ;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            BlockAmount = 10;
            BlockLoops = 1;
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
            return DescriptionModifier + $"Ethereal. Gain {BlockAmount} Block.";
        }

        public override Card AddCard()
        {
            return new GhostlyArmor();
        }
    }
}