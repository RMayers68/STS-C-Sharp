﻿
namespace STV
{
    public class MasterofStrategy : Card
    {
        public MasterofStrategy(bool Upgraded = false)
        {
            Name = "Master of Strategy";
            Type = "Skill";
            Rarity = "Rare";
            DescriptionModifier = "";
            EnergyCost = 0;
            if (EnergyCost >= 0)
                TmpEnergyCost = EnergyCost;
            GoldCost = CardRNG.Next(135, 166);
            CardsDrawn = 3;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            hero.DrawCards(CardsDrawn);
        }

        public override void UpgradeCard()
        {
            if (!Upgraded) 
                CardsDrawn++;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Draw {CardsDrawn} cards. Exhaust.";
        }

        public override Card AddCard()
        {
            return new MasterofStrategy();
        }
    }
}
