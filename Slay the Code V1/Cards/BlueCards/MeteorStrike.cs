
namespace STV
{
    public class MeteorStrike : Card
    {
        public MeteorStrike(bool Upgraded = false)
        {
            Name = "Meteor Strike";
            Type = "Attack";
            Rarity = "Rare";
            DescriptionModifier = "";
            EnergyCost = ;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            AttackDamage = 24;
            AttackLoops = 1;
            BlockLoops = 3;
            MagicNumber = 3;
            Targetable = true;
            SingleAttack = true;
            OrbChannels = true;
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
            return DescriptionModifier + $"Deal {AttackDamage} damage. Channel 3 Plasma.";
        }

        public override Card AddCard()
        {
            return new MeteorStrike();
        }
    }
}