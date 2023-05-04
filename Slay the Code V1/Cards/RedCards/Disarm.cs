namespace STV
{
    public class Disarm : Card
    {
        public Disarm(bool Upgraded = false)
        {
            Name = "Disarm";
            Type = "Skill";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = 1;
            if (EnergyCost >= 0)
                TmpEnergyCost = EnergyCost;
            GoldCost = CardRNG.Next(68, 83);
            BuffID = 4;
            BuffAmount = -2;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            int target = hero.DetermineTarget(encounter);
            encounter[target].AddBuff(BuffID, BuffAmount, hero);
        }

        public override void UpgradeCard()
        {
            if (!Upgraded) 
                BuffAmount--;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Enemy loses {-1 * BuffAmount} Strength. Exhaust.";
        }

        public override Card AddCard()
        {
            return new Disarm();
        }
    }
}
