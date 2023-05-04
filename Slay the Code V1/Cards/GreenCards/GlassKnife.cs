
namespace STV
{
    public class GlassKnife : Card
    {
        public GlassKnife(bool Upgraded = false)
        {
            Name = "Glass Knife";
            Type = "Attack";
            Rarity = "Rare";
            DescriptionModifier = "";
            EnergyCost = 1;
            if (EnergyCost >= 0)
                TmpEnergyCost = EnergyCost;
            GoldCost = CardRNG.Next(135, 166);
            AttackDamage = 8;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            int target = hero.DetermineTarget(encounter);
            for (int i = 0; i < 2; i++) 
                hero.Attack(encounter[target], AttackDamage + extraDamage);
            AttackDamage -= 2;
        }

        public override void UpgradeCard()
        {
            if (!Upgraded) 
                AttackDamage += 4;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Deal {AttackDamage} damage twice. Glass Knife's damage is lowered by 2 this combat.";
        }

        public override Card AddCard()
        {
            return new GlassKnife();
        }
    }
}