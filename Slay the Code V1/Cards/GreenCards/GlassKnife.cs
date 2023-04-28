
namespace STV
{
    public class GlassKnife : Card
    {
        public GlassKnife(bool Upgraded = false)
        {
            Name = "Glass Knife";
            Type = "Attack";
            Rarity = "Rare";
            DescriptionModifier = "";
            EnergyCost = ;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            AttackDamage = 8;
            AttackLoops = 2;
            MagicNumber = 2;
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
            return DescriptionModifier + $"Deal {AttackDamage} damage twice. Glass Knife's damage is lowered by 2 this combat.";
        }

        public override Card AddCard()
        {
            return new GlassKnife();
        }
    }
}