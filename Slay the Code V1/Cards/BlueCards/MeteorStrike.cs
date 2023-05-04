
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
            EnergyCost = 5;
            if (EnergyCost >= 0)
                TmpEnergyCost = EnergyCost;
            GoldCost = CardRNG.Next(135, 166);
            AttackDamage = 24;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            int target = hero.DetermineTarget(encounter);
            hero.Attack(encounter[target], AttackDamage + extraDamage);
            for (int i = 0; i < 3; i++)
                Orb.ChannelOrb(hero, encounter, 3);
        }

        public override void UpgradeCard()
        {
            if (!Upgraded) 
                AttackDamage += 6;
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