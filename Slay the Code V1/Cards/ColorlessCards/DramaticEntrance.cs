
namespace STV
{
    public class DramaticEntrance : Card
    {
        public DramaticEntrance(bool Upgraded = false)
        {
            Name = "Dramatic Entrance";
            Type = "Attack";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = 0;
            if (EnergyCost >= 0)
                TmpEnergyCost = EnergyCost;
            GoldCost = CardRNG.Next(68, 83);
            AttackDamage = 8;
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
            return DescriptionModifier + $"Innate. Deal {AttackDamage} damage to ALL enemies. Exhaust.";
        }

        public override Card AddCard()
        {
            return new DramaticEntrance();
        }
    }
}