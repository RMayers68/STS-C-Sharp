
namespace STV
{
    public class SeverSoul : Card
    {
        public SeverSoul(bool Upgraded = false)
        {
            Name = "Sever Soul";
            Type = "Attack";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = 2;
            if (EnergyCost >= 0)
                TmpEnergyCost = EnergyCost;
            GoldCost = CardRNG.Next(68, 83);
            AttackDamage = 16;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            for (int i = hero.Hand.Count - 1; i >= 0; i--)
                if (hero.Hand[i].Type != "Attack")
                    hero.Hand[i].Exhaust(hero, encounter, hero.Hand);
            int target = hero.DetermineTarget(encounter);
            hero.Attack(encounter[target], AttackDamage + extraDamage);
        }

        public override void UpgradeCard()
        {
            if (!Upgraded)
                AttackDamage += 6;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Exhaust all non-Attack cards in your hand. Deal {AttackDamage} damage.";
        }

        public override Card AddCard()
        {
            return new SeverSoul();
        }
    }
}