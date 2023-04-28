﻿namespace STV
{
    public class BloodforBlood : Card
    {
        public BloodforBlood(bool Upgraded = false)
        {
            Name = "Blood for Blood";
            Type = "Attack";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = 4;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            AttackDamage = 18;
            AttackLoops = 1;
            Targetable = true;
            SingleAttack = true;
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
            return DescriptionModifier + $"Costs 1 less Energy for each time you lose HP in combat. Deal {AttackDamage} damage.";
        }

        public override Card AddCard()
        {
            return new BloodforBlood();
        }
    }
}