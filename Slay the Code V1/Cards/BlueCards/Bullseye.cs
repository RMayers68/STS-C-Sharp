namespace STV
{
    public class Bullseye : Card
    {
        public Bullseye(bool Upgraded = false)
        {
            Name = "Bullseye";
            Type = "Attack";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = 1;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(68, 83);
            AttackDamage = 8;
            BuffID = 57;
            BuffAmount = 2;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            int target = hero.DetermineTarget(encounter);
            hero.Attack(encounter[target], AttackDamage + extraDamage);
            encounter[target].AddBuff(BuffID, BuffAmount,hero);
        }

        public override void UpgradeCard()
        {
            if (!Upgraded)
            {
                BuffAmount++;
                AttackDamage += 3;
            }
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Deal {AttackDamage} damage. Apply {BuffAmount} Lock-On.";
        }

        public override Card AddCard()
        {
            return new Bullseye();
        }
    }
}