
namespace STV
{
    public class Alchemize : Card
    {
        public Alchemize(bool Upgraded = false)
        {
            Name = "Alchemize";
            Type = "Skill";
            Rarity = "Rare";
            DescriptionModifier = "";
            EnergyCost = 1;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(135, 166);
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            hero.Potions.Add(Potion.RandomPotion(hero));
        }

        public override void UpgradeCard()
        {
            if (!Upgraded) 
                EnergyCost--;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Obtain a random potion. Exhaust.";
        }

        public override Card AddCard()
        {
            return new Alchemize();
        }
    }
}