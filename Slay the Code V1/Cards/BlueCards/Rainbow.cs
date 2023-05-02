
namespace STV
{
    public class Rainbow : Card
    {
        public Rainbow(bool Upgraded = false)
        {
            Name = "Rainbow";
            Type = "Skill";
            Rarity = "Rare";
            DescriptionModifier = "";
            EnergyCost = 2;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(135, 166);
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            for (int i = 0; i < 3; i++)
                Orb.ChannelOrb(hero,encounter,i);
        }

        public override void UpgradeCard()
        {
            if (!Upgraded) ;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Channel 1 Lightning, 1 Frost, and 1 Dark. {(Upgraded ? $"" : "Exhaust.")}";
        }

        public override Card AddCard()
        {
            return new Rainbow();
        }
    }
}