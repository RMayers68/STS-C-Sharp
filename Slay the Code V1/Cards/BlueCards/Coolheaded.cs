
namespace STV
{
    public class Coolheaded : Card
    {
        public Coolheaded(bool Upgraded = false)
        {
            Name = "Coolheaded";
            Type = "Skill";
            Rarity = "Common";
            DescriptionModifier = "";
            EnergyCost = ;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            BlockLoops = 1;
            MagicNumber = 1;
            CardsDrawn = 1;
            OrbChannels = true;
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
            return DescriptionModifier + $"Channel 1 Frost. Draw {CardsDrawn} card.";
        }

        public override Card AddCard()
        {
            return new Coolheaded();
        }
    }
}