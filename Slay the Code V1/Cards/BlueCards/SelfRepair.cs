
namespace STV
{
    public class SelfRepair : Card
    {
        public SelfRepair(bool Upgraded = false)
        {
            Name = "Self Repair";
            Type = "Power";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = 1;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            BuffID = 65;
            BuffAmount = 7;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            hero.AddBuff(BuffID, BuffAmount);
        }

        public override void UpgradeCard()
        {
            if (!Upgraded) 
                BuffAmount += 3;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"At the end of combat, heal {BuffAmount} HP.";
        }

        public override Card AddCard()
        {
            return new SelfRepair();
        }
    }
}
