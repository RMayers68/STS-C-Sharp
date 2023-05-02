
namespace STV
{
    public class Conclude : Card
    {
        public Conclude(bool Upgraded = false)
        {
            Name = "Conclude";
            Type = "Attack";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = 1;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(68, 83);
            AttackDamage = 12;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            foreach (Enemy e in encounter)
                hero.Attack(e, AttackDamage + extraDamage, encounter);
        }

        public override void UpgradeCard()
        {
            if (!Upgraded) 
                AttackDamage += 4;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Deal {AttackDamage} damage to ALL enemies. End your turn.";
        }

        public override Card AddCard()
        {
            return new Conclude();
        }
    }
}