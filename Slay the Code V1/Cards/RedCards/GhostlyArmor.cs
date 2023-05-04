
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
            EnergyCost = 1;
            if (EnergyCost >= 0)
                TmpEnergyCost = EnergyCost;
            GoldCost = CardRNG.Next(68, 83);
            BlockAmount = 10;
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
                BlockAmount += 3;
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