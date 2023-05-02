
using System.Xml.Linq;

namespace STV
{
    public class AllOutAttack : Card
    {
        public AllOutAttack(bool Upgraded = false)
        {
            Name = "All-Out Attack";
            Type = "Attack";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = 1;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(68, 83);
            AttackDamage = 10;
            MagicNumber = 1;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            foreach (Enemy e in encounter)
                hero.Attack(e, AttackDamage+extraDamage, encounter);
            hero.Hand[CardRNG.Next(hero.Hand.Count)].Discard(hero,encounter,turnNumber);
        }

        public override void UpgradeCard()
        {
            if (!Upgraded) 
                AttackDamage += 4;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Deal {AttackDamage} damage to ALL enemies. Discard 1 card at random.";
        }

        public override Card AddCard()
        {
            return new AllOutAttack();
        }




    }
}
