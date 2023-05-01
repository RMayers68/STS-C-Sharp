
namespace STV
{
    public class Whirlwind : Card
    {
        public Whirlwind(bool Upgraded = false)
        {
            Name = "Whirlwind";
            Type = "Attack";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = -1;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            AttackDamage = 5;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            for (int i = 0; i < hero.Energy; i++)
                foreach (Enemy e in encounter)
                    hero.Attack(e, AttackDamage, encounter);
            hero.Energy = 0;
        }

        public override void UpgradeCard()
        {
            if (!Upgraded) 
                AttackDamage += 3;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Deal {AttackDamage} damage to ALL enemies X times.";
        }

        public override Card AddCard()
        {
            return new Whirlwind();
        }
    }
}