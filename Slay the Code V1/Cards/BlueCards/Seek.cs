
namespace STV
{
    public class Seek : Card
    {
        public Seek(bool Upgraded = false)
        {
            Name = "Seek";
            Type = "Skill";
            Rarity = "Rare";
            DescriptionModifier = "";
            EnergyCost = 0;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(135, 166);
            MagicNumber++;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            for (int i = 0; i < MagicNumber; i++)
                hero.AddToHand(PickCard(hero.DrawPile, "move to your hand"));
        }

        public override void UpgradeCard()
        {
            if (!Upgraded) 
                MagicNumber++;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Choose {(Upgraded ? $"2 cards" : "a card")} from your drawpile and place {(Upgraded ? $"them" : "it")} in your hand.";
        }

        public override Card AddCard()
        {
            return new Seek();
        }
    }
}
