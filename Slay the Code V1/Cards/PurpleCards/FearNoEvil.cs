
namespace STV
{
    public class FearNoEvil : Card
    {
        public FearNoEvil(bool Upgraded = false)
        {
            Name = "Fear No Evil";
            Type = "Attack";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = 1;
            if (EnergyCost >= 0)
                TmpEnergyCost = EnergyCost;
            GoldCost = CardRNG.Next(68, 83);
            AttackDamage = 8;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            int target = DetermineTarget(encounter);
            hero.Attack(encounter[target], AttackDamage + extraDamage);
            if (Enemy.AttackIntents().Contains(encounter[target].Intent))
                hero.SwitchStance("Calm");
        }

        public override void UpgradeCard()
        {
            if (!Upgraded) 
                AttackDamage += 3;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Deal {AttackDamage} damage. If the enemy intends to Attack, enter Calm.";
        }

        public override Card AddCard()
        {
            return new FearNoEvil();
        }
    }
}