namespace STV
{
    public class Violence : Card
    {
        public Violence(bool Upgraded = false)
        {
            Name = "Safety";
            Type = "Skill";
            Rarity = "Common";
            DescriptionModifier = "";
            EnergyCost = 0;
            if (EnergyCost >= 0)
                TmpEnergyCost = EnergyCost;
            GoldCost = CardRNG.Next(45, 56);
            MagicNumber = 3;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            for (int i = 0; i < MagicNumber; i++)
            {
                Card violence = hero.DrawPile.Find(x => x.Type == "Attack");
                hero.AddToHand(violence);
                hero.DrawPile.Remove(violence);
            }
        }

        public override void UpgradeCard()
        {
            if (!Upgraded) 
                MagicNumber++;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Retain. Gain {BlockAmount} Block. Exhaust.";
        }

        public override Card AddCard()
        {
            return new Violence();
        }
    }
}