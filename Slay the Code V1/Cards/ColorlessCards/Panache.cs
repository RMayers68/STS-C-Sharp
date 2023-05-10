
namespace STV
{
    public class Panache : Card
    {
        public Panache(bool Upgraded = false)
        {
            Name = "Panache";
            Type = "Power";
            Rarity = "Rare";
            DescriptionModifier = "";
            EnergyCost = 0;
            if (EnergyCost >= 0)
                TmpEnergyCost = EnergyCost;
            GoldCost = CardRNG.Next(135, 166);
            BuffID = 93;
            BuffAmount = 10;
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
                BuffAmount += 4;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Every time you play 5 cards in a single turn, deal {BuffAmount} damage to ALL enemies.";
        }

        public override Card AddCard()
        {
            return new Panache();
        }
    }
}