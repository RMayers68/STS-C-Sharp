
namespace STV
{
    public class Blizzard : Card
    {
        public Blizzard(bool Upgraded = false)
        {
            Name = "Blizzard";
            Type = "Attack";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = 1;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(68, 83);
            MagicNumber = 2;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            foreach (string s in hero.Actions)
                if (s.Contains("Channel Frost"))
                    extraDamage += MagicNumber;
            foreach (Enemy e in encounter)
                hero.Attack(e, AttackDamage+extraDamage, encounter);
        }

        public override void UpgradeCard()
        {
            if (!Upgraded) 
                MagicNumber++;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Deal damage equal to {MagicNumber} times the Frost Channeled this combat to ALL enemies.";
        }

        public override Card AddCard()
        {
            return new Blizzard();
        }
    }
}