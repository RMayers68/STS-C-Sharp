
namespace STV
{
    public class Havoc : Card
    {
        public Havoc(bool Upgraded = false)
        {
            Name = "Havoc";
            Type = "Skill";
            Rarity = "Common";
            DescriptionModifier = "";
            EnergyCost = 1;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            Card havoc = hero.DrawPile.Last();
            havoc.CardAction(hero, encounter, turnNumber);
            havoc.Exhaust(hero, encounter, hero.DrawPile);
        }

        public override void UpgradeCard()
        {
            if (!Upgraded) 
                EnergyCost--;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Play the top card of your draw pile and Exhaust it.";
        }

        public override Card AddCard()
        {
            return new Havoc();
        }
    }
}