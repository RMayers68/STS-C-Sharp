﻿
namespace STV
{
    public class HeavyBlade : Card
    {
        public HeavyBlade(bool Upgraded = false)
        {
            Name = "Heavy Blade";
            Type = "Attack";
            Rarity = "Common";
            DescriptionModifier = "";
            EnergyCost = ;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            AttackDamage = 14;
            AttackLoops = 1;
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
            return DescriptionModifier + $"Deal {AttackDamage} damage. Strength affects Heavy Blade {MagicNumber + 1} times.";
        }

        public override Card AddCard()
        {
            return new HeavyBlade();
        }
    }
}