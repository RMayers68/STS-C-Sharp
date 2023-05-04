
namespace STV
{
    public class WildStrike : Card
    {
        public WildStrike(bool Upgraded = false)
        {
            Name = "Wild Strike";
            Type = "Attack";
            Rarity = "Common";
            DescriptionModifier = "";
            EnergyCost = 1;
            if (EnergyCost >= 0)
                TmpEnergyCost = EnergyCost;
            GoldCost = CardRNG.Next(45, 56);
            AttackDamage = 12;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            int target = hero.DetermineTarget(encounter);
            hero.Attack(encounter[target], AttackDamage + extraDamage);
            hero.AddToDrawPile(new Wound(),true);
        }

        public override void UpgradeCard()
        {
            if (!Upgraded) 
                AttackDamage += 5;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Deal {AttackDamage} damage. Shuffle a Wound into your draw pile.";
        }

        public override Card AddCard()
        {
            return new WildStrike();
        }
    }
}