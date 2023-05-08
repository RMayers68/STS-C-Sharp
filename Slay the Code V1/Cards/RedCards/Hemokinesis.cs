
namespace STV
{
    public class Hemokinesis : Card
    {
        public Hemokinesis(bool Upgraded = false)
        {
            Name = "Hemokinesis";
            Type = "Attack";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = 1;
            if (EnergyCost >= 0)
                TmpEnergyCost = EnergyCost;
            GoldCost = CardRNG.Next(68, 83);
            AttackDamage = 15;
            MagicNumber = 2;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            hero.SelfDamage(2);
            int target = DetermineTarget(encounter);
            hero.Attack(encounter[target], AttackDamage + extraDamage);
        }

        public override void UpgradeCard()
        {
            if (!Upgraded) 
                AttackDamage += 5;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Lose {MagicNumber} HP. {AttackDamage} damage.";
        }

        public override Card AddCard()
        {
            return new Hemokinesis();
        }
    }
}
