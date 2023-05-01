﻿
namespace STV
{
    public class Vigilance : Card
    {
        public Vigilance(bool Upgraded = false)
        {
            Name = "Vigilance";
            Type = "Skill";
            Rarity = "Common";
            DescriptionModifier = "";
            EnergyCost = 2;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            BlockAmount = 8;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            hero.CardBlock(BlockAmount, encounter);
            hero.SwitchStance("Calm");
        }

        public override void UpgradeCard()
        {
            if (!Upgraded) 
                BlockAmount += 4;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Gain {BlockAmount} Block. Enter Calm.";
        }

        public override Card AddCard()
        {
            return new Vigilance();
        }
    }
}
