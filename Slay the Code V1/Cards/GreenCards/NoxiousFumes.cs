
namespace STV
{
    public class NoxiousFumes : Card
    {
        public NoxiousFumes(bool Upgraded = false)
        {
            Name = "Noxious Fumes";
            Type = "Power";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = 1;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(68, 83);
            BuffID = 48;
            BuffAmount = 2;
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
                BuffAmount++;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"At the start of your turn, apply {BuffAmount} Poison to ALL enemies.";
        }

        public override Card AddCard()
        {
            return new NoxiousFumes();
        }
    }
}
