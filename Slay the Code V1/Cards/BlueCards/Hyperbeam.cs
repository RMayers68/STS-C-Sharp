
namespace STV
{
    public class Hyperbeam : Card
    {
        public Hyperbeam(bool Upgraded = false)
        {
            Name = "Hyperbeam";
            Type = "Attack";
            Rarity = "Rare";
            DescriptionModifier = "";
            EnergyCost = ;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            AttackDamage = 26;
            AttackLoops = 1;
            BuffID = 7;
            BuffAmount = 3;
            AttackAll = true;
            if (Upgraded)
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
            return DescriptionModifier + $"Deal {AttackDamage} damage to ALL enemies. Lose 3 Focus.";
        }

        public override Card AddCard()
        {
            return new Hyperbeam();
        }
    }
}