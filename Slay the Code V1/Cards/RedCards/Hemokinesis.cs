
namespace STV
{
    public class Hemokinesis : Card
    {
        public Hemokinesis(bool Upgraded = false)
        {
            Name = "Hemokinesis";
            Type = "Attack";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = ;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            AttackDamage = 15;
            AttackLoops = 1;
            MagicNumber = 2;
            Targetable = true;
            SingleAttack = true;
            SelfDamage = true;
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
            return DescriptionModifier + $"Lose {MagicNumber} HP. {AttackDamage} damage.";
        }

        public override Card AddCard()
        {
            return new Hemokinesis();
        }
    }
}
