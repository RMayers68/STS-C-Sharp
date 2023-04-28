
namespace STV
{
    public class TheBomb : Card
    {
        public TheBomb(bool Upgraded = false)
        {
            Name = "The Bomb";
            Type = "Skill";
            Rarity = "Rare";
            DescriptionModifier = "";
            EnergyCost = ;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            BuffID = 89;
            BuffAmount = 40;
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
            return DescriptionModifier + $"At the end of 3 turns, deal {BuffAmount} damage to ALL enemies.";
        }

        public override Card AddCard()
        {
            return new TheBomb();
        }
    }
}