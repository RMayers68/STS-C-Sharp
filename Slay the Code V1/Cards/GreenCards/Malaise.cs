
namespace STV
{
    public class Malaise : Card
    {
        public Malaise(bool Upgraded = false)
        {
            Name = "Malaise";
            Type = "Skill";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = ;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            BuffID = 4;
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
            return DescriptionModifier + $"Enemy loses X{(Upgraded ? ";
                }

                public override Card AddCard()
                {
                        return new Malaise();
                }
        }
}