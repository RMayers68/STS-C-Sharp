﻿
namespace STV
{
    public class EmptyMind : Card
    {
        public EmptyMind(bool Upgraded = false)
        {
            Name = "Empty Mind";
            Type = "Skill";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = ;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            CardsDrawn = 2;
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
            return DescriptionModifier + $"Draw {CardsDrawn} cards. Exit your Stance.";
        }

        public override Card AddCard()
        {
            return new EmptyMind();
        }
    }
}