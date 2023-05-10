
namespace STV
{
    public class CalculatedGamble : Card
    {
        public CalculatedGamble(bool Upgraded = false)
        {
            Name = "Calculated Gamble";
            Type = "Skill";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = 0;
            if (EnergyCost >= 0)
                TmpEnergyCost = EnergyCost;
            GoldCost = CardRNG.Next(68, 83);
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            int gamba = 0;
            for (int i = hero.Hand.Count - 1; i >= 0; i--)
            {
                hero.Hand[i].Discard(hero, encounter, turnNumber);
                gamba++;
            }
            hero.DrawCards(gamba);
        }

        public override void UpgradeCard()
        {
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Discard your hand, then draw that many cards.{(Upgraded ? $"" : " Exhaust.")}";
        }

        public override Card AddCard()
        {
            return new CalculatedGamble();
        }
    }
}