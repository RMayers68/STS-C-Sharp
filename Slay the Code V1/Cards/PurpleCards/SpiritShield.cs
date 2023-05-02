
namespace STV
{
    public class SpiritShield : Card
    {
        public SpiritShield(bool Upgraded = false)
        {
            Name = "Spirit Shield";
            Type = "Skill";
            Rarity = "Rare";
            DescriptionModifier = "";
            EnergyCost = 2;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(135, 166);
            MagicNumber = 3;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            hero.CardBlock(hero.Hand.Count * MagicNumber);
        }

        public override void UpgradeCard()
        {
            if (!Upgraded) 
                MagicNumber++;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Gain {MagicNumber} Block for each card in your hand.";
        }

        public override Card AddCard()
        {
            return new SpiritShield();
        }
    }
}