
namespace STV
{
    public class Darkness : Card
    {
        public Darkness(bool Upgraded = false)
        {
            Name = "Darkness";
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
            Orb.ChannelOrb(hero, encounter, 2);
            if (Upgraded)
                foreach (var orb in hero.Orbs)
                {
                    if (orb is null || orb.Name != "Dark")
                        continue;
                    orb.Effect += 6;
                    Console.WriteLine($"The {orb.Name} Orb stored 6 more Energy!");
                }
        }

        public override void UpgradeCard()
        {
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Channel 1 Dark. {(Upgraded ? $"Trigger the ability of all your passive Dark Orbs." : "")}";

        }

        public override Card AddCard()
        {
            return new Darkness();
        }
    }
}