
namespace STV
{
    public class Rampage : Card
    {
        public Rampage(bool Upgraded = false)
        {
            Name = "Rampage";
            Type = "Attack";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = 1;
            if (EnergyCost >= 0)
                TmpEnergyCost = EnergyCost;
            GoldCost = CardRNG.Next(68, 83);
            AttackDamage = 8;
            MagicNumber = 5;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            int target = DetermineTarget(encounter);
            hero.Attack(encounter[target], AttackDamage + extraDamage);
            AttackDamage += MagicNumber;
        }

        public override void UpgradeCard()
        {
            if (!Upgraded) 
                MagicNumber += 3;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Deal {AttackDamage} damage. Every time this card is played, increase its damage by {MagicNumber} for this combat.";
        }

        public override Card AddCard()
        {
            return new Rampage();
        }
    }
}