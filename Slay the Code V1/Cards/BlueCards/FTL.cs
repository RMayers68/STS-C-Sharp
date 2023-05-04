namespace STV
{
    public class FTL : Card
    {
        public FTL(bool Upgraded = false)
        {
            Name = "FTL";
            Type = "Attack";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = 0;
            if (EnergyCost >= 0)
                TmpEnergyCost = EnergyCost;
            GoldCost = CardRNG.Next(68, 83);
            AttackDamage = 5;
            MagicNumber = 3;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            int target = hero.DetermineTarget(encounter);
            hero.Attack(encounter[target], AttackDamage + extraDamage);
            if (hero.FindTurnActions(turnNumber, "Played").Count < MagicNumber)
                hero.DrawCards(1);
        }

        public override void UpgradeCard()
        {
            if (!Upgraded)
            {
                MagicNumber++;
                AttackDamage++;
            }
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Deal {AttackDamage} damage. If you have played less than {MagicNumber} cards this turn, draw 1 card.";
        }

        public override Card AddCard()
        {
            return new FTL();
        }
    }
}