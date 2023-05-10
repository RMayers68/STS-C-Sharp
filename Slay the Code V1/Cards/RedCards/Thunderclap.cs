
namespace STV
{
    public class Thunderclap : Card
    {
        public Thunderclap(bool Upgraded = false)
        {
            Name = "Thunderclap";
            Type = "Attack";
            Rarity = "Common";
            DescriptionModifier = "";
            EnergyCost = 1;
            if (EnergyCost >= 0)
                TmpEnergyCost = EnergyCost;
            GoldCost = CardRNG.Next(45, 56);
            AttackDamage = 4;
            BuffID = 1;
            BuffAmount = 1;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            foreach (Enemy e in encounter)
            {
                hero.Attack(e, AttackDamage+extraDamage, encounter);
                hero.AddBuff(BuffID, BuffAmount, hero);
            }             
        }

        public override void UpgradeCard()
        {
            if (!Upgraded) 
                AttackDamage += 3;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Deal {AttackDamage} damage and apply {BuffAmount} Vulnerable to ALL enemies.";
        }

        public override Card AddCard()
        {
            return new Thunderclap();
        }
    }
}