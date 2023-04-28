﻿
namespace STV
{
    public class DeadlyPoison : Card
    {
        public DeadlyPoison(bool Upgraded = false)
        {
            Name = "Deadly Poison";
            Type = "Skill";
            Rarity = "Common";
            DescriptionModifier = "";
            EnergyCost = ;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            BuffID = 39;
            BuffAmount = 5;
            Targetable = true;
            EnemyBuff = true;
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
            return DescriptionModifier + $"Apply {BuffAmount} Poison.";
        }

        public override Card AddCard()
        {
            return new DeadlyPoison();
        }
    }
}