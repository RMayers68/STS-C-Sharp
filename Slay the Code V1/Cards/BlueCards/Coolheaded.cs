
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
            EnergyCost = 1;
            if (EnergyCost >= 0)
                TmpEnergyCost = EnergyCost;
            GoldCost = CardRNG.Next(45, 56);
            CardsDrawn = 1;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            Orb.ChannelOrb(hero, encounter, 1);
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
            return DescriptionModifier + $"Channel 1 Frost. Draw {CardsDrawn} card.";
        }

        public override Card AddCard()
        {
            return new Coolheaded();
        }
    }
}