
namespace STV
{
    public class Wish : Card
    {
        public Wish(bool Upgraded = false)
        {
            Name = "Wish";
            Type = "Skill";
            Rarity = "Rare";
            DescriptionModifier = "";
            EnergyCost = 3;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(135, 166);
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            Card wish = PickCard(new() { Dict.cardL[361], Dict.cardL[362], Dict.cardL[363] }, "use");
            if (Upgraded)
                wish.UpgradeCard();
            wish.CardAction(hero, encounter, turnNumber);
        }

        public override void UpgradeCard()
        {
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Choose one: Gain {(Upgraded ? $"8" : "6")} Plated Armor, {(Upgraded ? $"4" : "3")} Strength, or {(Upgraded ? $"30" : "25")} Gold.";
        }

        public override Card AddCard()
        {
            return new Wish();
        }
    }
}