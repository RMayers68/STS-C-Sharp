﻿
namespace STV
{
    public class Chill : Card
    {
        public Chill(bool Upgraded = false)
        {
            Name = "Chill";
            Type = "Skill";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = ;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            BlockLoops = 1;
            MagicNumber = 1;
            OrbChannels = true;
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
            return DescriptionModifier + $"Channel 1 Frost for each enemy in combat. Exhaust.";
        }

        public override Card AddCard()
        {
            return new Chill();
        }
    }
}