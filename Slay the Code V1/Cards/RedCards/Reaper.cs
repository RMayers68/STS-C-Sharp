
namespace STV
{
    public class Reaper : Card
    {
        public Reaper(bool Upgraded = false)
        {
            Name = "Reaper";
            Type = "Attack";
            Rarity = "Rare";
            DescriptionModifier = "";
            EnergyCost = 2;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            AttackDamage = 4;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            int reaper = 0, enemyHP = 0;
            foreach (Enemy e in encounter)
            {
                enemyHP += e.Hp;
                hero.Attack(e, AttackDamage, encounter);
                reaper += e.Hp;
            }
            hero.CombatHeal(enemyHP - reaper);
        }

        public override void UpgradeCard()
        {
            if (!Upgraded) 
                AttackDamage++;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Deal {AttackDamage} damage to ALL enemies. Heal for unblocked damage. Exhaust.";
        }

        public override Card AddCard()
        {
            return new Reaper();
        }
    }
}
