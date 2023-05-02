
namespace STV
{
    public class AllForOne : Card
    {
        public AllForOne(bool Upgraded = false)
        {
            Name = "All For One";
            Type = "Attack";
            Rarity = "Rare";
            DescriptionModifier = "";
            EnergyCost = 2;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(135, 166);
            AttackDamage = 10;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            foreach (Enemy e in encounter)
                hero.Attack(e, AttackDamage+extraDamage, encounter);
            foreach (Card zeroCost in hero.DiscardPile)
            {
                if (zeroCost.EnergyCost == 0 && hero.Hand.Count < 10)
                    zeroCost.MoveCard(hero.Hand, hero.DiscardPile);
            }
        }

        public override void UpgradeCard()
        {
            if (!Upgraded) 
                AttackDamage += 4;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Deal {AttackDamage} damage. Put all Cost 0 cards from your discard pile into your hand.";
        }

        public override Card AddCard()
        {
            return new AllForOne();
        }
    }
}
