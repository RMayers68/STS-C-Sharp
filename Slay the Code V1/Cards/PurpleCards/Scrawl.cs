﻿
namespace STV
{
    public class Scrawl : Card
    {
        public Scrawl(bool Upgraded = false)
        {
            Name = "Scrawl";
            Type = "Skill";
            Rarity = "Rare";
            DescriptionModifier = "";
            EnergyCost = ;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            CardsDrawn = 10;
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
            return DescriptionModifier + $"Draw cards until your hand is full. Exhaust.";
        }

        public override Card AddCard()
        {
            return new Scrawl();
        }
    }
}
