﻿
namespace STV
{
    public class Worship : Card
    {
        public Worship(bool Upgraded = false)
        {
            Name = "Worship";
            Type = "Skill";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = ;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            BuffID = 10;
            BuffAmount = 5;
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
            return DescriptionModifier + $"{(Upgraded ? ";
                }

                public override Card AddCard()
                {
                        return new Worship();
                }
        }
}