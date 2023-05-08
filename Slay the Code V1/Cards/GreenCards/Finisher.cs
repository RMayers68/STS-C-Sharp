
namespace STV
{
    public class Finisher : Card
    {
        public Finisher(bool Upgraded = false)
        {
            Name = "Finisher";
            Type = "Attack";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = 1;
            if (EnergyCost >= 0)
                TmpEnergyCost = EnergyCost;
            GoldCost = CardRNG.Next(68, 83);
            AttackDamage = 6;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            int finisher = 0;
            foreach (string s in hero.FindTurnActions(turnNumber,"Attack"))
                if (s.Contains("Attack"))
                    finisher++;
            int target = DetermineTarget(encounter);
            for (int i = 0; i < finisher; i++)
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
            return DescriptionModifier + $"Deal {AttackDamage} damage for each Attack played this turn.";
        }

        public override Card AddCard()
        {
            return new Finisher();
        }
    }
}