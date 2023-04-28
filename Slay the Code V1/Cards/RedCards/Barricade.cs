namespace STV
{
    public class Barricade : Card
    {
        public Barricade(bool upgraded = false)
        {
            Name = "Barricade";
            Type = "Power";
            Rarity = "Rare";
            DescriptionModifier = "";
            EnergyCost = 3;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(135, 166);
            Upgraded = false;
            if (upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            hero.AddBuff(20, 1);
        }

        public override void UpgradeCard()
        {
            if (!Upgraded)
                EnergyCost--;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Block no longer expires at the start of your turn.";
        }

        public override Card AddCard()
        {
            return new Barricade();
        }
    }
}