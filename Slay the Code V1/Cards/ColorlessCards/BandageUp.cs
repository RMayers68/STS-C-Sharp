﻿
namespace STV
{
    public class BandageUp : Card
    {
        public BandageUp(bool Upgraded = false)
        {
            Name = "Bandage Up";
            Type = "Skill";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = ;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            MagicNumber = 4;
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
            return DescriptionModifier + $"Heal {MagicNumber} HP. Exhaust.";
        }

        public override Card AddCard()
        {
            return new BandageUp();
        }
    }
}