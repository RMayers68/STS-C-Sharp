
namespace STV
{
    public class Feed : Card
    {
        public Feed(bool Upgraded = false)
        {
            Name = "Feed";
            Type = "Attack";
            Rarity = "Rare";
            DescriptionModifier = "";
            EnergyCost = 1;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(135, 166);
            AttackDamage = 10;
            MagicNumber = 3;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            int target = hero.DetermineTarget(encounter);
            hero.Attack(encounter[target], AttackDamage + extraDamage);
            if (encounter[target].Hp <= 0 && !encounter[target].HasBuff("Minion"))
                hero.SetMaxHP(MagicNumber);
        }

        public override void UpgradeCard()
        {
            if (!Upgraded)
            {
                AttackDamage += 2;
                MagicNumber++;
            }
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Deal {AttackDamage} damage. If this kills the enemy, gain {MagicNumber} permanent Max HP. Exhaust.";
        }

        public override Card AddCard()
        {
            return new Feed();
        }
    }
}