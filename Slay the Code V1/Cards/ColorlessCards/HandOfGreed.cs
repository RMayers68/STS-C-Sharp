
namespace STV
{
    public class HandofGreed : Card
    {
        public HandofGreed(bool Upgraded = false)
        {
            Name = "Hand of Greed";
            Type = "Attack";
            Rarity = "Rare";
            DescriptionModifier = "";
            EnergyCost = 2;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            AttackDamage = 20;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            int target = hero.DetermineTarget(encounter);
            hero.Attack(encounter[target], AttackDamage + extraDamage);
            if (encounter[target].Hp <= 0 && !encounter[target].HasBuff("Minion"))
                hero.GoldChange(AttackDamage);
        }

        public override void UpgradeCard()
        {
            if (!Upgraded) 
                AttackDamage +=5;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Deal {AttackDamage} damage. If Fatal, gain {AttackDamage} Gold.";
        }

        public override Card AddCard()
        {
            return new HandofGreed();
        }
    }
}