﻿
namespace STV
{
    public class Dualcast : Card
    {
        public Dualcast(bool Upgraded = false)
        {
            Name = "Dualcast";
            Type = "Skill";
            Rarity = "Common";
            DescriptionModifier = "";
            EnergyCost = 1;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            hero.Orbs[0].Evoke(hero, encounter);
            hero.Orbs[0].Evoke(hero, encounter);
            hero.Orbs.RemoveAt(0);
        }

        public override void UpgradeCard()
        {
            if (!Upgraded) 
                EnergyCost--;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Evoke your next Orb twice.";
        }

        public override Card AddCard()
        {
            return new Dualcast();
        }
    }
}