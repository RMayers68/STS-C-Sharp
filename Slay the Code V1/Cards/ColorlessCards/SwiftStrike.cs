﻿
namespace STV
{
    public class SwiftStrike : Card
    {
        public SwiftStrike(bool Upgraded = false)
        {
            Name = "Swift Strike";
            Type = "Attack";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = 0;
            if (EnergyCost >= 0)
                TmpEnergyCost = EnergyCost;
            GoldCost = CardRNG.Next(68, 83);
            AttackDamage = 7;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            int target = DetermineTarget(encounter);
            hero.Attack(encounter[target], AttackDamage + extraDamage);

        }

        public override void UpgradeCard()
        {
            if (!Upgraded) 
                AttackDamage +=3;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Deal {AttackDamage} damage.";
        }

        public override Card AddCard()
        {
            return new SwiftStrike();
        }
    }
}