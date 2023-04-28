
namespace STV
{
    public class MasterfulStab : Card
    {
        public MasterfulStab(bool Upgraded = false)
        {
            Name = "Masterful Stab";
            Type = "Attack";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = ;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            AttackDamage = 12;
            AttackLoops = 1;
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
            return DescriptionModifier + $"Cost 1 additional for each time you lose HP this combat. Deal {AttackDamage} Damage.";
        }

        public override Card AddCard()
        {
            return new MasterfulStab();
        }
    }
}