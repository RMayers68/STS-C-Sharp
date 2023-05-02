
namespace STV
{
    public class Distraction : Card
    {
        public Distraction(bool Upgraded = false)
        {
            Name = "Distraction";
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
            Card distraction = SpecificTypeRandomCard(hero.Name, "Skill");
            hero.AddToHand(distraction);
            distraction.TmpEnergyCost = 0;
        }

        public override void UpgradeCard()
        {
            if (!Upgraded) 
                EnergyCost--;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Add a random Skill to your hand. It costs 0 this turn. Exhaust.";
        }

        public override Card AddCard()
        {
            return new Distraction();
        }
    }
}