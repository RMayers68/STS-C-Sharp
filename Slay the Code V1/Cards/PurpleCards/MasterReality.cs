
namespace STV
{
    public class MasterReality : Card
    {
        public MasterReality(bool Upgraded = false)
        {
            Name = "Master Reality";
            Type = "Power";
            Rarity = "Rare";
            DescriptionModifier = "";
            EnergyCost = 1;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            BuffID = 78;
            BuffAmount = 1;
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
                EnergyCost--;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Whenever a card is created during combat, Upgrade it.";
        }

        public override Card AddCard()
        {
            return new MasterReality();
        }
    }
}