
namespace STV
{
    public class CorpseExplosion : Card
    {
        public CorpseExplosion(bool Upgraded = false)
        {
            Name = "Corpse Explosion";
            Type = "Skill";
            Rarity = "Rare";
            DescriptionModifier = "";
            EnergyCost = 2;
            if (EnergyCost >= 0)
                TmpEnergyCost = EnergyCost;
            GoldCost = CardRNG.Next(135, 166);
            BuffID = 39;
            BuffAmount = 6;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            int target = hero.DetermineTarget(encounter);
            encounter[target].AddBuff(BuffID, BuffAmount,hero);
            encounter[target].AddBuff(43, 1, hero);
        }

        public override void UpgradeCard()
        {
            if (!Upgraded) 
                BuffAmount += 3;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Apply {BuffAmount} Poison. When an enemy dies, deal damage equal to its MAX HP to ALL enemies.";
        }

        public override Card AddCard()
        {
            return new CorpseExplosion();
        }
    }
}