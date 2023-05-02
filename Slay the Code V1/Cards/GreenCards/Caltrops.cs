
namespace STV
{
    public class Caltrops : Card
    {
        public Caltrops(bool Upgraded = false)
        {
            Name = "Caltrops";
            Type = "Power";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = 1;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(68, 83);
            BuffID = 41;
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
                BuffAmount += 2;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Whenever you are attacked, deal {BuffAmount} damage to the attacker.";
        }

        public override Card AddCard()
        {
            return new Caltrops();
        }
    }
}