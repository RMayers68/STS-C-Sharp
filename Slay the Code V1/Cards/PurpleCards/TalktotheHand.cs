
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
            EnergyCost = 1;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            AttackDamage = 5;
            BuffID = 83;
            BuffAmount = 2;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            int target = hero.DetermineTarget(encounter);
            hero.Attack(encounter[target], AttackDamage + extraDamage);
            encounter[target].AddBuff(BuffID, BuffAmount, hero);
        }

        public override void UpgradeCard()
        {
            if (!Upgraded)
            {
                AttackDamage += 2;
                BuffAmount++;
            }
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