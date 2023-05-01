
namespace STV
{
    public class Hologram : Card
    {
        public Hologram(bool Upgraded = false)
        {
            Name = "Hologram";
            Type = "Skill";
            Rarity = "Common";
            DescriptionModifier = "";
            EnergyCost = 1;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            BlockAmount = 3;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            hero.CardBlock(BlockAmount, encounter);
            PickCard(hero.DiscardPile, "add into your hand").MoveCard(hero.DiscardPile, hero.Hand);
        }

        public override void UpgradeCard()
        {
            if (!Upgraded) 
                BlockAmount += 2;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Gain {BlockAmount} Block. Return a card from your discard pile to your hand. {(Upgraded ? $"" : "Exhaust")}";
        }

        public override Card AddCard()
        {
            return new Hologram();
        }
    }
}