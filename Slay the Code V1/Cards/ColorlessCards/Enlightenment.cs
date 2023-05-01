
namespace STV
{
    public class Enlightenment : Card
    {
        public Enlightenment(bool Upgraded = false)
        {
            Name = "Enlightenment";
            Type = "Skill";
            Rarity = "Rare";
            DescriptionModifier = "";
            EnergyCost = 0;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            foreach (Card c in hero.Hand)
                if (EnergyCost > MagicNumber)
                {
                    if (Upgraded)
                        c.SetEnergyCost(1);
                    else c.TmpEnergyCost = 1;
                }
        }

        public override void UpgradeCard()
        {
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Reduce the cost of cards in your hand to 1 this {(Upgraded ? $"combat" : "turn")}.";
        }

        public override Card AddCard()
        {
            return new Enlightenment();
        }
    }
}
