
namespace STV
{
    public class Recycle : Card
    {
        public Recycle(bool Upgraded = false)
        {
            Name = "Recycle";
            Type = "Skill";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = 1;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(68, 83);
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            Card recycle = PickCard(hero.Hand, "exhaust");
            recycle.Exhaust(hero, encounter, hero.Hand);
            hero.GainTurnEnergy(recycle.EnergyCost);
        }

        public override void UpgradeCard()
        {
            if (!Upgraded) 
                EnergyCost--;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Exhaust a card. Gain Energy equal to its cost.";
        }

        public override Card AddCard()
        {
            return new Recycle();
        }
    }
}