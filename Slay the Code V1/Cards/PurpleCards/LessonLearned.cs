
namespace STV
{
    public class LessonLearned : Card
    {
        public LessonLearned(bool Upgraded = false)
        {
            Name = "Lesson Learned";
            Type = "Attack";
            Rarity = "Rare";
            DescriptionModifier = "";
            EnergyCost = 2;
            if (EnergyCost >= 0)
                TmpEnergyCost = EnergyCost;
            GoldCost = CardRNG.Next(135, 166);
            AttackDamage = 10;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            int target = hero.DetermineTarget(encounter);
            hero.Attack(encounter[target], AttackDamage + extraDamage);
            if (encounter[target].Hp <= 0 && !encounter[target].HasBuff("Minion") && hero.Deck.Any(x => !x.Upgraded))
                    hero.Deck.FindAll(x => !x.Upgraded)[CardRNG.Next(hero.Deck.FindAll(x => !x.Upgraded).Count)].UpgradeCard();
        }

        public override void UpgradeCard()
        {
            if (!Upgraded) 
                AttackDamage += 3;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Deal {AttackDamage} damage. If Fatal, Upgrade a random card in your deck. Exhaust.";
        }

        public override Card AddCard()
        {
            return new LessonLearned();
        }
    }
}
