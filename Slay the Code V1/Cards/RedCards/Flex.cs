
namespace STV
{
    public class Flex : Card
    {
        public Flex(bool Upgraded = false)
        {
            Name = "Flex";
            Type = "Skill";
            Rarity = "Common";
            DescriptionModifier = "";
            EnergyCost = 0;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            BuffID = 4;
            BuffAmount = 2;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            hero.AddBuff(BuffID, BuffAmount);
            hero.AddBuff(30, BuffAmount);
        }

        public override void UpgradeCard()
        {
            if (!Upgraded) 
                BuffAmount += 2;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Gain {BuffAmount} Strength. At the end of your turn, lose {BuffAmount} Strength.";
        }

        public override Card AddCard()
        {
            return new Flex();
        }
    }
}