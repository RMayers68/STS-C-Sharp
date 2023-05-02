
namespace STV
{
    public class Judgment : Card
    {
        public Judgment(bool Upgraded = false)
        {
            Name = "Judgment";
            Type = "Skill";
            Rarity = "Rare";
            DescriptionModifier = "";
            EnergyCost = 1;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(135, 166);
            MagicNumber = 30;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            int target = hero.DetermineTarget(encounter);
            if (encounter[target].Hp <= MagicNumber)
            {
                encounter[target].Hp = 0;
                Console.WriteLine($"The {encounter[target].Name} has been judged!");
            }
        }

        public override void UpgradeCard()
        {
            if (!Upgraded) 
                MagicNumber += 10;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"If the enemy has {MagicNumber} or less HP, set their HP to 0.";
        }

        public override Card AddCard()
        {
            return new Judgment();
        }
    }
}