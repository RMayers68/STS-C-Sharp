
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
            EnergyCost = ;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            BuffID = 39;
            BuffAmount = 6;
            Targetable = true;
            EnemyBuff = true;
            if (upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
        }

        public override void UpgradeCard()
        {
            if (!Upgraded) ;
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