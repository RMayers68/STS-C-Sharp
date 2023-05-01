
namespace STV
{
    public class Meditate : Card
    {
        public Meditate(bool Upgraded = false)
        {
            Name = "Meditate";
            Type = "Skill";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = 1;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            MagicNumber = 1;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            for (int i = 0; i < MagicNumber; i++)
            {
                if (hero.Hand.Count == 10) break;
                Card meditate = PickCard(hero.DiscardPile, "add to your hand");
                meditate.MoveCard(hero.DiscardPile, hero.Hand);
                meditate.DescriptionModifier += "Retain.";
            }
            hero.SwitchStance("Calm");
        }

        public override void UpgradeCard()
        {
            if (!Upgraded) 
                MagicNumber++;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Put {(Upgraded ? $"2 cards" : "a card")} from your discard pile into your hand and Retain it. Enter Calm. End your turn.";
        }

        public override Card AddCard()
        {
            return new Meditate();
        }
    }
}