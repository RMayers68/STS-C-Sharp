
namespace STV
{
    public class TrueGrit : Card
    {
        public TrueGrit(bool Upgraded = false)
        {
            Name = "True Grit";
            Type = "Skill";
            Rarity = "Common";
            DescriptionModifier = "";
            EnergyCost = 1;
            if (EnergyCost >= 0)
                TmpEnergyCost = EnergyCost;
            GoldCost = CardRNG.Next(45, 56);
            BlockAmount = 7;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            if (Upgraded)
                Card.PickCard(hero.Hand, "exhaust").Exhaust(hero, encounter, hero.Hand);
            else hero.Hand[CardRNG.Next(hero.Hand.Count)].Exhaust(hero, encounter, hero.Hand);
            hero.CardBlock(BlockAmount, encounter);
        }

        public override void UpgradeCard()
        {
            if (!Upgraded) 
                BlockAmount += 2;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Gain {BlockAmount} Block. Exhaust a {(Upgraded ? $"" : "random")} card from your hand.";
        }

        public override Card AddCard()
        {
            return new TrueGrit();
        }
    }
}
