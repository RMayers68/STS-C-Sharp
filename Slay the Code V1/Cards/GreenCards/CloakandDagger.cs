
namespace STV
{
    public class CloakAndDagger : Card
    {
        public CloakAndDagger(bool Upgraded = false)
        {
            Name = "Cloak And Dagger";
            Type = "Skill";
            Rarity = "Common";
            DescriptionModifier = "";
            EnergyCost = 1;
            if (EnergyCost >= 0)
                TmpEnergyCost = EnergyCost;
            GoldCost = CardRNG.Next(45, 56);
            BlockAmount = 6;
            MagicNumber = 1;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            hero.CardBlock(BlockAmount, encounter);
            AddShivs(hero, MagicNumber);
        }

        public override void UpgradeCard()
        {
            if (!Upgraded) 
                MagicNumber++;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Gain {BlockAmount} Block. Add {MagicNumber} Shiv to your hand.";
        }

        public override Card AddCard()
        {
            return new CloakAndDagger();
        }
    }
}