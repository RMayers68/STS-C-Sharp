﻿
namespace STV
{
    public class Blasphemy : Card
    {
        public Blasphemy(bool Upgraded = false)
        {
            Name = "Blasphemy";
            Type = "Skill";
            Rarity = "Rare";
            DescriptionModifier = "";
            EnergyCost = ;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            BuffID = 71;
            BuffAmount = 1;
            HeroBuff = true;
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
            return DescriptionModifier + $"{(Upgraded ? ";
                }

                public override Card AddCard()
                {
                        return new Blasphemy();
                }
        }
}