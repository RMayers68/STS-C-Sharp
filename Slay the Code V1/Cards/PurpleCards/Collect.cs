
namespace STV
{
    public class Collect : Card
    {
        public Collect(bool Upgraded = false)
        {
            Name = "Collect";
            Type = "Skill";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = -1;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            BuffID = 72;
            MagicNumber = 0;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            hero.AddBuff(BuffID, hero.Energy + MagicNumber);
            hero.Energy = 0;
        }

        public override void UpgradeCard()
        {
            if (!Upgraded) 
                MagicNumber++;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Put a Miracle+ into your hand at the start of your next X{(Upgraded ? $"+1" : "")} turns. Exhaust";
        }

        public override Card AddCard()
        {
            return new Collect();
        }
    }
}