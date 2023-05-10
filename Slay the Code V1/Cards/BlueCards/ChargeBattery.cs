
namespace STV
{
    public class ChargeBattery : Card
    {
        public ChargeBattery(bool Upgraded = false)
        {
            Name = "Charge Battery";
            Type = "Skill";
            Rarity = "Common";
            DescriptionModifier = "";
            EnergyCost = 1;
            if (EnergyCost >= 0)
                TmpEnergyCost = EnergyCost;
            GoldCost = CardRNG.Next(45, 56);
            BlockAmount = 7;
            BuffID = 22;
            BuffAmount = 1;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            hero.CardBlock(BlockAmount, encounter);
            hero.AddBuff(BuffID, BuffAmount);   
        }

        public override void UpgradeCard()
        {
            if (!Upgraded) 
                BlockAmount += 3;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Gain {BlockAmount} Block. Next turn gain 1 Energy.";
        }

        public override Card AddCard()
        {
            return new ChargeBattery();
        }
    }
}