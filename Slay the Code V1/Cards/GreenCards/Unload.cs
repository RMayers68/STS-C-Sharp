
namespace STV
{
    public class Unload : Card
    {
        public Unload(bool Upgraded = false)
        {
            Name = "Unload";
            Type = "Attack";
            Rarity = "Rare";
            DescriptionModifier = "";
            EnergyCost = 1;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(135, 166);
            AttackDamage = 14;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            int target = hero.DetermineTarget(encounter);
            hero.Attack(encounter[target], AttackDamage + extraDamage);
            for (int i = hero.Hand.Count - 1; i >= 0; i--)
            {
                if (hero.Hand[i].Type == "Attack")
                    hero.Hand[i].Discard(hero, encounter, turnNumber);
            }
        }

        public override void UpgradeCard()
        {
            if (!Upgraded) 
                AttackDamage += 4;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Deal {AttackDamage} damage. Discard ALL non-Attack cards.";
        }

        public override Card AddCard()
        {
            return new Unload();
        }
    }
}