
namespace STV
{
    public class DeusExMachina : Card
    {
        public DeusExMachina(bool Upgraded = false)
        {
            Name = "Deus Ex Machina";
            Type = "Skill";
            Rarity = "Rare";
            DescriptionModifier = "";
            EnergyCost = ;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            MagicNumber = 2;
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
            return DescriptionModifier + $"Unplayable. When you draw this card, add {MagicNumber} Miracles to your hand. Exhaust.";
        }

        public override Card AddCard()
        {
            return new DeusExMachina();
        }
    }
}