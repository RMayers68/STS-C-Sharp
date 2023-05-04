﻿
namespace STV
{
    public class Trip : Card
    {
        public Trip(bool Upgraded = false)
        {
            Name = "Trip";
            Type = "Skill";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = 0;
            if (EnergyCost >= 0)
                TmpEnergyCost = EnergyCost;
            GoldCost = CardRNG.Next(68, 83);
            BuffID = 1;
            BuffAmount = 2;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            if (Upgraded)
                foreach (Enemy e in encounter)
                    hero.Attack(e, AttackDamage + extraDamage, encounter);
            else
            {
                int target = hero.DetermineTarget(encounter);
                encounter[target].AddBuff(BuffID, BuffAmount, hero);
            }
        }

        public override void UpgradeCard()
        {
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Apply 2 Vulnerable{(Upgraded ? $" to ALL enemies" : "")}.";
        }

        public override Card AddCard()
        {
            return new Trip();
        }
    }
}