﻿
namespace STV
{
    public class Neutralize : Card
    {
        public Neutralize(bool Upgraded = false)
        {
            Name = "Neutralize";
            Type = "Attack";
            Rarity = "Basic";
            DescriptionModifier = "";
            EnergyCost = 0;
            if (EnergyCost >= 0)
                TmpEnergyCost = EnergyCost;
            GoldCost = 0;
            AttackDamage = 3;
            BuffID = 2;
            BuffAmount = 1;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            int target = DetermineTarget(encounter);
            hero.Attack(encounter[target], AttackDamage + extraDamage);
            encounter[target].AddBuff(BuffID, BuffAmount,hero);
        }

        public override void UpgradeCard()
        {
            if (!Upgraded)
            {
                AttackDamage++;
                MagicNumber++;
            }
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Deal {AttackDamage} damage. Apply {BuffAmount} Weak.";
        }

        public override Card AddCard()
        {
            return new Neutralize();
        }
    }
}