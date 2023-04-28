
namespace STV
{
    public class Juggernaut : Card
    {
        public Juggernaut(bool Upgraded = false)
        {
            Name = "Juggernaut";
            Type = "Power";
            Rarity = "Rare";
            DescriptionModifier = "";
            EnergyCost = ;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            BuffID = 31;
            BuffAmount = 5;
            HeroBuff = true;
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
            return DescriptionModifier + $"Whenever you gain Block, deal {BuffAmount} damage to a random enemy.";
        }

        public override Card AddCard()
        {
            return new Juggernaut();
        }
    }
}