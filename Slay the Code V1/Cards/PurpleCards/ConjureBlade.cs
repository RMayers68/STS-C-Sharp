namespace STV
{
    public class ConjureBlade : Card
    {
        public ConjureBlade(bool Upgraded = false)
        {
            Name = "Conjure Blade";
            Type = "Skill";
            Rarity = "Rare";
            DescriptionModifier = "";
            EnergyCost = -1;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            MagicNumber = 0;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            Card expunger = new Expunger {  MagicNumber = hero.Energy + MagicNumber };
            hero.AddToHand(expunger);
        }

        public override void UpgradeCard()
        {
            if (!Upgraded) 
                MagicNumber++;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Shuffle an Expunger into your draw pile. Exhaust. (Expunger costs 1, deals 9 damage X{(Upgraded ? $"+1" : "")} times.";

        }

        public override Card AddCard()
        {
            return new ConjureBlade();
        }
    }
}