
namespace STV
{
    public class JustLucky : Card
    {
        public JustLucky(bool Upgraded = false)
        {
            Name = "Just Lucky";
            Type = "Attack";
            Rarity = "Common";
            DescriptionModifier = "";
            EnergyCost = 0;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            AttackDamage = 3;
            BlockAmount = 2;
            MagicNumber = 1;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            int target = hero.DetermineTarget(encounter);
            Scry(hero, MagicNumber);
            hero.CardBlock(BlockAmount);
            hero.Attack(encounter[target], AttackDamage + extraDamage);
        }

        public override void UpgradeCard()
        {
            if (!Upgraded)
            {
                MagicNumber++;
                BlockAmount++;
                AttackDamage++;
            }
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Scry {MagicNumber}. Gain {BlockAmount} Block. Deal {AttackDamage} damage.";
        }

        public override Card AddCard()
        {
            return new JustLucky();
        }
    }
}