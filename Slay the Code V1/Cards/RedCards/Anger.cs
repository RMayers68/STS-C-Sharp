namespace STV
{
    public class Anger : Card
    {
        public Anger(bool Upgraded = false)
        {
            Name = "Anger";
            Type = "Attack";
            Rarity = "Common";
            DescriptionModifier = "";
            EnergyCost = 0;
            if (EnergyCost >= 0)
                TmpEnergyCost = EnergyCost;
            GoldCost = CardRNG.Next(45, 56);
            AttackDamage = 6;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage)
        {
            int target = DetermineTarget(encounter);
            hero.Attack(encounter[target], AttackDamage + extraDamage);
            hero.DiscardPile.Add(new Anger(Upgraded));
        }

        public override void UpgradeCard()
        {
            if (!Upgraded)
                AttackDamage += 2;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Deal {AttackDamage} damage. Place a copy of this card in your discard pile.";
        }

        public override Card AddCard()
        {
            return new Anger();
        }
    }
}
