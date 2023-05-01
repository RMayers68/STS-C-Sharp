
namespace STV
{
    public class InfernalBlade : Card
    {
        public InfernalBlade(bool Upgraded = false)
        {
            Name = "Infernal Blade";
            Type = "Skill";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = 1;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            Card infernalBlade = SpecificTypeRandomCard(hero.Name, "Attack");
            hero.AddToHand(infernalBlade);
            infernalBlade.TmpEnergyCost = 0;
        }

        public override void UpgradeCard()
        {
            if (!Upgraded) 
                EnergyCost--;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Add a random Attack to your hand. It costs 0 this turn. Exhaust.";
        }

        public override Card AddCard()
        {
            return new InfernalBlade();
        }
    }
}
