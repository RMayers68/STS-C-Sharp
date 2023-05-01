
namespace STV
{
    public class Glacier : Card
    {
        public Glacier(bool Upgraded = false)
        {
            Name = "Glacier";
            Type = "Skill";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = 2;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            BlockAmount = 7;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            hero.CardBlock(BlockAmount, encounter);
            for (int i = 0; i < 2; i++)
                Orb.ChannelOrb(hero, encounter, 1);
        }

        public override void UpgradeCard()
        {
            if (!Upgraded) 
                BlockAmount += 3;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Gain {BlockAmount} Block. Channel 2 Frost.";
        }

        public override Card AddCard()
        {
            return new Glacier();
        }
    }
}