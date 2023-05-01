
using STV;

namespace STV
{
    public class JAX : Card
    {
        public JAX(bool Upgraded = false)
        {
            Name = "J.A.X.";
            Type = "Skill";
            Rarity = "Common";
            DescriptionModifier = "";
            EnergyCost = 0;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            BuffID = 4;
            BuffAmount = 2;         
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            hero.SelfDamage(3);
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
            return DescriptionModifier + $"Lose 3 HP. Gain {BuffAmount} Strength.";
        }

        public override Card AddCard()
        {
            return new JAX();
        }
    }
}
