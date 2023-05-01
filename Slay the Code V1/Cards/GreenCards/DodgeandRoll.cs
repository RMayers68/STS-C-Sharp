
namespace STV
{
    public class DodgeandRoll : Card
    {
        public DodgeandRoll(bool Upgraded = false)
        {
            Name = "Dodge and Roll";
            Type = "Skill";
            Rarity = "Common";
            DescriptionModifier = "";
            EnergyCost = 1;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            BlockAmount = 4;
            BuffID = 44;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            hero.CardBlock(BlockAmount, encounter);
            hero.AddBuff(BuffID, BlockAmount);
        }

        public override void UpgradeCard()
        {
            if (!Upgraded) 
                BlockAmount += 2;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Gain {BlockAmount} Block. Next turn gain {BlockAmount} Block.";
        }

        public override Card AddCard()
        {
            return new DodgeandRoll();
        }
    }
}