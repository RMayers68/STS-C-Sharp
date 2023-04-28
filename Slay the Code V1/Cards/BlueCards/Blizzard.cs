
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
            EnergyCost = ;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            AttackLoops = 1;
            MagicNumber = 2;
            AttackAll = true;
            if (upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
        }

        public override void UpgradeCard()
        {
            if (!Upgraded) ;
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