
namespace STV
{
    public class Burn : Card
    {
        public Burn(bool Upgraded = false)
        {
            Name = "Burn";
            Type = "Status";
            Rarity = "Common";
            DescriptionModifier = "";
            EnergyCost = -2;
            AttackDamage = 2;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            hero.NonAttackDamage(hero, AttackDamage, "Burn");
        }

        public override void UpgradeCard()
        {
            if (!Upgraded) 
                AttackDamage += 2;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Unplayable. At the end of your turn, take {AttackDamage} damage.";
        }

        public override Card AddCard()
        {
            return new Burn();
        }
    }
}