
namespace STV
{
    public class Immolate : Card
    {
        public Immolate(bool Upgraded = false)
        {
            Name = "Immolate";
            Type = "Attack";
            Rarity = "Rare";
            DescriptionModifier = "";
            EnergyCost = 2;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(135, 166);
            AttackDamage = 21;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            foreach (Enemy e in encounter)
            {
                hero.Attack(e, AttackDamage+extraDamage, encounter);
            }
            hero.AddToDiscard(new Burn());
        }

        public override void UpgradeCard()
        {
            if (!Upgraded) 
                AttackDamage += 7;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Deal {AttackDamage} damage to ALL enemies. Shuffle a Burn into your discard pile.";
        }

        public override Card AddCard()
        {
            return new Immolate();
        }
    }
}