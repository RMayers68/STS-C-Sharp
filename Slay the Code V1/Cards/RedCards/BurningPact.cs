namespace STV
{
    public class BurningPact : Card
    {
        public BurningPact(bool Upgraded = false)
        {
            Name = "Burning Pact";
            Type = "Skill";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = 1;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(68, 83);
            CardsDrawn = 2;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            PickCard(hero.Hand, "exhaust").Exhaust(hero, encounter, hero.Hand);
            hero.DrawCards(CardsDrawn);
        }

        public override void UpgradeCard()
        {
            if (!Upgraded) 
                CardsDrawn++;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Exhaust 1 card. Draw {CardsDrawn} cards.";
        }

        public override Card AddCard()
        {
            return new BurningPact();
        }
    }
}
