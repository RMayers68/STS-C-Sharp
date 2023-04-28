
namespace STV
{
    public class SignatureMove : Card
    {
        public SignatureMove(bool Upgraded = false)
        {
            Name = "Signature Move";
            Type = "Attack";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = ;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            AttackDamage = 30;
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
            return DescriptionModifier + $"Can only be played if this is the only Attack in your hand. Deal {AttackDamage} damage.";
        }

        public override Card AddCard()
        {
            return new SignatureMove();
        }
    }
}