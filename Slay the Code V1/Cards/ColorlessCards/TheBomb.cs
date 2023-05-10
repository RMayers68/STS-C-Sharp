
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
            EnergyCost = 2;
            if (EnergyCost >= 0)
                TmpEnergyCost = EnergyCost;
            GoldCost = CardRNG.Next(135, 166);
            BuffID = 89;
            BuffAmount = 40;
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
            return DescriptionModifier + $"At the end of 3 turns, deal {BuffAmount} damage to ALL enemies.";
        }

        public override Card AddCard()
        {
            return new TheBomb();
        }
    }
}