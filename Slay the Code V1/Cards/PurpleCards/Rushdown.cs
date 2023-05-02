
namespace STV
{
    public class Rushdown : Card
    {
        public Rushdown(bool Upgraded = false)
        {
            Name = "Rushdown";
            Type = "Power";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = 1;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(68, 83);
            BuffID = 80;
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
                EnergyCost--;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Whenever you enter Wrath, draw 2 cards.";
        }

        public override Card AddCard()
        {
            return new Rushdown();
        }
    }
}