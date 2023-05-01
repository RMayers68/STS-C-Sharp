
namespace STV
{
    public class Fission : Card
    {
        public Fission(bool Upgraded = false)
        {
            Name = "Fission";
            Type = "Skill";
            Rarity = "Rare";
            DescriptionModifier = "";
            EnergyCost = 0;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            int fission = 0;
            foreach (Orb o in hero.Orbs)
            {
                if (Upgraded)
                    o.Evoke(hero, encounter);
                hero.Orbs.Remove(o);
                fission++;
            }
            hero.DrawCards(fission);
            hero.GainTurnEnergy(fission);
        }

        public override void UpgradeCard()
        {
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"{(Upgraded ? $"Evoke" : "Remove")}  ALL of your Orbs. Gain 1 Energy and draw 1 card for each Orb. Exhaust.";

        }

        public override Card AddCard()
        {
            return new Fission();
        }
    }
}