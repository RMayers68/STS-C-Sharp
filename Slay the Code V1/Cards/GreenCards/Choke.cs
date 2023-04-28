
namespace STV
{
    public class Choke : Card
    {
        public Choke(bool Upgraded = false)
        {
            Name = "Choke";
            Type = "Attack";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = ;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            AttackDamage = 12;
            AttackLoops = 1;
            BuffID = 42;
            BuffAmount = 3;
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
            return DescriptionModifier + $"Deal {AttackDamage} damage. Whenever you play a card this turn, targeted enemy loses {BuffAmount} HP.";
        }

        public override Card AddCard()
        {
            return new Choke();
        }
    }
}