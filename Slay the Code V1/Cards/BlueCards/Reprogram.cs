
namespace STV
{
    public class Reprogram : Card
    {
        public Reprogram(bool Upgraded = false)
        {
            Name = "Reprogram";
            Type = "Skill";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = 1;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            BuffID = 7;
            BuffAmount = 1;     
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            hero.AddBuff(BuffID, -1 * BuffAmount);
            hero.AddBuff(4, BuffAmount);
            hero.AddBuff(9, BuffAmount);
        }

        public override void UpgradeCard()
        {
            if (!Upgraded) 
                BuffAmount++;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Lose {BuffAmount} Focus. Gain {BuffAmount} Strength. Gain {BuffAmount} Dexterity.";
        }

        public override Card AddCard()
        {
            return new Reprogram();
        }
    }
}