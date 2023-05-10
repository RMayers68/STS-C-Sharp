
namespace STV
{
    public class Dualcast : Card
    {
        public Dualcast(bool Upgraded = false)
        {
            Name = "Dualcast";
            Type = "Skill";
            Rarity = "Basic";
            DescriptionModifier = "";
            EnergyCost = 1;
            if (EnergyCost >= 0)
                TmpEnergyCost = EnergyCost;
            GoldCost = 0;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            hero.Orbs[0].Evoke(hero, encounter);
            hero.Orbs[0].Evoke(hero, encounter);
            hero.Orbs.RemoveAt(0);
        }

        public override void UpgradeCard()
        {
            if (!Upgraded) 
                EnergyCost--;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Evoke your next Orb twice.";
        }

        public override Card AddCard()
        {
            return new Dualcast();
        }
    }
}