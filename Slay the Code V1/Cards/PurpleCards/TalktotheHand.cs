
namespace STV
{
    public class TalktotheHand : Card
    {
        public TalktotheHand(bool Upgraded = false)
        {
            Name = "Talk to the Hand";
            Type = "Attack";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = ;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            AttackDamage = 5;
            AttackLoops = 1;
            BuffID = 83;
            BuffAmount = 2;
            Targetable = true;
            EnemyBuff = true;
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
            return DescriptionModifier + $"Deal {AttackDamage} damage. Whenever you attack this enemy, gain {BuffAmount} Block. Exhaust.";
        }

        public override Card AddCard()
        {
            return new TalktotheHand();
        }
    }
}