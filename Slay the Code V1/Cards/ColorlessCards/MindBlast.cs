
namespace STV
{
    public class MindBlast : Card
    {
        public MindBlast(bool Upgraded = false)
        {
            Name = "Mind Blast";
            Type = "Attack";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = 2;
            if (EnergyCost >= 0)
                TmpEnergyCost = EnergyCost;
            GoldCost = CardRNG.Next(68, 83);
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            int target = DetermineTarget(encounter);
            hero.Attack(encounter[target], hero.DrawPile.Count + extraDamage);
        }

        public override void UpgradeCard()
        {
            if (!Upgraded) 
                EnergyCost--;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Innate. Deal damage equal to the number of cards in your draw pile.";
        }

        public override Card AddCard()
        {
            return new MindBlast();
        }
    }
}