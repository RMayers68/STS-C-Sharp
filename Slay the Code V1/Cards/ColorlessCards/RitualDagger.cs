
namespace STV
{
    public class RitualDagger : Card
    {
        public RitualDagger(bool Upgraded = false)
        {
            Name = "Ritual Dagger";
            Type = "Attack";
            Rarity = "Common";
            DescriptionModifier = "";
            EnergyCost = 1;
            if (EnergyCost >= 0)
                TmpEnergyCost = EnergyCost;
            GoldCost = CardRNG.Next(45, 56);
            AttackDamage = 15;
            MagicNumber = 3;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            int target = DetermineTarget(encounter);
            hero.Attack(encounter[target], AttackDamage + extraDamage);
            if (encounter[target].Hp <= 0 && !encounter[target].HasBuff("Minion"))
            {
                hero.Deck.FindAll(x => x.Name == Name).Find(x => x.AttackDamage == AttackDamage).AttackDamage += MagicNumber;
                AttackDamage += MagicNumber;
            }
        }

        public override void UpgradeCard()
        {
            if (!Upgraded) 
                MagicNumber += 2;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Deal {AttackDamage} Damage. If this kills an enemy, permanently increase this card's damage by {MagicNumber}. Exhaust.";
        }

        public override Card AddCard()
        {
            return new RitualDagger();
        }
    }
}
