
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
            EnergyCost = -2;
            if (EnergyCost >= 0)
                TmpEnergyCost = EnergyCost;
            GoldCost = CardRNG.Next(135, 166);
            MagicNumber = 2;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            return;
        }

        public override void UpgradeCard()
        {
            if (!Upgraded) 
                MagicNumber++;
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