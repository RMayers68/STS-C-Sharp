
namespace STV
{
    public class SwordBoomerang : Card
    {
        public SwordBoomerang(bool Upgraded = false)
        {
            Name = "Sword Boomerang";
            Type = "Attack";
            Rarity = "Common";
            DescriptionModifier = "";
            EnergyCost = 1;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            AttackDamage = 3;
            MagicNumber = 3;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            for (int i = 0; i < MagicNumber; i++)
                hero.Attack(encounter[CardRNG.Next(0, encounter.Count)], AttackDamage + extraDamage);
        }

        public override void UpgradeCard()
        {
            if (!Upgraded) 
                MagicNumber++;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Deal {AttackDamage} damage to a random enemy {MagicNumber} times.";
        }

        public override Card AddCard()
        {
            return new SwordBoomerang();
        }
    }
}