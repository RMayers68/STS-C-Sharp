
namespace STV
{
    public class Brutality : Card
    {
        public Brutality(bool Upgraded = false)
        {
            Name = "Brutality";
            Type = "Power";
            Rarity = "Rare";
            DescriptionModifier = "";
            EnergyCost = 0;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(135, 166);
            BuffID = 23;
            BuffAmount = 1;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            hero.AddBuff(BuffID, BuffAmount);
        }

        public override void UpgradeCard()
        {
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"{(Upgraded ? $"Innate. " : "")}At the start of your turn, lose 1 HP and draw 1 card.";
        }
        public override Card AddCard()
        {
            return new Brutality();
        }
    }
}