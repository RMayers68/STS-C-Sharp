namespace STV
{
    public class Scrape : Card
    {
        public Scrape(bool Upgraded = false)
        {
            Name = "Scrape";
            Type = "Attack";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = 1;
            if (EnergyCost >= 0)
                TmpEnergyCost = EnergyCost;
            GoldCost = CardRNG.Next(68, 83);
            AttackDamage = 7;
            CardsDrawn = 4;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            int target = DetermineTarget(encounter);
            hero.Attack(encounter[target], AttackDamage + extraDamage);
            for (int i = 0; i < CardsDrawn; i++)
            {
                if (hero.Hand.Count == 10)
                    break;
                hero.DrawCards(1);
                if (hero.Hand.Last().EnergyCost != 0)
                    hero.Hand.Last().MoveCard(hero.Hand, hero.DiscardPile);
            }
        }

        public override void UpgradeCard()
        {
            if (!Upgraded)
            {
                CardsDrawn++;
                AttackDamage += 3;
            }
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Deal {AttackDamage} damage. Draw {CardsDrawn} cards. Discard all cards drawn this way that do not cost 0.";
        }

        public override Card AddCard()
        {
            return new Scrape();
        }
    }
}