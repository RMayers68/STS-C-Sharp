
namespace STV
{
    public class Wallop : Card
    {
        public Wallop(bool Upgraded = false)
        {
            Name = "Wallop";
            Type = "Attack";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = 2;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(68, 83);
            AttackDamage = 9;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            int wallop = 0, enemyHP = 0;
            foreach (Enemy e in encounter)
            {
                enemyHP += e.Hp;
                hero.Attack(e, AttackDamage + extraDamage, encounter);
                wallop += e.Hp;
            }
            hero.CardBlock(wallop, encounter);
        }

        public override void UpgradeCard()
        {
            if (!Upgraded) 
               AttackDamage += 3;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Deal {AttackDamage} damage. Gain Block equal to unblocked damage dealt.";
        }

        public override Card AddCard()
        {
            return new Wallop();
        }
    }
}