
namespace STV
{
    public class ReinforcedBody : Card
    {
        public ReinforcedBody(bool Upgraded = false)
        {
            Name = "Reinforced Body";
            Type = "Skill";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = -1;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(68, 83);
            BlockAmount = 7;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            for (int i = 0; i < hero.Energy; i++)
                hero.CardBlock(BlockAmount, encounter);
            hero.Energy = 0;
        }

        public override void UpgradeCard()
        {
            if (!Upgraded) 
                BlockAmount += 2;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Gain {BlockAmount} Block X times.";
        }

        public override Card AddCard()
        {
            return new ReinforcedBody();
        }
    }
}