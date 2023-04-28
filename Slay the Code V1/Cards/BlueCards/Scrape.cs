
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
            EnergyCost = ;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            AttackDamage = 7;
            AttackLoops = 1;
            CardsDrawn = 4;
            Targetable = true;
            SingleAttack = true;
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
            return DescriptionModifier + $"Deal {AttackDamage} damage. Draw {CardsDrawn} cards. Discard all cards drawn this way that do not cost 0.";
        }

        public override Card AddCard()
        {
            return new Scrape();
        }
    }
}
