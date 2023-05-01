
namespace STV
{
    public class Mayhem : Card
    {
        public Mayhem(bool Upgraded = false)
        {
            Name = "Mayhem";
            Type = "Power";
            Rarity = "Rare";
            DescriptionModifier = "";
            EnergyCost = 2;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            BuffID = 88;
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
            return DescriptionModifier + $"At the start of your turn, play the top card of your draw pile.";
        }

        public override Card AddCard()
        {
            return new Mayhem();
        }
    }
}