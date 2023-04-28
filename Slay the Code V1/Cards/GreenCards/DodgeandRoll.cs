
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
            EnergyCost = ;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            BlockAmount = 4;
            BlockLoops = 1;
            BuffID = 44;
            BuffAmount = 4;
            HeroBuff = true;
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
            return DescriptionModifier + $"Gain {BlockAmount} Block. Next turn gain {BuffAmount} Block.";
        }

        public override Card AddCard()
        {
            return new DodgeandRoll();
        }
    }
}