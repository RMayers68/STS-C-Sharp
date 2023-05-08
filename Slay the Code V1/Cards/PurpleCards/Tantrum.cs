
namespace STV
{
    public class Tantrum : Card
    {
        public Tantrum(bool Upgraded = false)
        {
            Name = "Tantrum";
            Type = "Attack";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = 1;
            if (EnergyCost >= 0)
                TmpEnergyCost = EnergyCost;
            GoldCost = CardRNG.Next(68, 83);
            AttackDamage = 3;
            MagicNumber = 3;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            int target = DetermineTarget(encounter);
            for (int i = 0; i < MagicNumber; i++) 
                hero.Attack(encounter[target], AttackDamage + extraDamage);
            hero.SwitchStance("Wrath");
        }

        public override void UpgradeCard()
        {
            if (!Upgraded) 
                MagicNumber++;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Deal {AttackDamage} damage {MagicNumber} times. Enter Wrath. Shuffle this card into your draw pile.";
        }

        public override Card AddCard()
        {
            return new Tantrum();
        }
    }
}