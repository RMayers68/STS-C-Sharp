
namespace STV
{
    public class DaggerSpray : Card
    {
        public DaggerSpray(bool Upgraded = false)
        {
            Name = "Dagger Spray";
            Type = "Attack";
            Rarity = "Common";
            DescriptionModifier = "";
            EnergyCost = 1;
            if (EnergyCost >= 0)
                TmpEnergyCost = EnergyCost;
            GoldCost = CardRNG.Next(45, 56);
            AttackDamage = 4;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            for (int i = 0; i < 2; i++)
                foreach (Enemy e in encounter)
                    hero.Attack(e, AttackDamage+extraDamage, encounter);
        }

        public override void UpgradeCard()
        {
            if (!Upgraded) 
                AttackDamage += 2;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Deal {AttackDamage} damage to ALL enemies twice.";
        }

        public override Card AddCard()
        {
            return new DaggerSpray();
        }
    }
}