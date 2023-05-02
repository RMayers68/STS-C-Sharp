
namespace STV
{
    public class StormofSteel : Card
    {
        public StormofSteel(bool Upgraded = false)
        {
            Name = "Storm of Steel";
            Type = "Skill";
            Rarity = "Rare";
            DescriptionModifier = "";
            EnergyCost = 1;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(135, 166);
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            int storm = 0;
            for (int i = hero.Hand.Count - 1; i >= 0; i--)
            {
                hero.Hand[i].Discard(hero, encounter, turnNumber);
                storm++;
            }
            AddShivs(hero, storm);
            if (Upgraded)
                foreach (Card c in hero.Hand)
                    c.UpgradeCard();
        }

        public override void UpgradeCard()
        {
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Discard your hand. Add 1 Shiv{(Upgraded ? $"+" : "")} into your hand for each card discarded.";
        }

        public override Card AddCard()
        {
            return new StormofSteel();
        }
    }
}