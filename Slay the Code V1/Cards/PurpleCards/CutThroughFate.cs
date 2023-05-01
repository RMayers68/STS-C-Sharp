
namespace STV
{
    public class CutThroughFate : Card
    {
        public CutThroughFate(bool Upgraded = false)
        {
            Name = "Cut Through Fate";
            Type = "Attack";
            Rarity = "Common";
            DescriptionModifier = "";
            EnergyCost = 1;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            AttackDamage = 7;
            MagicNumber = 2;
            CardsDrawn = 1;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            int target = hero.DetermineTarget(encounter);
            hero.Attack(encounter[target], AttackDamage + extraDamage);
            Scry(hero, MagicNumber);
            hero.DrawCards(CardsDrawn);
        }

        public override void UpgradeCard()
        {
            if (!Upgraded)
            {
                AttackDamage += 2;
                MagicNumber++;
            }
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Deal {AttackDamage} damage. Scry {MagicNumber}. Draw {CardsDrawn} card.";
        }

        public override Card AddCard()
        {
            return new CutThroughFate();
        }
    }
}