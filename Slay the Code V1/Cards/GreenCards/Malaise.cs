﻿
namespace STV
{
    public class Malaise : Card
    {
        public Malaise(bool Upgraded = false)
        {
            Name = "Malaise";
            Type = "Skill";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = -1;
            if (EnergyCost >= 0)
                TmpEnergyCost = EnergyCost;
            GoldCost = CardRNG.Next(68, 83);
            BuffID = 4;
            MagicNumber = 0;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            int target = DetermineTarget(encounter);
            encounter[target].AddBuff(BuffID, -1 * (hero.Energy + MagicNumber));
            encounter[target].AddBuff(1, hero.Energy + MagicNumber);
        }

        public override void UpgradeCard()
        {
            if (!Upgraded) 
                MagicNumber++;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Enemy loses X{(Upgraded ? $"+1" : "")} Strength. Apply X{(Upgraded ? $"+1" : "")} Weak. Exhaust";
        }

        public override Card AddCard()
        {
            return new Malaise();
        }
    }
}