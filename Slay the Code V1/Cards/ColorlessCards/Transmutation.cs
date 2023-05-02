
namespace STV
{
    public class Transmutation : Card
    {
        public Transmutation(bool Upgraded = false)
        {
            Name = "Transmutation";
            Type = "Skill";
            Rarity = "Rare";
            DescriptionModifier = "";
            EnergyCost = -1;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(135, 166);
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            Card transmutation;
            for (int i = 0; i < hero.Energy; i++)
            {
                transmutation = RandomCard("Colorless");
                if (Upgraded)
                    transmutation.UpgradeCard();
                transmutation.SetTmpEnergyCost(0);
                hero.AddToHand( transmutation);
            }
            hero.Energy = 0;
        }

        public override void UpgradeCard()
        {
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Add X random {(Upgraded ? $"Upgraded" : "")} colorless cards into your hand. They cost 0 this turn. Exhaust.";
        }

        public override Card AddCard()
        {
            return new Transmutation();
        }
    }
}
