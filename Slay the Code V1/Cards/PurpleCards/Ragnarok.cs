
namespace STV
{
    public class Ragnarok : Card
    {
        public Ragnarok(bool Upgraded = false)
        {
            Name = "Ragnarok";
            Type = "Attack";
            Rarity = "Rare";
            DescriptionModifier = "";
            EnergyCost = 3;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(135, 166);
            AttackDamage = 5;
            MagicNumber = 5;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            for (int i = 0; i < MagicNumber; i++)
            hero.Attack(encounter[CardRNG.Next(encounter.Count)], AttackDamage + extraDamage);
        }

        public override void UpgradeCard()
        {
            if (!Upgraded)
            {
                AttackDamage++;
                MagicNumber++;
            }
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Deal {AttackDamage} damage to a random enemy {MagicNumber} times.";
        }

        public override Card AddCard()
        {
            return new Ragnarok();
        }
    }
}
