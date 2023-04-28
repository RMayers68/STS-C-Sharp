﻿
namespace STV
{
    public class Doppelganger : Card
    {
        public Doppelganger(bool Upgraded = false)
        {
            Name = "Doppelganger";
            Type = "Skill";
            Rarity = "Rare";
            DescriptionModifier = "";
            EnergyCost = ;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            BuffID = 22;
            HeroBuff = true;
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
            return DescriptionModifier + $"Next turn, draw X{(Upgraded ? ";
                }

                public override Card AddCard()
                {
                        return new Doppelganger();
                }
        }
}