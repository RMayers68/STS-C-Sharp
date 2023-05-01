
namespace STV
{
    public class Claw : Card
    {
        public Claw(bool Upgraded = false)
        {
            Name = "Claw";
            Type = "Attack";
            Rarity = "Common";
            DescriptionModifier = "";
            EnergyCost = 0;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            AttackDamage = 3;
            MagicNumber = 2;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            int target = hero.DetermineTarget(encounter);
            hero.Attack(encounter[target], AttackDamage + extraDamage);
            foreach (Card claw in FindAllCardsInCombat(hero, "Claw"))
                claw.AttackDamage += 2;
        }

        public override void UpgradeCard()
        {
            if (!Upgraded) 
                AttackDamage += 2;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Deal {AttackDamage} damage. All Claw cards deal 2 additional damage this combat.";
        }

        public override Card AddCard()
        {
            return new Claw();
        }
    }
}