
namespace STV
{
    public class ThunderStrike : Card
    {
        public ThunderStrike(bool Upgraded = false)
        {
            Name = "Thunder Strike";
            Type = "Attack";
            Rarity = "Rare";
            DescriptionModifier = "";
            EnergyCost = 3;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            AttackDamage = 7;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            int thunderStrike = 0;
            foreach (string s in hero.Actions)
                if (s.Contains("Channel Lightning"))
                    thunderStrike++;
            for (int i = 0; i < thunderStrike; i++)
                hero.Attack(encounter[CardRNG.Next(0, encounter.Count)], AttackDamage + extraDamage);
        }

        public override void UpgradeCard()
        {
            if (!Upgraded) 
                AttackDamage += 2;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Deal {AttackDamage} damage to a random enemy for each Lightning Channeled this combat.";
        }

        public override Card AddCard()
        {
            return new ThunderStrike();
        }
    }
}