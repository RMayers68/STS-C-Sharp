
namespace STV
{
    public class Crescendo : Card
    {
        public Crescendo(bool Upgraded = false)
        {
            Name = "Crescendo";
            Type = "Skill";
            Rarity = "Common";
            DescriptionModifier = "";
            EnergyCost = 1;
            if (EnergyCost >= 0)
                TmpEnergyCost = EnergyCost;
            GoldCost = CardRNG.Next(45, 56);
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            hero.SwitchStance("Wrath");
        }

        public override void UpgradeCard()
        {
            if (!Upgraded) 
                EnergyCost--;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Retain. Enter Wrath. Exhaust.";
        }

        public override Card AddCard()
        {
            return new Crescendo();
        }
    }
}