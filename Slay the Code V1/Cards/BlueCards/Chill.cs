
namespace STV
{
    public class Chill : Card
    {
        public Chill(bool Upgraded = false)
        {
            Name = "Chill";
            Type = "Skill";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = 0;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(68, 83);
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            foreach (Enemy enemy in encounter)
                Orb.ChannelOrb(hero, encounter, 1);
        }

        public override void UpgradeCard()
        {
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"{(Upgraded ? $"Innate. " : "")}Channel 1 Frost for each enemy in combat. Exhaust.";
        }

        public override Card AddCard()
        {
            return new Chill();
        }
    }
}
