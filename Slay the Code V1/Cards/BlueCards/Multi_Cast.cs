
namespace STV
{
    public class MultiCast : Card
    {
        public MultiCast(bool Upgraded = false)
        {
            Name = "Multi-Cast";
            Type = "Skill";
            Rarity = "Rare";
            DescriptionModifier = "";
            EnergyCost = -1;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(135, 166);
            MagicNumber = 0;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            for (int i = 0; i < hero.Energy + MagicNumber; i++)
                hero.Orbs[0].Evoke(hero, encounter);
            hero.Orbs.RemoveAt(0);
            hero.Energy = 0;
        }

        public override void UpgradeCard()
        {
            if (!Upgraded) 
                MagicNumber++;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Evoke your next Orb X{(Upgraded ? $"+1" : "")} times.";
        }

        public override Card AddCard()
        {
            return new MultiCast();
        }
    }
}