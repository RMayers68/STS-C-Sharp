
namespace STV
{
    public class SecondWind : Card
    {
        public SecondWind(bool Upgraded = false)
        {
            Name = "Second Wind";
            Type = "Skill";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = 1;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            BlockAmount = 5;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            int secondWind = 0;
            for (int i = hero.Hand.Count - 1; i >= 0; i--)
            {
                if (hero.Hand[i].Type != "Attack")
                {
                    hero.Hand[i].Exhaust(hero, encounter, hero.Hand);
                    secondWind++;
                }
            }
            for (int i = 0; i < secondWind; i++)
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
            return DescriptionModifier + $"Exhaust all non-Attack cards in your hand and gain {BlockAmount} Block for each.";
        }

        public override Card AddCard()
        {
            return new SecondWind();
        }
    }
}