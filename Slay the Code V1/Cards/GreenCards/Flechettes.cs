
namespace STV
{
    public class Flechettes : Card
    {
        public Flechettes(bool Upgraded = false)
        {
            Name = "Flechettes";
            Type = "Attack";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = 1;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            AttackDamage = 4;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            int flechettes = 0;
            for (int i = 0; i < hero.Hand.Count; i++)
            {
                if (hero.Hand[i].Type == "Skill")
                    flechettes++;
            }
            int target = hero.DetermineTarget(encounter);
            for (int i = 0; i < flechettes; i++)
                hero.Attack(encounter[target], AttackDamage + extraDamage);
        }

        public override void UpgradeCard()
        {
            if (!Upgraded) 
                AttackDamage += 2;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Deal {AttackDamage} damage for each Skill in your hand.";
        }

        public override Card AddCard()
        {
            return new Flechettes();
        }
    }
}