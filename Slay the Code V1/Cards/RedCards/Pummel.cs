﻿
namespace STV
{
    public class Pummel : Card
    {
        public Pummel(bool Upgraded = false)
        {
            Name = "Pummel";
            Type = "Attack";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = ;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            AttackDamage = 2;
            AttackLoops = 4;
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
            return DescriptionModifier + $"Deal {AttackDamage} damage {AttackLoops} times. Exhaust.";
        }

        public override Card AddCard()
        {
            return new Pummel();
        }
    }
}