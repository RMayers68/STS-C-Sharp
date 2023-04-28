
namespace STV
{
    public class JustLucky : Card
    {
        public JustLucky(bool Upgraded = false)
        {
            Name = "Just Lucky";
            Type = "Attack";
            Rarity = "Common";
            DescriptionModifier = "";
            EnergyCost = ;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            AttackDamage = 3;
            AttackLoops = 1;
            BlockAmount = 2;
            BlockLoops = 1;
            MagicNumber = 1;
            Targetable = true;
            SingleAttack = true;
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
            return DescriptionModifier + $"Scry {MagicNumber}. Gain {BlockAmount} Block. Deal {AttackDamage} damage.";
        }

        public override Card AddCard()
        {
            return new JustLucky();
        }
    }
}