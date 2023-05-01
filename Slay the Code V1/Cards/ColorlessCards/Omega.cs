
namespace STV
{
    public class Omega : Card
    {
        public Omega(bool Upgraded = false)
        {
            Name = "Omega";
            Type = "Power";
            Rarity = "Common";
            DescriptionModifier = "";
            EnergyCost = 3;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            BuffID = 94;
            BuffAmount = 50;
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
                BuffAmount += 10;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"At the start of your turn deal {BuffAmount} damage to ALL enemies.";
        }

        public override Card AddCard()
        {
            return new Omega();
        }
    }
}