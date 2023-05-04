
namespace STV
{
    public class Madness : Card
    {
        public Madness(bool Upgraded = false)
        {
            Name = "Madness";
            Type = "Skill";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = 1;
            if (EnergyCost >= 0)
                TmpEnergyCost = EnergyCost;
            GoldCost = CardRNG.Next(68, 83);
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            if (hero.Hand.Count > 0 && hero.Hand.FindAll(x => x.EnergyCost > 0).Count > 0)
                hero.Hand.FindAll(x => x.EnergyCost > 0)[CardRNG.Next(hero.Hand.FindAll(x => x.EnergyCost > 0).Count)].SetEnergyCost(0);
        }

        public override void UpgradeCard()
        {
            if (!Upgraded) 
                EnergyCost--;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"A random card in your hand costs 0 for the rest of combat. Exhaust.";
        }

        public override Card AddCard()
        {
            return new Madness();
        }
    }
}