
namespace STV
{
    public class Recursion : Card
    {
        public Recursion(bool Upgraded = false)
        {
            Name = "Recursion";
            Type = "Skill";
            Rarity = "Common";
            DescriptionModifier = "";
            EnergyCost = 1;
            if (EnergyCost >= 0)
                TmpEnergyCost = EnergyCost;
            GoldCost = CardRNG.Next(45, 56);
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            Orb recursion = hero.Orbs[0];
            hero.Orbs[0].Evoke(hero, encounter);
            hero.Orbs.RemoveAt(0);
            hero.Orbs.Add(recursion);
        }

        public override void UpgradeCard()
        {
            if (!Upgraded) 
                EnergyCost--;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Evoke your next Orb. Channel the Orb that was just Evoked.";
        }

        public override Card AddCard()
        {
            return new Recursion();
        }
    }
}