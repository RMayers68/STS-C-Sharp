namespace STV
{
    public class Combust : Card
    {
        public Combust(bool Upgraded = false)
        {
            Name = "Combust";
            Type = "Power";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = ;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            BuffID = 24;
            BuffAmount = 5;
            HeroBuff = true;
            if (upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
        }

        public override void UpgradeCard()
        {
            if (!Upgraded) ;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"At the end of your turn, lose 1 HP and deal {BuffAmount} damage to ALL enemies.";
        }

        public override Card AddCard()
        {
            return new Combust();
        }
    }
}
