﻿
namespace STV
{
    public class Nightmare : Card
    {
        public Nightmare(bool Upgraded = false)
        {
            Name = "Nightmare";
            Type = "Skill";
            Rarity = "Rare";
            DescriptionModifier = "";
            EnergyCost = ;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
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
            return DescriptionModifier + $"Choose a card. Next turn, add 3 copies of that card into your hand. Exhaust.";
        }

        public override Card AddCard()
        {
            return new Nightmare();
        }
    }
}
