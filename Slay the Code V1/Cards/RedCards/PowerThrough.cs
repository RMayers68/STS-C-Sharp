
namespace STV
{
    public class PowerThrough : Card
    {
        public PowerThrough(bool Upgraded = false)
        {
            Name = "Power Through";
            Type = "Skill";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = 1;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            BlockAmount = 20;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            for (int i = 0; i < 2; i++)
                hero.AddToHand(new Wound());
            hero.CardBlock(BlockAmount, encounter);
        }

        public override void UpgradeCard()
        {
            if (!Upgraded) 
                BlockAmount += 5;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Add 2 Wounds to your hand. Gain {BlockAmount} Block.";
        }

        public override Card AddCard()
        {
            return new PowerThrough();
        }
    }
}
