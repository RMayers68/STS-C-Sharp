
namespace STV
{
    public class DaggerThrow : Card
    {
        public DaggerThrow(bool Upgraded = false)
        {
            Name = "Dagger Throw";
            Type = "Attack";
            Rarity = "Common";
            DescriptionModifier = "";
            EnergyCost = 1;
            if (EnergyCost >= 0)
                TmpEnergyCost = EnergyCost;
            GoldCost = CardRNG.Next(45, 56);
            AttackDamage = 9;
            CardsDrawn = 1;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            int target = DetermineTarget(encounter);
            hero.Attack(encounter[target], AttackDamage + extraDamage);
            hero.DrawCards(CardsDrawn);
            PickCard(hero.Hand,"discard").Discard(hero,encounter,turnNumber);
        }

        public override void UpgradeCard()
        {
            if (!Upgraded) 
                AttackDamage += 3;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Deal {AttackDamage} damage. Draw {CardsDrawn} card. Discard {MagicNumber} card.";
        }

        public override Card AddCard()
        {
            return new DaggerThrow();
        }
    }
}