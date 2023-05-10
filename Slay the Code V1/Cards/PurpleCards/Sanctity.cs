
namespace STV
{
    public class Sanctity : Card
    {
        public Sanctity(bool Upgraded = false)
        {
            Name = "Sanctity";
            Type = "Skill";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = 1;
            if (EnergyCost >= 0)
                TmpEnergyCost = EnergyCost;
            GoldCost = CardRNG.Next(68, 83);
            BlockAmount = 6;
            CardsDrawn = 2;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            hero.CardBlock(BlockAmount, encounter);
            string lastCardPlayed = hero.Actions.FindLast(x => x.Contains("Played"));
            if (lastCardPlayed != null && lastCardPlayed.Contains("Skill"))
                hero.DrawCards(CardsDrawn);
        }

        public override void UpgradeCard()
        {
            if (!Upgraded) 
                BlockAmount += 3;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Gain {BlockAmount} Block. If the last card played was a Skill, draw 2 cards.";
        }

        public override Card AddCard()
        {
            return new Sanctity();
        }
    }
}